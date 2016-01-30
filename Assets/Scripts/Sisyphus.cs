using UnityEngine;
using System.Collections;

public class Sisyphus : MonoBehaviour {
    public Rigidbody boulder;
    public float hillSlope;
    public float strength;

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
            float horizontalSpeed = Input.GetAxis("Mouse X");
            float verticalSpeed = Input.GetAxis("Mouse Y");

            Vector3 mouseForce = (Input.mousePosition - mouseStart) / Screen.height * strength;
            //Debug.Log(mouseForce);
            Vector3 pushForce = Vector3.Project(mouseForce, hillDirection);
            //boulder.AddForce(pushForce * 500f);
            rb.velocity = pushForce;
            //Debug.Log(pushForce);
        }

        if (Input.GetMouseButtonUp(0)) {
            mouseStart = Vector3.zero;
        }
    }

}
