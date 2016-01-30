using UnityEngine;
using System.Collections;

[AddComponentMenu("Equation/Sine")]
public class Sine : Equation {
	public float Amplitude;
	public float Period;
	public float PhaseOffset;

	public override float f (float x) {
		return Amplitude * Mathf.Sin(2 * Mathf.PI * x / Period + PhaseOffset);
	}
}
