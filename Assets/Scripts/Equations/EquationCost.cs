using UnityEngine;
using System.Collections;

// f(x) = 2^x
[AddComponentMenu("Equation/EquationCost")]
public class EquationCost : Equation {

	public override float f(float x) {
		return Mathf.Pow(2, x);
	}

}
