using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steeringwheel : MonoBehaviour {

    public float lerpAlpha;
    public Transform trackerInitial;
    public Transform trackerCurrent;
    public Transform rightHand;
    public Transform leftHand;
    public Transform mesh;

    public vroom CarController;

    public Vector2 rotationRange = new Vector3(-90,90);

    private Rigidbody rb;
    public bool isSteeringWheelActive;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void UpdateSteering() {
        float steerVal = Mathf.Abs(mesh.eulerAngles.z/360f);
        Debug.Log(steerVal);
        CarController.steer = steerVal;
    }

    public void Test() {
        Debug.Log("Hello!");
    }

    Vector3 rotDiff = Vector3.zero;
    Vector3 init = Vector3.zero;
    void FixedUpdate() {
        if (isSteeringWheelActive) { 
            trackerCurrent.LookAt(rightHand);  
            rotDiff = trackerInitial.localEulerAngles - trackerCurrent.localEulerAngles;
            //if (rotDiff.z > 90 || rotDiff.z < -90 ) { rotDiff.z = -rotDiff.z; }
            UpdateSteering();
        }
        mesh.eulerAngles = new Vector3(mesh.eulerAngles.x,mesh.eulerAngles.y,(init.x - rotDiff.x));
        //float angle = Mathf.LerpAngle(transform.rotation.z,pointer.rotation.z,transform.up,lerpAlpha);
        //print(angle); 
    }

    public void StartCur() { isSteeringWheelActive = true; }
    public void EndCur() { isSteeringWheelActive = false; }
    public void SetInit() { init = trackerInitial.eulerAngles; trackerInitial.LookAt(rightHand); }

}
