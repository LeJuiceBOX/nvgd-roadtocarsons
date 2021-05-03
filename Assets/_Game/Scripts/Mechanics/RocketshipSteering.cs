using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketshipSteering : MonoBehaviour {

    [Header("Config:")]
    public RocketshipController RocketshipController;
    public float maxSteeringVel = 10;
    public bool active = true;
    [Header("Data:")]
    public Vector2 steeringAxis;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        steeringAxis.x = Mathf.Clamp(steeringAxis.x,-1,1);
        steeringAxis.y = Mathf.Clamp(steeringAxis.y,-1,1);
    }

    void FixedUpdate() {
        if (active) {
            rb.AddTorque(new Vector3((maxSteeringVel*steeringAxis.x)*Time.deltaTime,0,(maxSteeringVel*steeringAxis.y)*Time.deltaTime),ForceMode.Force);
        }
    }
}
