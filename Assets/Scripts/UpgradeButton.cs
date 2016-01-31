using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeButton : MonoBehaviour {
    public enum ButtonType {
        AESTHETIC,
        STRENGTH,
        STAMINA,
        TRACTION
    }

    public Equation costEquation;
    public ButtonType type;

    private Button button;
    private int cost;

    // Use this for initialization
    void Awake () {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update () {
    }

    public void UpdateCost() {
        switch (type) {
            case ButtonType.AESTHETIC:
                cost = (int) costEquation.eval(Upgrades.aesthetics);
                break;

            case ButtonType.STRENGTH:
                cost = (int) costEquation.eval(Upgrades.strengthUpgrade);
                break;

            case ButtonType.STAMINA:
                cost = (int) costEquation.eval(Upgrades.staminaUpgrade);
                break;

            case ButtonType.TRACTION:
                cost = (int) costEquation.eval(Upgrades.tractionUpgrade);
                break;
        }
        button.interactable = GameState.karma > cost;
        // TODO: update text
    }

}
