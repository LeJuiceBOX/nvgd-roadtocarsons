using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum DriveType {
    Front,
    Back,
    All
}
public class vroom : MonoBehaviour {


    [Header("Control:")]
    [Range(-1f,1f)]
    public float throttle;
    [Range(-1f,1f)]
    public float steer;
    [Header("Config:")]
    public int maxSteer = 35;
    public int motorForce = 50;
    public DriveType driveType = DriveType.Front;

    [Header("References:")]
    public WheelCollider leftFrontW, leftBackW;
    public WheelCollider rightFrontW, rightBackW;
    public Transform leftFrontT, leftBackT;
    public Transform rightFrontT, rightBackT;

    [Header("Input:")]
    public InputAction kbControls;

    private Rigidbody rb;
    private float steeringAngle;


    private void Steer() {
        steeringAngle = maxSteer*steer;
        rightFrontW.steerAngle = steeringAngle;
        leftFrontW.steerAngle = steeringAngle;
    }

    private void Accelerate() {
        // if using All execute both, if otherwise only use the cooresponding block
        if (driveType == DriveType.All || driveType == DriveType.Front ) {
            leftFrontW.motorTorque = throttle*motorForce;
            rightFrontW.motorTorque = throttle*motorForce;
        }
        if (driveType == DriveType.All || driveType == DriveType.Back) {
            leftBackW.motorTorque = throttle*motorForce;
            rightBackW.motorTorque = throttle*motorForce;
        }    
    }

    private void UpdateWheelPose() {
        SingleWheelPose(leftFrontW,leftFrontT);
        SingleWheelPose(rightFrontW,rightFrontT);
        SingleWheelPose(leftBackW,leftBackT);
        SingleWheelPose(rightBackW,rightBackT);
    }

    private void SingleWheelPose(WheelCollider coll, Transform trans) {
        Vector3 pos = trans.position;
        Quaternion rot = trans.rotation;
        coll.GetWorldPose(out pos, out rot);
        trans.position = pos;
        trans.rotation = rot;
    }


    void OnEnable() { kbControls.Enable(); }
    void OnDisable() { kbControls.Disable(); }

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        Vector2 axis = kbControls.ReadValue<Vector2>();
        throttle = axis.y;
        steer = Mathf.Clamp(axis.x,-1,1);
    }

    void FixedUpdate() {
        Steer();
        Accelerate();
        UpdateWheelPose();
    }
}
