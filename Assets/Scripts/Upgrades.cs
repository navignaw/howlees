using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {
    public Sisyphus sisyphus;
    public Equation strengthFormula; // how fast strength grows
    public Equation staminaFormula;  // how fast stamina grows
    public Equation tractionFormula; // how fast traction grows

    public static int aesthetics = 0;
    public static int strengthUpgrade = 0;
    public static int staminaUpgrade = 0;
    public static int tractionUpgrade = 0;


    // Use this for initialization
    void Start () {
        UpdateUpgrades();
    }

    void OnEnable() {
        UpdateUpgrades();
    }

    public void UpdateUpgrades() {
        sisyphus.maxStrength = strengthFormula.eval(strengthUpgrade);
        sisyphus.energyDepleteRate = staminaFormula.eval(staminaUpgrade);
        sisyphus.traction = tractionFormula.eval(tractionUpgrade);

        foreach (UpgradeButton button in GetComponentsInChildren<UpgradeButton>()) {
            button.UpdateCost();
        }
        foreach (ValueText valueText in GetComponentsInChildren<ValueText>()) {
            valueText.SetText();
        }
    }

    public void BuyAestheticUpgrade() {
        aesthetics++;
        // TODO: stuff
        UpdateUpgrades();
    }

    public void BuyStrengthUpgrade() {
        strengthUpgrade++;
        UpdateUpgrades();
    }

    public void BuyStaminaUpgrade() {
        staminaUpgrade++;
        UpdateUpgrades();
    }

    public void BuyTractionUpgrade() {
        tractionUpgrade++;
        UpdateUpgrades();
    }

}
