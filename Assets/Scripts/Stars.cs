using UnityEngine;
using System.Collections;

public class Stars : MonoBehaviour {
	private Animator anim;
	private bool fadedIn = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if (!fadedIn && (GameState.time >= GameState.night || GameState.time < GameState.morning)) {
			anim.SetTrigger("fadeIn");
			fadedIn = true;
		} else if (fadedIn && GameState.time >= GameState.morning && GameState.time < GameState.night) {
			anim.SetTrigger("fadeOut");
			fadedIn = false;
		}
	}

}