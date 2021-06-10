using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel_Hotspot : MonoBehaviour {
    [Header("Config:")]
    public float angle;
    public SteeringWheel main;
    public string controllerTag = "XR Controller"; 

    private void OnTriggerStay(Collider col) {
        //Debug.Log("Enter!");
        if (col.CompareTag(controllerTag)) { main.HotspotTouched(transform); }
    }

    public void Init(float ang, SteeringWheel mainScript) {
        angle = ang; 
        main = mainScript;
    }

}
