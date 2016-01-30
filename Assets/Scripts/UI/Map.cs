using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Map : MonoBehaviour {
    public float maxHeight = 500f;
    public float maxDepth = 150f;
    public Transform ground;

    public RectTransform boulderTransform;
    public RectTransform highestTransform;

    private float startZ;

    void Start () {
        startZ = ground.position.z;
    }

    void Update () {
        Vector2 newPos = boulderTransform.localPosition;
        newPos.y = Mathf.Clamp(0f, ((startZ - ground.position.z) / maxDepth) * maxHeight, maxHeight);
        boulderTransform.localPosition = newPos;

        if (newPos.y > highestTransform.localPosition.y) {
            highestTransform.localPosition = newPos;
        }
    }

}
