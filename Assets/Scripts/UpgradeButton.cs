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
    public GameObject[] tallies;

    private Button button;
    private int numTallies;
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
                numTallies = Upgrades.aesthetics;
                cost = (int) costEquation.eval(Upgrades.aesthetics);
                break;

            case ButtonType.STRENGTH:
                numTallies = Upgrades.strengthUpgrade;
                cost = (int) costEquation.eval(Upgrades.strengthUpgrade);
                break;

            case ButtonType.STAMINA:
                numTallies = Upgrades.staminaUpgrade;
                cost = (int) costEquation.eval(Upgrades.staminaUpgrade);
                break;

            case ButtonType.TRACTION:
                numTallies = Upgrades.tractionUpgrade;
                cost = (int) costEquation.eval(Upgrades.tractionUpgrade);
                break;
        }
        if (!button) button = GetComponent<Button>();
        button.interactable = GameState.karma >= cost;
        costText.text = cost.ToString();
        DrawTallies();
    }

    public void PayCost() {
        GameState.karma -= cost;
    }

    void DrawTallies() {
        for (int i = 0; i < Mathf.Min(numTallies, tallies.Length); i++) {
            tallies[i].SetActive(true);
        }
        for (int i = numTallies; i < tallies.Length; i++) {
            tallies[i].SetActive(false);
        }
    }
}
