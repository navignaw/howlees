﻿using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {
    public Sisyphus sisyphus;
    public Equation strengthFormula; // how strength grows

    public static int strengthUpgrade = 0;
    public static int flowers = 0;


    // Use this for initialization
    void Start () {
        UpdateUpgrades();
    }

    // Update is called once per frame
    void Update () {

    }

    public void UpdateUpgrades() {
        sisyphus.maxStrength = strengthFormula.eval(strengthUpgrade);
    }


}