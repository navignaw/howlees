using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour {
	public Vector3 speed;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		transform.position += Time.deltaTime * new Vector3(
			Random.Range(0f, speed.x * 2) - speed.x,
			Random.Range(0f, speed.y * 2) - speed.y,
			Random.Range(0f, speed.z * 2) - speed.z);
	}
}
