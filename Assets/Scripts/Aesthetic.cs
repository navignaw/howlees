using UnityEngine;
using System;
using System.Collections;

public class Aesthetic : MonoBehaviour {
    public int unlockAt;

    // Use this for initialization
    void Awake () {
        if (Upgrades.aesthetics < unlockAt) {
            gameObject.SetActive(false);
            // Subscribe to upgrade events.
            Upgrades.Listener += new Upgrades.AestheticHandler(OnAestheticChanged);
        }
    }

    private void OnAestheticChanged(object sender, EventArgs e) {
        if (Upgrades.aesthetics >= unlockAt) {
            gameObject.SetActive(true);
            // Unsubscribe from upgrade event.
            Upgrades.Listener -= new Upgrades.AestheticHandler(OnAestheticChanged);
        }
    }
}
