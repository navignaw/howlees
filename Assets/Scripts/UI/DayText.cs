using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DayText : FadeText {

    // set the text to day number
    protected override void UpdateText() {
        text.text = "Day " + GameState.day.ToString();
    }

}
