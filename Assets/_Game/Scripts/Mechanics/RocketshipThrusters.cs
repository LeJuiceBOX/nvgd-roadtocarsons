using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketshipThrusters : MonoBehaviour {

    [Header("Config:")]
    public RocketshipController RocketshipController;
    public float thrusterPower;
    public bool active = false;

    [SerializeField]
    private List<GameObject> thrusters;
    private Rigidbody rb;


    void Start() {
        rb = GetComponent<Rigidbody>();
        GetThrusters();
    }

    void FixedUpdate() {
        if (active) {
            foreach(GameObject thruster in thrusters) {
                rb.AddForceAtPosition((transform.up*thrusterPower)*Time.deltaTime,thruster.transform.position,ForceMode.Force);
            }
        }
    }

    // gets called in RocketshipController after its done intializing
    public void GetThrusters() {
        thrusters.Clear();
        thrusters = RocketshipController.GetPartsByName("Thruster",true);
    }

}
