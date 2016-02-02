using UnityEngine;
using System.Collections;

public class RandomAnimFrame : MonoBehaviour {
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.SetFloat("offset", Random.Range(0f, 1f));
	}

	// Update is called once per frame
	void Update () {
	}
}
