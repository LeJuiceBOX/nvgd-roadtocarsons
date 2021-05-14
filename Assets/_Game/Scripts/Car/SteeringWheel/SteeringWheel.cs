using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SteeringWheel : MonoBehaviour {
    [Header("Data:")]
    [Range(-1f,1f)]
    public float steerValue;
    public float steerAngle;
    public float targetSteerAngle;
    [Header("Config:")]
    public bool active = true;
    public bool showHotspots = false;
    public int segments = 30;
    public float alphaMod = 4; // bigger val -> faster lerp
    public int steerValueAcc = 2; // decimal places before cutoff
    public float maxSteerAngle = 80; // the angle at which steerValue is 1 or -1;
    public float wheelRadius = 0.5f; // the radius of the steering wheel
    public Vector3 trigSize = new Vector3(0.015f,0.2f,0.015f);
    [Header("References:")]
    public vroom carController;
    public Transform model;
    public Transform hotspot;
    public Transform hotspotFolder;
    public TMP_Text text;

    private Transform currentHotspot;

    void Start() {
        maxSteerAngle = Mathf.Clamp(maxSteerAngle,0,180);
        steerValueAcc = Mathf.Clamp(steerValueAcc,0,5);
        GenerateHotspots();
    }

    void FixedUpdate() {
        targetSteerAngle = Mathf.Clamp(targetSteerAngle,-maxSteerAngle,maxSteerAngle);
        steerAngle = Mathf.LerpAngle(steerAngle,targetSteerAngle,Time.deltaTime*alphaMod);
        model.localRotation = Quaternion.Euler(0,-steerAngle,0);
        float pow = Mathf.Pow(10,steerValueAcc);
        steerValue = -Mathf.Round(steerAngle/maxSteerAngle*pow)/pow;
        text.text = steerValue.ToString();
        carController.steer = steerValue;
    }

    public void HotspotTouched(Transform hs) {
        currentHotspot = hs;
        targetSteerAngle = hs.GetComponent<SteeringWheel_Hotspot>().angle;
    }

    void GenerateHotspots() {
        for (int i = 0; i < segments; i++) {
            float angle = (i * Mathf.PI*2f)/segments;
            GameObject go = Instantiate(hotspot.gameObject, Vector3.zero, Quaternion.identity, model);
            go.transform.localPosition = new Vector3(Mathf.Cos(angle)*wheelRadius, model.localPosition.y, Mathf.Sin(angle)*wheelRadius);
            float a = Mathf.Rad2Deg*angle;
            go.transform.localEulerAngles = new Vector3(0,-a,0);
            if (a > 180) {a -= 360;}
            go.name = a.ToString();
            go.GetComponent<SteeringWheel_Hotspot>().Init(a,this);
            go.transform.parent = hotspotFolder;
            go.transform.localScale = new Vector3(trigSize.y,trigSize.x,trigSize.z); // x is up
            if (!showHotspots) { go.GetComponent<MeshRenderer>().enabled = false; }
        }
    }
}
