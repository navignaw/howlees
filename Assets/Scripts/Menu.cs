using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {
	public int escapeSceneChange = 0; // -1 for quit

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (escapeSceneChange == -1) {
				Application.Quit();
			} else {
				SceneManager.LoadScene(escapeSceneChange);
			}
		}
	}

	public void LoadScene(string scene) {
		SceneManager.LoadScene(scene);
	}

	public void QuitGame() {
		Application.Quit();
	}

}
