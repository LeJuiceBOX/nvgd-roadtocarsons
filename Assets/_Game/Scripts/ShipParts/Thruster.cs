using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class Thruster : MonoBehaviour {

    [Header("Config:")]
    public bool active;
    public float thrust;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (active) {
            print("Thrusting!");
            Vector3 thr = (Vector3.up*thrust)*Time.deltaTime;
            rb.AddForceAtPosition(thr,transform.position+new Vector3(0,-transform.localScale.y/2,0),ForceMode.Force);
        }
    }

}
