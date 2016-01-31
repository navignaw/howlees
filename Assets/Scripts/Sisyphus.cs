using UnityEngine;
using System.Collections;

public class Sisyphus : MonoBehaviour {
    public Rigidbody boulder;
    public Transform ground;
    public Vector2 moveBounds; // how far player can move horizontally from starting pos
    public Vector2 moveCursorZone; // safe zone in which you can move cursor without affecting movement
    public float hillSlope;
    public float maxStrength = 10f;
    public float energy;
    public float speed = 1f;
    public float energyDepleteRate = 2f;
    public float energyGainRate = 0.5f;
    public float maxHorizontalForce = 1f;

    const float loseDistance = 1f;

    private Rigidbody rb;
    private Vector3 hillDirection;
    private Vector3 mouseStart;
    private Vector3 startPos;
    private Vector3 boulderStartPos;
    private Vector3 groundStartPos;
    private Quaternion startRot;
    private Animator anim;

    private bool active = false;

    void Awake () {
        rb = GetComponent<Rigidbody>();
        rb.Sleep();
        boulder.Sleep();
        startPos = transform.position;
        startRot = transform.rotation;
        boulderStartPos = boulder.transform.position;
        groundStartPos = ground.position;
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        hillDirection = Vector3.RotateTowards(Vector3.forward, Vector3.up, hillSlope * Mathf.PI / 180f, 0f);
        energy = maxStrength;
    }

    // Update is called once per frame
    void Update () {
        if (!active) {
            return;
        }

        // click start
        if (Input.GetMouseButtonDown(0)) {
            mouseStart = Input.mousePosition;
            rb.constraints |= RigidbodyConstraints.FreezeRotationY;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        }

        // holding mouse button (pushing)
        if (Input.GetMouseButton(0)) {
            // start idle push animation

            Vector3 mouseOffset = Input.mousePosition - mouseStart;
            Vector3 mouseForce = new Vector3(energy * mouseOffset.x / Screen.width, energy * mouseOffset.y / Screen.height, 0f);
            rb.velocity = Vector3.Project(mouseForce, transform.forward);

            // Move ground towards camera
            ground.Translate(Vector3.back * Mathf.Max(mouseForce.y, 0f) * Time.deltaTime * 0.5f);

            // Move boulder and player's x position
            Vector3 pushForce = mouseForce;
            pushForce *= 20f;
            pushForce.x = Mathf.Clamp(-mouseForce.x, -maxHorizontalForce, maxHorizontalForce);
            Debug.Log(pushForce);
            boulder.transform.RotateAround(boulder.transform.position, Vector3.right, pushForce.y * Time.deltaTime);

            // lose energy while pushing
            if (mouseOffset.y > moveCursorZone.y) {
                energy = Mathf.Max(0f, energy - energyDepleteRate * Time.deltaTime);
            }
        } else {
            // not holding mouse button (moving)
            Vector3 mouseOffset = Input.mousePosition - new Vector3(Screen.width / 2, 0f, 0f);
            if (Mathf.Abs(mouseOffset.x) > moveCursorZone.x) {
                float angle = Time.deltaTime * (-speed * mouseOffset.x / Screen.width);
                transform.RotateAround(boulder.transform.position, Vector3.up, angle);
            }

            energy = Mathf.Min(maxStrength, energy + energyGainRate * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0)) {
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
            rb.constraints |= RigidbodyConstraints.FreezePositionX;
            rb.velocity = Vector3.zero;
        }

        // Check boulder distance
        if (boulder.transform.position.z > transform.position.z) {
            GameState.bestDistance = Mathf.Max(GameState.bestDistance, groundStartPos.z - ground.position.z);
        } else if ((boulder.transform.position - transform.position).sqrMagnitude >= loseDistance) {
			GameState.TurnNight();
            SetPlayable(false);
        }
    }

    public void SetPlayable(bool playable) {
        if (playable && !active) {
            rb.transform.position = startPos;
            rb.transform.rotation = startRot;
            boulder.transform.position = boulderStartPos;
            ground.position = groundStartPos;
            StopRigidbody(rb);
            StopRigidbody(boulder);
            rb.WakeUp();
            boulder.WakeUp();
            energy = maxStrength;
        }
        active = playable;
    }

    void StopRigidbody(Rigidbody rb) {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
    }

}
