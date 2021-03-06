﻿using UnityEngine;
using System.Collections;

public class Sisyphus : MonoBehaviour {
    public Rigidbody boulder;
    public Transform ground;
    public Transform objectTransform;
    public Vector2 moveBounds; // how far player can move horizontally from starting pos
    public float hillSlope;
    public float maxEnergy = 10f;
    public float strength = 10f;
    public float energy;
    public float speed = 1f;
    public float traction = 1f;
    public float boulderSpeed;

    const float horizontalForce = 50f;
    const float maxRollSpeed = 1f;
    const float energyDepleteRate = 2f;
    const float energyGainRate = 0.05f;
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
    private bool active = false;
    private float playStart;
    private bool delayOver;
    private bool pushing = false;
    private int moving = 0; // -1 for left, 1 for right


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
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update () {
        // measure boulder speed
        if (Time.time - prevGroundTime >= 0.1f) {
            boulderSpeed = (ground.position - prevGroundPos).magnitude;
            prevGroundTime = Time.time;
            prevGroundPos = ground.position;
        }

        if (!delayOver && Time.time - playStart > 2.5f) {
            delayOver = true;
        }
        if (!active || !delayOver) {
            return;
        }

        bool canPush = Mathf.Abs(boulder.transform.position.x - objectTransform.position.x) <= 1f;
        float horizontalSpeed = Input.GetAxis("Horizontal");
        float verticalSpeed = Input.GetAxis("Vertical");

        // Push animations triggers
        if (verticalSpeed > 0) {
            if (!pushing && canPush) {
                anim.SetTrigger("push");
                pushing = true;
            } else if (pushing && !canPush) {
                anim.SetTrigger("idleRest");
                pushing = false;
            }
        } else if (pushing) {
            anim.SetTrigger("idleRest");
            pushing = false;
        } else if (horizontalSpeed == 0 && moving != 0) {
            anim.SetTrigger("idleRest");
            moving = 0;
        }

        // Push if pressing up
        if (verticalSpeed > 0 && canPush) {
            float pushStrength = strength * energy / maxEnergy;

            // Move ground towards camera
            ground.Translate(Vector3.back * pushStrength * Time.deltaTime * 0.25f);

            // Rotate boulder and push horizontally
            boulder.transform.RotateAround(boulder.transform.position, Vector3.right, Mathf.Min(pushStrength * Time.deltaTime, maxRollSpeed));
            boulder.AddForce(new Vector3((boulder.transform.position.x - objectTransform.position.x) * horizontalForce, 0f, 0f));

            // lose energy while pushing
            energy = Mathf.Max(0f, energy - energyDepleteRate * Time.deltaTime);

        } else {
            ground.Translate(Vector3.forward * traction * Time.deltaTime);
            boulder.transform.RotateAround(boulder.transform.position, Vector3.left, Mathf.Min(traction * 0.25f, maxRollSpeed));
            energy = Mathf.Min(maxEnergy, energy + Mathf.Min(energyGainRate * maxEnergy, 2f) * Time.deltaTime);
        }

        if (horizontalSpeed != 0) {
            if (verticalSpeed == 0 && horizontalSpeed > 0 && moving != 1) {
                anim.SetTrigger("right");
                moving = 1;
            } else if (verticalSpeed == 0 && horizontalSpeed < 0 && moving != -1) {
                anim.SetTrigger("left");
                moving = -1;
            }
            horizontalSpeed *= Time.deltaTime * speed;
            Vector3 newPos = objectTransform.position;
            newPos.x = Mathf.Clamp(newPos.x + horizontalSpeed, startTransPos.x + moveBounds.x, startTransPos.x + moveBounds.y);
            objectTransform.position = newPos;
            boulder.AddForce(new Vector3(-horizontalSpeed, 15f, 100f)); // move boulder up so it doesn't slide with player
        } else if (Mathf.Abs(boulder.velocity.x) < 0.5f && ground.transform.position.z < groundStartPos.z) {
            boulder.AddForce(new Vector3(Random.Range(-60f, 60f) * 50f, 0f, 0f)); // randomly deviate from center
        }
        // stopped moving this frame

        // Check boulder distance
        GameState.todaysBest = Mathf.Max(GameState.todaysBest, groundStartPos.z - ground.position.z);
        if (energy == 0 || (boulderStartPos.z - boulder.transform.position.z) >= loseDistance) {
			GameState.TurnNight();
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
            StopRigidbody(boulder);
            rb.WakeUp();
            boulder.WakeUp();
            energy = maxEnergy;
            playStart = Time.time;
            delayOver = false;
            pushing = false;
            moving = 0;
        } else if (!playable) {
            StopRigidbody(rb);
            anim.SetTrigger("sit");
        }
        active = playable;
    }

    void StopRigidbody(Rigidbody rb) {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
    }

}
