using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ValueText : MonoBehaviour {
    public enum TextType {
        DAY,
        BEST_DISTANCE,
        KARMA,
    }

    public TextType type;
    public string prefix;
    public string suffix;

    private Text text;

    // set the text to appropriate value
    void Start() {
        string value;
        switch (type) {
            case TextType.DAY:
                value = GameState.day.ToString();
                break;

            case TextType.BEST_DISTANCE:
                value = GameState.bestDistance.ToString();
                break;

            case TextType.KARMA:
                value = GameState.karma.ToString();
                break;

            default:
                value = string.Empty;
                break;
        };

        text = GetComponent<Text>();
        text.text = prefix + value + suffix;
    }

}
