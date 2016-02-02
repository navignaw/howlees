using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnergyBar : MonoBehaviour {
    public float maxHeight = 300f;
    public Sisyphus sisyphus;

    private RectTransform rectTransform;

    void Start () {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update () {
        Vector2 newSize = rectTransform.sizeDelta;
        newSize.y = (sisyphus.energy / sisyphus.maxEnergy) * maxHeight;
        rectTransform.sizeDelta = newSize;
    }

}
