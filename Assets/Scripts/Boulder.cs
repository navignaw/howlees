using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boulder : MonoBehaviour {

	AudioSource audio;
	float rockSpeed;
	float boulderSpeed;
	float curPitch;
	float startRollingTime;
	bool wasRolling;
	float prevTrailTime;
	Quaternion startRot;
	public Sisyphus sisyphus;
	public GameObject trail;
	public GameObject hill;

	private Rigidbody rb;
	private List<List<Trail>> trails = new List<List<Trail>>();

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		audio = GetComponent<AudioSource>();
		startRot = trail.transform.rotation;
		prevTrailTime = 0;
	}

	// Update is called once per frame
	void Update () {
		boulderSpeed = sisyphus.boulderSpeed;
		rockSpeed = rb.velocity.magnitude + boulderSpeed;

		if (wasRolling) {
			rockSpeed += 2;
		}
		if (sisyphus.boulderSpeed > 0.15f) {
			startRollingTime = Time.time;
			wasRolling = true;

			if ((Time.time - prevTrailTime) > 0.2f) {
				GameObject thisTrail = Instantiate(trail, new Vector3(transform.position.x, 0.5f, transform.position.z + 0.3f), startRot) as GameObject;
				thisTrail.transform.parent = hill.transform;
				trails[trails.Count - 1].Add(thisTrail.GetComponent<Trail>());
				prevTrailTime = Time.time;
			}
		}
		if (boulderSpeed <= 0.15f && Time.time - startRollingTime > 0.3f) {
			rockSpeed += 0;
			wasRolling = false;
		}
		curPitch = Mathf.Lerp(curPitch, Mathf.Clamp(rockSpeed, 0, 4), 0.4f);
		audio.volume = curPitch / 2;
	}

	public void SetPlayable(bool playable) {
		if (!playable) {
			return;
		}

		bool shouldDelete = false;
		foreach (List<Trail> dayTrails in trails) {
			foreach (Trail trailObject in dayTrails) {
				if (trailObject.UpdateOpacity()) {
					Destroy(trailObject.gameObject);
					shouldDelete = true;
				}
			}
		}
		if (shouldDelete) {
			trails.RemoveAt(0);
		}

		trails.Add(new List<Trail>());
	}
}
