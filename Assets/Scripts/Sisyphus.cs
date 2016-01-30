using UnityEngine;
using System.Collections;

public class Sisyphus : MonoBehaviour {
    public Rigidbody boulder;
    public float hillSlope;
    public float maxStrength;
    public float energy;
    public float energyDepleteRate = 2f;
    public float energyGainRate = 0.5f;

    const float loseDistance = 1f;

    private Rigidbody rb;
    private Vector3 hillDirection;
    private Vector3 mouseStart;
    private Vector3 startPos;
    private Vector3 boulderStartPos;
    private bool active = false;

    void Awake () {
        rb = GetComponent<Rigidbody>();
        rb.Sleep();
        boulder.Sleep();
        startPos = transform.position;
        boulderStartPos = boulder.transform.position;
    }

    // Use this for initialization
    void Start () {
        hillDirection = Vector3.RotateTowards(Vector3.forward, Vector3.up, hillSlope * Mathf.PI / 180f, 0f);
    }

    // Update is called once per frame
    void Update () {
        if (!active) {
            return;
        }

        // click start
        if (Input.GetMouseButtonDown(0)) {
            mouseStart = Input.mousePosition;
        }

        // holding mouse button
        if (Input.GetMouseButton(0)) {
            Vector3 mouseOffset = Input.mousePosition - mouseStart;
            Vector3 mouseForce = new Vector3(energy * mouseOffset.x / Screen.width, energy * mouseOffset.y / Screen.height, 0f);
            Vector3 pushForce = Vector3.Project(mouseForce, hillDirection);
            rb.velocity = pushForce;

            pushForce.x = -mouseForce.x;
            pushForce *= 20f;
            boulder.AddForce(pushForce);

            // lose energy while pushing
            energy = Mathf.Max(0f, energy - energyDepleteRate * Time.deltaTime);
        } else {
            energy = Mathf.Min(maxStrength, energy + energyGainRate * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0)) {
            rb.velocity = Vector3.zero;
        }

        // Check boulder distance
        if (boulder.transform.position.z > transform.position.z) {
            GameState.bestDistance = Mathf.Max(GameState.bestDistance, boulder.transform.position.z - boulderStartPos.z);
        } else if ((boulder.transform.position - transform.position).sqrMagnitude >= loseDistance) {
			GameState.TurnNight();
            SetPlayable(false);
        } else {
        }
    }

    public void SetPlayable(bool playable) {
        if (playable && !active) {
            rb.transform.position = startPos;
            boulder.transform.position = boulderStartPos;
            rb.velocity = Vector3.zero;
            boulder.velocity = Vector3.zero;
            rb.WakeUp();
            boulder.WakeUp();
            energy = maxStrength;
        }
        active = playable;
    }

}
