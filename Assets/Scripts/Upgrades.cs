using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {
    public Sisyphus sisyphus;
    public Equation strengthFormula; // how fast strength grows
    public Equation staminaFormula;  // how fast stamina grows
    public Equation tractionFormula; // how fast traction grows

    public AudioSource music;
    public GameObject[] aestheticObjects;

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

    void Update() {
#if UNITY_EDITOR
        // Cheat for getting karma
        if (Input.GetKeyDown("k")) {
            GameState.karma = 1000;
            UpdateUpgrades();
        }
#endif
    }

    public void UpdateUpgrades() {
        sisyphus.maxStrength = strengthFormula.eval(strengthUpgrade);
        sisyphus.energyGainRate = staminaFormula.eval(staminaUpgrade);
        sisyphus.traction = tractionFormula.eval(tractionUpgrade);

        foreach (UpgradeButton button in GetComponentsInChildren<UpgradeButton>()) {
            button.UpdateCost();
        }
        foreach (ValueText valueText in GetComponentsInChildren<ValueText>()) {
            valueText.SetText();
        }
    }

    public void BuyAestheticUpgrade() {
        if (aesthetics == 0) {
            music.Play();
        }

        if (aesthetics < aestheticObjects.Length) {
            aestheticObjects[aesthetics].SetActive(true);
        }

        aesthetics++;
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
