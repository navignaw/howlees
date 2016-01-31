﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	public void StartGame() {
		SceneManager.LoadScene("Mountain");
	}

	public void QuitGame() {
		Application.Quit();
	}

	public void CreditGame() {
		SceneManager.LoadScene("Credit");
	}


}
