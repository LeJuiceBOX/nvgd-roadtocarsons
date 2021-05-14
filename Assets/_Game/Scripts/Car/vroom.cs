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
    [Range(0f,1f)]
    public float brake;
    [Header("Config:")]
    public int maxSteer = 35;
    public int motorForce = 50;
    public int brakeTorque = 65;
    public Vector3 centerOfMass = new Vector3(0,-1,0);
    public DriveType driveType = DriveType.Front;

    [Header("References:")]
    public WheelCollider wcFL, wcBL;
    public WheelCollider wcFR, wcBR;
    public Transform wtFL, wtBL;
    public Transform wtFR, wtBR;

    [Header("Input:")]
    public InputAction kbControls;
    public InputAction vrControls;

    private Rigidbody rb;
    private float steeringAngle;

    private void Steer() {
        steeringAngle = maxSteer*steer;
        wcFR.steerAngle = steeringAngle;
        wcFL.steerAngle = steeringAngle;
    }

    private void Accelerate() {
        // if using All execute both, if otherwise only use the cooresponding block
        if (driveType == DriveType.All || driveType == DriveType.Front ) {
            wcFL.motorTorque = throttle*motorForce;
            wcFR.motorTorque = throttle*motorForce;
        }
        if (driveType == DriveType.All || driveType == DriveType.Back) {
            wcBL.motorTorque = throttle*motorForce;
            wcBR.motorTorque = throttle*motorForce;
        }    
    }

    private void Brake() {
        float bt = 0;
        if (brake > 0) { bt = brakeTorque; }
        wcFR.brakeTorque = bt; wcFL.brakeTorque = bt;
        wcBR.brakeTorque = bt; wcBL.brakeTorque = bt;
    }

    private void UpdateWheelPose() {
        SingleWheelPose(wcFL,wtFL);
        SingleWheelPose(wcFR,wtFR);
        SingleWheelPose(wcBL,wtBL);
        SingleWheelPose(wcBR,wtBR);
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
        rb.centerOfMass = centerOfMass;
    }

    void FixedUpdate() {
        Steer();
        Accelerate();
        Brake();
        UpdateWheelPose();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.rotation * centerOfMass, 0.075f);
    }
}
