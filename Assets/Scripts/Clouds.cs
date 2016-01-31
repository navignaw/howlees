using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour {
	public float moveSpeed;
	public Vector2 loopBound;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		Vector3 newPos = transform.position;
		newPos.x = newPos.x >= loopBound.y ? loopBound.x : newPos.x + moveSpeed * Time.deltaTime;
		transform.position = newPos;
	}
}
