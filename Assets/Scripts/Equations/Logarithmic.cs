using UnityEngine;
using System.Collections;

[AddComponentMenu("Equation/Logarithmic")]
public class Logarithmic : Equation {
    public float logBase = 2;

	public override float f (float x) {
		return Mathf.Log(x, logBase);
	}
}
