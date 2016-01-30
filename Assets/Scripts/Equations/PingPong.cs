using UnityEngine;
using System.Collections;

[AddComponentMenu("Equation/PingPong")]
public class PingPong : Sine {

	public override float f (float x) {
		return Mathf.PingPong((x / Period + PhaseOffset) * Amplitude, Amplitude);
	}
}