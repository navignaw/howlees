using UnityEngine;
using System.Collections;

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

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		startRot = trail.transform.rotation;
		prevTrailTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		boulderSpeed = sisyphus.boulderSpeed;
		rockSpeed = GetComponent<Rigidbody>().velocity.magnitude + boulderSpeed;

		print (sisyphus.boulderSpeed);
		if (wasRolling)
		{
			rockSpeed += 2;
		}
		if (sisyphus.boulderSpeed > 0.15f)
		{
			startRollingTime = Time.time;
			wasRolling = true;

			if ((Time.time - prevTrailTime) > 0.1f)
			{
				GameObject thisTrail = Instantiate(trail, new Vector3(transform.position.x, 0.4f, transform.position.z + 0.3f), startRot) as GameObject;
				thisTrail.transform.parent = hill.transform;
				prevTrailTime = Time.time;
			}
		}
		if (boulderSpeed <= 0.15f && Time.time - startRollingTime > 0.3f)
		{
			rockSpeed += 0;
			wasRolling = false;
		}
		curPitch = Mathf.Lerp(curPitch, Mathf.Clamp(rockSpeed,0,4), 0.4f);
		audio.volume = curPitch / 2;
//		audio.pitch = curPitch;
	}
}
