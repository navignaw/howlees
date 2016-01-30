using UnityEngine;
using System.Collections;

public class Sisyphus : MonoBehaviour {
    public Rigidbody boulder;
    public float hillSlope;
    public float strength;

    public float bestDistance = 0f;

    private Rigidbody rb;
    private Vector3 hillDirection;
    private Vector3 mouseStart;

    // Use this for initialization
    void Start () {
        hillDirection = Vector3.RotateTowards(Vector3.forward, Vector3.up, hillSlope * Mathf.PI / 180f, 0f);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        // click start
        if (Input.GetMouseButtonDown(0)) {
            mouseStart = Input.mousePosition;
        }

        // holding mouse button
        if (Input.GetMouseButton(0)) {
            Vector3 mouseOffset = Input.mousePosition - mouseStart;
            Vector3 mouseForce = new Vector3(strength * mouseOffset.x / Screen.width, strength * mouseOffset.y / Screen.height, 0f);
            Vector3 pushForce = Vector3.Project(mouseForce, hillDirection);
            rb.velocity = pushForce;

            pushForce.x = -mouseForce.x;
            pushForce *= 20f;
            boulder.AddForce(pushForce);
        }

        if (Input.GetMouseButtonUp(0)) {
            rb.velocity = Vector3.zero;
        }

        bestDistance = Mathf.Max(bestDistance, boulder.transform.position.z);
    }

}
