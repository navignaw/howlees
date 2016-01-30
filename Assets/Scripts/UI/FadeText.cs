using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeText : MonoBehaviour {
    public float animateSpeed = 0.5f;

    private Text text;
    private bool fadingIn = true;
    private bool fading = true;

    // Use this for initialization
    void Awake () {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        if (!fading) {
            return;
        }

        // fade opacity
        Color newColor = text.color;
        newColor.a = Mathf.Clamp01(newColor.a + animateSpeed * Time.deltaTime * (fadingIn ? 1 : -1));
        text.color = newColor;

        if (fadingIn && newColor.a >= 0.99f) {
            fadingIn = false;
        } else if (!fadingIn && newColor.a <= 0.01f) {
            fading = false;
        }
    }

    void OnEnable() {
        Color newColor = text.color;
        newColor.a = 0;
        text.color = newColor;
        fading = true;
        fadingIn = true;
    }

}
