using UnityEngine;
using System.Collections;

// f(x) = eval(x)
[AddComponentMenu("Equation/Identity")]
public class EquationCost : MonoBehaviour {
#if UNITY_EDITOR
    public string Name;
#endif

	public float Scale = 1.0f;
	public Vector2 Offset;

	public float eval(float x) {
		return Scale * f (x + Offset.x) + Offset.y;
	}

	public virtual float f(float x) {
		return Mathf.Pow(2, x);
	}    

}
