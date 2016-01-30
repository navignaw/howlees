using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {
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

	public Sisyphus sisyphus;
	public GameObject pauseScreen;
	public GameObject upgradeScreen;
	public GameObject startScreen;
	public GameObject gameScreen;

	State curGameState = State.START;
	//Boulder boulder;

	// Use this for initialization
	void Start () {
		TurnDay();
	}

	// Update is called once per frame
	void Update () {
		switch (curGameState)
		{
			case State.NIGHT:
				break;
			case State.DAY:
				break;
			case State.UPGRADE:
				break;
			case State.PAUSE:
				break;
			case State.INTRO:
				break;
		}
	}

	void TurnNight ()
	{
		Init();
		curGameState = State.NIGHT;
	}

	void TurnDay ()
	{
		Init();
		GameState.day++;
		curGameState = State.DAY;
		gameScreen.SetActive(true);
		sisyphus.SetPlayable(true);
		//boulder.SetPlayable(true);
	}

	void TurnUpgrade ()
	{
		Init();
		curGameState = State.UPGRADE;
		upgradeScreen.SetActive(true);
	}

	public void TurnPause ()
	{
		Init();
		curGameState = State.PAUSE;
	}

	public void TurnIntro ()
	{
		Init();
		curGameState = State.INTRO;
		//Camera.main.GetComponent<CameraMove>().RunIntro();
	}

	public void TurnStart ()
	{
		Init();
		curGameState = State.START;
		startScreen.SetActive(true);
	}

	void Init()
	{
		sisyphus.SetPlayable(false);
		//boulder.SetPlayable(false);
		upgradeScreen.SetActive(false);
		pauseScreen.SetActive(false);
		startScreen.SetActive(false);
		gameScreen.SetActive(false);
	}
}
