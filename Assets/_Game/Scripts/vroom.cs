using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class vroom : MonoBehaviour {
    [Range(-1f,1f)]
    public float throttle;
    [Range(-1f,1f)]
    public float turn;
    public int maxTurn;
    public int maxSpeed;
    private Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        turn = Mathf.Clamp(turn,-1,1);
    }

    void FixedUpdate() {
        rb.AddForce(transform.forward*(maxSpeed*throttle),ForceMode.Acceleration);
    }
}
