using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeText : MonoBehaviour {
    public float animateSpeed = 0.5f;

    protected Text text;
    private bool fadingIn = true;

    // Use this for initialization
    void Awake () {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        // fade opacity
        Color newColor = text.color;
        newColor.a = Mathf.Clamp01(newColor.a + animateSpeed * Time.deltaTime * (fadingIn ? 1 : -1));
        text.color = newColor;

        if (fadingIn && newColor.a >= 0.99f) {
            fadingIn = false;
        } else if (!fadingIn && newColor.a <= 0.01f) {
            enabled = false;
        }
    }

    void OnEnable() {
        Color newColor = text.color;
        newColor.a = 0;
        text.color = newColor;
        fadingIn = true;
        UpdateText();
    }

    // override in children to set text
    protected virtual void UpdateText() {
    }

}
