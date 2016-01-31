using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {
	public static GameState gameState;
	public enum State {
		NIGHT,
		DAY,
		DIARY,
		INTRO,
		START
	}

	// Score and records
	public static int day = 0;
	public static float todaysBest = 0f;
	public static float bestDistance = 0f;
	public static int karma = 0;
	public static float time = 90;
	public static float morning = 90;
	public static float noon = 180;
	public static float night = 270;
	public static float midnight = 0;

	static float dayTimeScale = 5;
	static float nightTimeScale = 50;
	static float timeScale = 10;

	public Sisyphus sisyphus;
	public Sun sun;
	public Boulder boulder;
	public GameObject diaryScreen;
	public GameObject upgradeScreen;
	public GameObject nightScreen;
	public GameObject startScreen;
	public GameObject gameScreen;
	public GameObject nightCamera;
	public Texture2D defaultCursor;
	public Texture2D pushCursor;

	static State curGameState = State.START;

	// Use this for initialization
	void Start () {
		gameState = this;
		time = morning;
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
				if (time < night && time >= morning)
				{
					Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
					time += nightTimeScale * Time.deltaTime;
				}
				else if (time != midnight)
					time = midnight;
			}
				break;
			case State.DAY:
			{
				time += timeScale * Time.deltaTime;
				if (time < morning)
					time += nightTimeScale * Time.deltaTime;
				if (Input.GetMouseButtonDown(0))
					Cursor.SetCursor(pushCursor, Vector2.zero, CursorMode.Auto);
				else if (Input.GetMouseButtonUp(0))
					Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
				if (time >= night) {
					Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
					TurnNight();
				}
			}
				break;
			case State.DIARY:
				break;
			case State.INTRO:
				break;
		}
	}

	static public void TurnNight ()
	{
		Init();
		bestDistance = Mathf.Max(todaysBest, bestDistance);
		EarnKarma();
		curGameState = State.NIGHT;
		timeScale = nightTimeScale;
		gameState.nightCamera.SetActive(true);
		gameState.nightScreen.SetActive(true);
	}

	static public void TurnDay ()
	{
		Init();
		todaysBest = 0;
		curGameState = State.DAY;
		if (time > morning) time = midnight;
		timeScale = dayTimeScale;
		gameState.gameScreen.SetActive(true);
		gameState.sisyphus.SetPlayable(true);
		gameState.boulder.SetPlayable(true);
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
		gameState.boulder.SetPlayable(false);
		gameState.upgradeScreen.SetActive(false);
		gameState.diaryScreen.SetActive(false);
		gameState.nightScreen.SetActive(false);
		gameState.startScreen.SetActive(false);
		gameState.gameScreen.SetActive(false);
	}

	public static void EarnKarma() {
		karma += (int) todaysBest / 2;
	}

	public void UpgradeMenu(bool open) {
		if (open) {
			gameState.upgradeScreen.SetActive(true);
			gameState.nightScreen.SetActive(false);
		} else {
			gameState.upgradeScreen.SetActive(false);
			gameState.nightScreen.SetActive(true);
		}
	}

	public void DiaryMenu(bool open) {
		if (open) {
			gameState.diaryScreen.SetActive(true);
			gameState.nightScreen.SetActive(false);
		} else {
			gameState.diaryScreen.SetActive(false);
			gameState.nightScreen.SetActive(true);
		}
	}

	public void NextDay() {
		GameState.day++;
		TurnDay();
		nightCamera.SetActive(false);
	}
}