using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Trail : MonoBehaviour {
    public SpriteRenderer spriteRenderer;
    public float opacitySpeed = 0.2f;

    public bool UpdateOpacity() {
        Color newColor = spriteRenderer.color;
        newColor.a = Mathf.Clamp01(newColor.a - opacitySpeed);
        spriteRenderer.color = newColor;

        if (newColor.a == 0) {
            return true;
        }
        return false;
    }

}
