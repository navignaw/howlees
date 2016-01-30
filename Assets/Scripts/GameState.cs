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
	public GameObject startScreen;
	public GameObject gameScreen;

	static State curGameState = State.START;
	//Boulder boulder;

	// Use this for initialization
	void Start () {
		gameState = this;
		TurnDay();
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
				if (time >= night) TurnNight();
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
	}

	static public void TurnDay ()
	{
		Init();
		GameState.day++;
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
		gameState.startScreen.SetActive(false);
		gameState.gameScreen.SetActive(false);
	}
}