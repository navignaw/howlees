using UnityEngine;
using System.Collections;

[AddComponentMenu("Equation/Logarithmic")]
public class Logarithmic : Equation {

	public override float f (float x) {
		return Mathf.Log(x);
	}
}
