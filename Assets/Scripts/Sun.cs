using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {
	static Color morningSun = new Color(0.498f, 1, 0.902f);
	static Color noonSun = new Color(1, 0.525f, 0.439f);
	static Color nightSun = new Color(0.396f, 0.357f, 0.760f);
	static Color sunColor = new Color(0,0,0);

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
	}

	public void Tick (float time)
	{
		this.transform.eulerAngles = new Vector3 (0,180,time);
		sunColor = ColorMap(time);
//		spriteRenderer.color = sunColor;
	}

	Color ColorMap (float value)
	{
		Color newColor = new Color(0,0,0);
		float fromSource = 90;
		float toSource = 270;
		Color fromTarget = morningSun;
		Color toTarget = nightSun;
		if (value > GameState.morning && value <= GameState.noon)
		{
			fromSource = GameState.morning;
			toSource = GameState.noon;
			fromTarget = morningSun;
			toTarget = noonSun;
		}
		if (value > GameState.noon && value <= GameState.night)
		{
			fromSource = GameState.noon;
			toSource = GameState.night;
			fromTarget = noonSun;
			toTarget = nightSun;
		}
		newColor.r = (value - fromSource) / (toSource - fromSource) * (toTarget.r - fromTarget.r) + fromTarget.r;
		newColor.g = (value - fromSource) / (toSource - fromSource) * (toTarget.g - fromTarget.g) + fromTarget.g;
		newColor.b = (value - fromSource) / (toSource - fromSource) * (toTarget.b - fromTarget.b) + fromTarget.b;
		return newColor;
	}
}