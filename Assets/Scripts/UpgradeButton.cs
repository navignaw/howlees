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

    public Text costText;
    public Equation costEquation;
    public ButtonType type;

    private Button button;
    private int tallies;
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
                tallies = Upgrades.aesthetics;
                cost = (int) costEquation.eval(Upgrades.aesthetics);
                break;

            case ButtonType.STRENGTH:
                tallies = Upgrades.strengthUpgrade;
                cost = (int) costEquation.eval(Upgrades.strengthUpgrade);
                break;

            case ButtonType.STAMINA:
                tallies = Upgrades.staminaUpgrade;
                cost = (int) costEquation.eval(Upgrades.staminaUpgrade);
                break;

            case ButtonType.TRACTION:
                tallies = Upgrades.tractionUpgrade;
                cost = (int) costEquation.eval(Upgrades.tractionUpgrade);
                break;
        }
        if (!button) button = GetComponent<Button>();
        button.interactable = GameState.karma >= cost;
        costText.text = cost.ToString();
        DrawTallies();
    }

    public void PayCost() {
        // TODO: play blessed sound effect
        GameState.karma -= cost;
    }

    void DrawTallies() {
        // TODO: draw tallies
    }
}
