using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Map : MonoBehaviour {
    public float maxHeight = 500f;
    public float maxDepth = 500f;
    public Transform boulder;

    public RectTransform boulderTransform;
    public RectTransform highestTransform;

    void Start () {
    }

    void Update () {
        Vector2 newPos = boulderTransform.localPosition;
        newPos.y = Mathf.Clamp(0f, (boulder.position.z / maxDepth) * maxHeight, maxHeight);
        boulderTransform.localPosition = newPos;

        if (newPos.y > highestTransform.localPosition.y) {
            highestTransform.localPosition = newPos;
        }
    }

}
