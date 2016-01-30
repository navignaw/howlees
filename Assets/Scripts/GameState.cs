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

	static float dayTimeScale = 10;
	static float nightTimeScale = 100;
	static float timeScale = 10;

	static Color morningSun = new Color(127, 255, 230);
	static Color noonSun = new Color(254, 134, 112);
	static Color nightSun = new Color(101, 91, 194);
	static Color sunColor;

	public Sisyphus sisyphus;
	public GameObject sun;
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
		sun.transform.eulerAngles = new Vector3 (0,180,time);
		sunColor = ColorMap(sunColor, 0, 90, morningSun, noonSun);
		print(sunColor);
		sun.GetComponentInChildren<SpriteRenderer>().color = sunColor;


		switch (curGameState)
		{
			case State.NIGHT:
			{
				/* speed up to sundown */
				if (time < 270)	time += timeScale * Time.deltaTime;
			}
				break;
			case State.DAY:
			{
				time += timeScale * Time.deltaTime;
				if (time >= 180) TurnNight();
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
	}

	static public void TurnDay ()
	{
		Init();
		GameState.day++;
		curGameState = State.DAY;
		timeScale = dayTimeScale;
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

	Color ColorMap (Color value, float fromSource, float toSource, Color fromTarget, Color toTarget)
	{
		value.r = (value.r - fromSource) / (toSource - fromSource) * (toTarget.r - fromTarget.r);
		value.g = (value.g - fromSource) / (toSource - fromSource) * (toTarget.g - fromTarget.g);
		value.b = (value.b - fromSource) / (toSource - fromSource) * (toTarget.b - fromTarget.b);
		return value;
	}
}