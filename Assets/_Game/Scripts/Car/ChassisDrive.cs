using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChassisDrive : MonoBehaviour {
    [Header("Config:")]
    public bool active = false;
    public float force = 100f;

    [Header("Inputs:")]
    public InputAction forward;
    public InputAction back;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        print(forward.ReadValueAsObject());
        if (false) {
            print("Go!");
            rb.AddForce(((transform.forward.normalized)*force)*Time.deltaTime,ForceMode.Acceleration);
        }
        if (back.ReadValue<bool>()) {
            rb.AddForce(((transform.forward.normalized)*-force)*Time.deltaTime,ForceMode.Acceleration);
        }
    }
}
