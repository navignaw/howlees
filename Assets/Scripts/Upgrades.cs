using UnityEngine;
using System;
using System.Collections;

public class Upgrades : MonoBehaviour {
    public Sisyphus sisyphus;
    public Equation strengthFormula; // how fast strength grows
    public Equation staminaFormula;  // how fast stamina grows
    public Equation tractionFormula; // how fast traction grows

    public AudioSource music;

    public static int aesthetics = 0;
    public static int strengthUpgrade = 0;
    public static int staminaUpgrade = 0;
    public static int tractionUpgrade = 0;

    /**
     * Delegate and events. A client can subscribe to the event with
     * Upgrades.Listener += new Upgrades.AestheticHandler(OnAestheticChanged)
     */
    public delegate void AestheticHandler(object sender, EventArgs e);
    public static event AestheticHandler Listener;

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
            GameState.karma = 99999;
            UpdateUpgrades();
        }
#endif
    }

    public void UpdateUpgrades() {
        sisyphus.strength = strengthFormula.eval(strengthUpgrade);
        sisyphus.maxEnergy = staminaFormula.eval(staminaUpgrade);
        //sisyphus.traction = tractionFormula.eval(tractionUpgrade);

        foreach (UpgradeButton button in GetComponentsInChildren<UpgradeButton>()) {
            button.UpdateCost();
        }
        foreach (ValueText valueText in GetComponentsInChildren<ValueText>()) {
            valueText.SetText();
        }
    }

    public void BuyAestheticUpgrade() {
        if (aesthetics == 0) {
            music.Play();  // first upgrade includes music!
        }

        aesthetics++;
        // Send message to all listeners, prompting an aesthetic check.
        if (Listener != null) {
            Listener(this, EventArgs.Empty);
        }

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
