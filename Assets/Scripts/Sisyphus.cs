using UnityEngine;
using System.Collections;

public class Sisyphus : MonoBehaviour {
    public Rigidbody boulder;
    public Transform ground;
    public Transform objectTransform;
    public Vector2 moveBounds; // how far player can move horizontally from starting pos
    public float hillSlope;
    public float maxStrength = 10f;
    public float energy;
    public float speed = 1f;
    public float traction = 1f;
    public float energyDepleteRate = 2f;
    public float energyGainRate = 0.5f;
    public float horizontalForce = 50f;
    public float maxRollSpeed = 50f;
    public float boulderSpeed;

    const float loseDistance = 4f;

    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 startTransPos;
    private Vector3 boulderStartPos;
    private Vector3 groundStartPos;
    private Quaternion startRot;
    private Animator anim;
    private Vector3 prevGroundPos;
    private float prevGroundTime;
    private float prevVerticalSpeed;
    private bool active = false;
    private float playDelay = 2;
    private float playStart;
    private bool delayOver;
    private bool pushing;


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
        energy = maxStrength;
    }

    // Update is called once per frame
    void Update () {
        // measure boulder speed
        if (Time.time - prevGroundTime >= 0.1f)
        {
            boulderSpeed = (ground.position - prevGroundPos).magnitude;
            prevGroundTime = Time.time;
            prevGroundPos = ground.position;
        }

        if (!delayOver && Time.time - playStart > 2.5fs)
        {
            delayOver = true;
        }
        if (!active || !delayOver) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetTrigger("push");                        
            pushing = true;
        }
        if (Input.GetKey(KeyCode.W) &&
            !pushing)
        {
            anim.SetTrigger("push");
            pushing = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetTrigger("idleRest");
            pushing = false;
        }

        float horizontalSpeed = Input.GetAxis("Horizontal");
        float verticalSpeed = Input.GetAxis("Vertical");

        // Push if pressing up
        if (verticalSpeed > 0) {
            // Move ground towards camera
            ground.Translate(Vector3.back * energy * Time.deltaTime * 0.25f);

            // Rotate boulder and push horizontally
            boulder.transform.RotateAround(boulder.transform.position, Vector3.right, Mathf.Min(energy * energy * Time.deltaTime, maxRollSpeed));
            boulder.AddForce(new Vector3((boulder.transform.position.x - objectTransform.position.x) * horizontalForce, 0f, 0f));

            // lose energy while pushing
            energy = Mathf.Max(0f, energy - Mathf.Max(energyDepleteRate, 0.1f) * Time.deltaTime);

        } else {
            ground.Translate(Vector3.forward * traction * Time.deltaTime);
            boulder.transform.RotateAround(boulder.transform.position, Vector3.left, Mathf.Min(traction * 0.25f, maxRollSpeed));
            energy = Mathf.Min(maxStrength, energy + energyGainRate * Time.deltaTime);
        }

        if (horizontalSpeed != 0) {
            horizontalSpeed *= Time.deltaTime * speed;
            Vector3 newPos = objectTransform.position;
            newPos.x = Mathf.Clamp(newPos.x + horizontalSpeed, startTransPos.x + moveBounds.x, startTransPos.x + moveBounds.y);
            objectTransform.position = newPos;
            boulder.AddForce(new Vector3(-horizontalSpeed, 15f, 100f)); // move boulder up so it doesn't slide with player
        }
        // stopped moving this frame

        prevVerticalSpeed = verticalSpeed;

        // Check boulder distance
        GameState.todaysBest = Mathf.Max(GameState.todaysBest, groundStartPos.z - ground.position.z);
        if (energy == 0 || (boulderStartPos.z - boulder.transform.position.z) >= loseDistance) {
            anim.SetTrigger("sit");
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
        playStart = Time.time;
        delayOver = false;
        pushing = false;
        active = playable;
    }

    void StopRigidbody(Rigidbody rb) {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
    }

}
