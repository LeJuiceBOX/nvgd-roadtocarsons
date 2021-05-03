using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flightmechanics_Camera : MonoBehaviour
{

    public GameObject target;
    public Vector3 offset;

    void Start() {
        transform.rotation = target.transform.rotation;
    }

    void Update() {
        transform.position = target.transform.position+offset;
    }
}
