using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour {

	AudioSource audio;
	float rockSpeed;
	float boulderSpeed;
	float curPitch;
	float startRollingTime;
	bool wasRolling;
	public Sisyphus sisyphus;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		boulderSpeed = sisyphus.boulderSpeed;
		rockSpeed = GetComponent<Rigidbody>().velocity.magnitude + boulderSpeed;
		//print (sisyphus.boulderSpeed);
		if (wasRolling)
		{
			rockSpeed += 2;
		}
		if (sisyphus.boulderSpeed > 0.15f)
		{
			startRollingTime = Time.time;
			wasRolling = true;
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
