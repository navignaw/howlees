using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {
	public static GameState gameState;
	public enum State {
		NIGHT,
		DAY,
		UPGRADE,
		PAUSE,
		INTRO,
		START
	}

	// Score and records
	public static int day = 0;
	public static float bestDistance = 0f;
	public static float karma = 0f;
	public static float time = 0f;
	public static float morning = 90;
	public static float noon = 180;
	public static float night = 270;

	static float dayTimeScale = 10;
	static float nightTimeScale = 100;
	static float timeScale = 10;

	public Sisyphus sisyphus;
	public Sun sun;
	public GameObject pauseScreen;
	public GameObject upgradeScreen;
	public GameObject nightScreen;
	public GameObject startScreen;
	public GameObject gameScreen;
	public Texture2D defaultCursor;
	public Texture2D pushCursor;

	static State curGameState = State.START;
	//Boulder boulder;

	// Use this for initialization
	void Start () {
		gameState = this;
		NextDay();
		Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
	}

	// Update is called once per frame
	void Update () {
		time = time % 360;
		sun.Tick(time);

		switch (curGameState)
		{
			case State.NIGHT:
			{
				/* speed up to sundown */
				if (time < morning || time >= night)
					time += nightTimeScale * Time.deltaTime;
			}
				break;
			case State.DAY:
			{
				time += timeScale * Time.deltaTime;
				if (Input.GetMouseButtonDown(0))
					Cursor.SetCursor(pushCursor, Vector2.zero, CursorMode.Auto);
				else if (Input.GetMouseButtonUp(0))
					Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
				if (time >= night) 
				{
					Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
					TurnNight();
				}
			}
				break;
			case State.UPGRADE:
				break;
			case State.PAUSE:
				break;
			case State.INTRO:
				break;
		}
	}

	static public void TurnNight ()
	{
		Init();
		curGameState = State.NIGHT;
		timeScale = nightTimeScale;
		gameState.nightScreen.SetActive(true);
	}

	static public void TurnDay ()
	{
		Init();
		curGameState = State.DAY;
		timeScale = dayTimeScale;
		time = morning;
		gameState.gameScreen.SetActive(true);
		gameState.sisyphus.SetPlayable(true);
		//boulder.SetPlayable(true);
	}

	static public void TurnUpgrade ()
	{
		Init();
		curGameState = State.UPGRADE;
		gameState.upgradeScreen.SetActive(true);
	}

	static public void TurnPause ()
	{
		Init();
		curGameState = State.PAUSE;
	}

	static public void TurnIntro ()
	{
		Init();
		curGameState = State.INTRO;
		//Camera.main.GetComponent<CameraMove>().RunIntro();
	}

	static public void TurnStart ()
	{
		Init();
		curGameState = State.START;
		gameState.startScreen.SetActive(true);
	}

	static void Init()
	{
		gameState.sisyphus.SetPlayable(false);
		//boulder.SetPlayable(false);
		gameState.upgradeScreen.SetActive(false);
		gameState.pauseScreen.SetActive(false);
		gameState.nightScreen.SetActive(false);
		gameState.startScreen.SetActive(false);
		gameState.gameScreen.SetActive(false);
//		Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
	}

	public void UpgradeMenu(bool open) {
		if (open) {
			TurnUpgrade();
		} else {
			TurnNight();
		}
	}

	public void NextDay() {
		GameState.day++;
		TurnDay();
	}
}