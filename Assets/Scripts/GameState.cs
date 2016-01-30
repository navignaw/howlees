using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {
	const int NIGHT = 0;
	const int DAY = 1;
	const int UPGRADE = 2;
	const int PAUSE = 3;
	const int INTRO = 4;
	const int START = 5;
	int curGameState;
	Boulder boulder;
	Sisyphus sisyphus;
	GameObject pauseScreen;
	GameObject upgradeScreen;
	GameObject startScreen;

	// Use this for initialization
	void Start () {
		TurnIntro();
		sisyphus = GameObject.Find("Sisyphus").GetComponent<Sisyphus>();
		boulder = GameObject.Find("Boulder").GetComponent<Boulder>();
		pauseScreen = GameObject.Find("PauseScreen");
		upgradeScreen = GameObject.Find("UpgradeScreen");
		startScreen = GameObject.Find("StartScreen");
	}
	
	// Update is called once per frame
	void Update () {		
		switch (curGameState)
		{
			case NIGHT:
				break;
			case DAY:
				break;
			case UPGRADE:
				break;
			case PAUSE:
				break;
			case INTRO:
				break;
		}
	}

	void TurnNight ()
	{
		Init();
		curGameState = NIGHT;
	}	

	void TurnDay ()
	{
		Init();
		curGameState = DAY;
		sisyphus.SetPlayable(true);
		boulder.SetPlayable(true);
	}

	void TurnUpgrade ()
	{
		Init();
		curGameState = UPGRADE;
		upgradeScreen.SetActive(true);
	}

	public void TurnPause ()
	{
		Init();
		curGameState = PAUSE;
	}	

	public void TurnIntro ()
	{
		Init();
		curGameState = INTRO;
		Camera.main.GetComponent<CameraMove>().RunIntro();
	}

	public void TurnStart ()
	{
		Init();
		curGameState = START;
		startScreen.SetActive(true);
	}

	void Init()
	{
		sisyphus.SetPlayable(false);
		boulder.SetPlayable(false);
		upgradeScreen.SetActive(false);
		pauseScreen.SetActive(false);
		startScreen.SetActive(false);
	}
}
