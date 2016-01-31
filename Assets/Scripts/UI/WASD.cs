using UnityEngine;
using System.Collections;

public class WASD : MonoBehaviour {
    public float animateSpeed = 0.5f;

    private SpriteRenderer spriteRenderer;
    private bool fading = false;

    // Use this for initialization
    void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        if (!fading && Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            fading = true;
        }

        // fade opacity
        if (fading) {
            Color newColor = spriteRenderer.color;
            newColor.a = Mathf.Clamp01(newColor.a - animateSpeed * Time.deltaTime);
            spriteRenderer.color = newColor;

            if (newColor.a <= 0.01f) {
                Destroy(gameObject);
            }
        }
    }

}
