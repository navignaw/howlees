using UnityEngine;
using System.Collections;

public class LightingColors : MonoBehaviour {

	SpriteRenderer spr;
	Light _light;
	Camera _camera;

	// Use this for initialization
	void Start () {
		spr = GameObject.FindWithTag("Sun").GetComponent<SpriteRenderer>();
		_light = GetComponent<Light>();
		_camera = Camera.main.GetComponent<Camera>();
		_camera.clearFlags = CameraClearFlags.SolidColor;
	}

	// Update is called once per frame
	void Update () {
		_light.color = spr.color;
		_camera.backgroundColor = spr.color - new Color(0.05f, 0.05f, 0.05f);

		float red = _camera.backgroundColor.r;
		float green = _camera.backgroundColor.g;
		float blue = _camera.backgroundColor.b;
		float gray = 0.5f;

		_camera.backgroundColor = new Color ((red + gray) / 2f, (green + gray) / 2f, (blue + gray) / 2f);
	}
}
