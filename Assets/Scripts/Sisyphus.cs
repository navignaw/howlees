using UnityEngine;
using System.Collections;

public class Sisyphus : MonoBehaviour {
    public Rigidbody boulder;
    public Transform ground;
    public Transform objectTransform;
    public Vector2 moveBounds; // how far player can move horizontally from starting pos
    public Vector2 moveCursorZone; // safe zone in which you can move cursor without affecting movement
    public float hillSlope;
    public float maxStrength = 10f;
    public float energy;
    public float speed = 1f;
    public float traction = 1f;
    public float energyDepleteRate = 2f;
    public float energyGainRate = 0.5f;
    public float horizontalForce = 50f;
    public float boulderSpeed;
    public float maxRollSpeed = 3f;

    const float loseDistance = 4f;

    private Rigidbody rb;
    private Vector3 hillDirection;
    private Vector3 startPos;
    private Vector3 startTransPos;
    private Vector3 boulderStartPos;
    private Vector3 groundStartPos;
    private Quaternion startRot;
    private Animator anim;
    private Vector3 prevGroundPos;
    private float prevGroundTime;

    private bool active = false;

    void Awake () {
        rb = GetComponent<Rigidbody>();
        rb.Sleep();
        boulder.Sleep();
        startPos = transform.position;
        startRot = transform.rotation;
        startTransPos = objectTransform.position;
        boulderStartPos = boulder.transform.position;
        groundStartPos = ground.position;
        prevGroundPos = ground.position;

        anim = transform.GetComponentInParent<Animator>();
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

        // measure boulder speed
        if (Time.time - prevGroundTime >= 0.1f)
        {
            boulderSpeed = (ground.position - prevGroundPos).magnitude;
            prevGroundTime = Time.time;
            prevGroundPos = ground.position;
        }

        // click start
        if (Input.GetMouseButtonDown(0)) {
            // start idle push animation
            anim.SetTrigger("idlePush");
            rb.constraints |= RigidbodyConstraints.FreezePositionX;
        }

        // holding mouse button (pushing)
        Vector3 mouseOffset = Input.mousePosition - new Vector3(Screen.width / 2, 0f, 0f);
        if (Input.GetMouseButton(0)) {

            Vector2 mouseVelocity = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            if (mouseVelocity.y > 0) {
                Vector3 mouseForce = new Vector3(mouseVelocity.x, energy * mouseVelocity.y, 0f);
                rb.velocity = Vector3.Project(mouseForce, transform.forward);

                // Move ground towards camera
                ground.Translate(Vector3.back * Mathf.Max(mouseForce.y, 0f) * Time.deltaTime * 0.25f);

                // Move boulder and player's x position
                Vector3 pushForce = mouseForce;
                boulder.transform.RotateAround(boulder.transform.position, Vector3.right, Mathf.Min(energy * pushForce.y * Time.deltaTime, maxRollSpeed));
                boulder.AddForce(new Vector3(-mouseOffset.x / Screen.width * horizontalForce, 0f, 0f));

                // lose energy while pushing
                energy = Mathf.Max(0f, energy - energyDepleteRate * Time.deltaTime);

                // TODO: FIX THIS
                if (mouseVelocity.x > 0.5f) {
                    anim.SetTrigger("pushRight");
                } else if (mouseVelocity.x < -0.5f) {
                    anim.SetTrigger("pushLeft");
                } else {
                    anim.SetTrigger("pushForward");
                }
            }
        } else {
            // not holding mouse button (moving)
            float horizontalSpeed = Time.deltaTime * speed * mouseOffset.x / Screen.width;
            if (Mathf.Abs(mouseOffset.x) > moveCursorZone.x) {
                objectTransform.position += new Vector3(horizontalSpeed, 0f, 0f);
                boulder.AddForce(new Vector3(0f, 5f, 5f)); // move boulder up so it doesn't slide with player
            }

            ground.Translate(Vector3.forward * traction * Time.deltaTime);
            boulder.transform.RotateAround(boulder.transform.position, Vector3.left, Mathf.Min(traction * 0.25f, maxRollSpeed));
            energy = Mathf.Min(maxStrength, energy + energyGainRate * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0)) {
            // start idle rest animation
            anim.SetTrigger("idleRest");

            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            rb.velocity = Vector3.zero;
        }

        // Check boulder distance
        GameState.todaysBest = Mathf.Max(GameState.todaysBest, groundStartPos.z - ground.position.z);
        if (energy == 0 || (boulderStartPos.z - boulder.transform.position.z) >= loseDistance) {
			GameState.TurnNight();
            SetPlayable(false);
        }
    }

    public void SetPlayable(bool playable) {
        if (playable && !active) {
            anim.SetTrigger("idleRest");
            rb.transform.position = startPos;
            rb.transform.rotation = startRot;
            objectTransform.position = startTransPos;
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
