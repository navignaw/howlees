using UnityEngine;
using System.Collections;

public class LightingColors : MonoBehaviour {

	SpriteRenderer spr;

	Camera camera;

	// Use this for initialization
	void Start () {
		spr = GameObject.FindWithTag ("Sun").GetComponent<SpriteRenderer> ();
		camera = Camera.main.GetComponent<Camera> ();
		camera.clearFlags = CameraClearFlags.SolidColor;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Light> ().color = spr.color;
		//camera.backgroundColor = new Color(spr.color - 
		camera.backgroundColor = spr.color - new Color(0.05f,0.05f,0.05f);


		float red = camera.backgroundColor.r;
		float green = camera.backgroundColor.g;
		float blue = camera.backgroundColor.b;
		float gray = 0.5f;

		camera.backgroundColor = new Color ((red + gray) / 2f, (green + gray) / 2f, (blue + gray) / 2f);


		//camera.backgroundColor = spr.color - new Color(0.08f,0.08f,0.08f,-0.08f);
		//camera.backgroundColor += spr.color
		print (camera.backgroundColor);
		//camera.backgroundColor = new Color(spr.color.r-10.0f, spr.color.g-10.0f, spr.color.b-10.0f);
	
	}
}
