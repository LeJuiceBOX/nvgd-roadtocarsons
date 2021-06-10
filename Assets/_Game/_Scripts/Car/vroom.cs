using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public enum DriveType {
    Front,
    Back,
    All
}
public class vroom : MonoBehaviour {


    [Header("Control:")]
    public bool engineRunning;
    [Range(-1f,1f)]
    public float throttle;
    [Range(-1f,1f)]
    public float steer;
    [Range(0f,1f)]
    public float brake;
    [Header("Config:")]
    public int maxSteer = 35;
    public int motorForce = 50;
    public float thrusterForce = 80;
    public int brakeTorque = 65;
    public Vector3 centerOfMass = new Vector3(0,-1,0);
    public DriveType driveType = DriveType.Front;
    [Header("CarParts:")]
    public bool partEngine;
    public bool partEngineV2;
    public bool partSpringBox;
    public int numThrusters;
    [Header("References:")]
    public Transform rig;
    public ActionBasedContinuousMoveProvider moveProvider;
    public Transform partContainer;
    public WheelCollider wcFL;
    public WheelCollider wcBL;
    public WheelCollider wcFR;
    public WheelCollider wcBR;
    public Transform wtFL;
    public Transform wtBL;
    public Transform wtFR;
    public Transform wtBR;

    [Header("Input:")]
    public InputAction kbControls;
    public InputAction vrControls;

    private WheelHandler wheelHandler;
    private Rigidbody rb;
    private float steeringAngle;

    void Start() {
        rb = GetComponent<Rigidbody>();
        wheelHandler = GetComponent<WheelHandler>();
        // init values
        wheelHandler.springLength = 0f;
    }

    public void StartEngine() {
        transform.GetComponent<CarSoundEffects>().StartEngine();
        partEngine = (bool) HasPart("Engine");
        partEngineV2 = (bool) HasPart("EngineV2");
        partSpringBox = (bool) HasPart("SpringBox");
        numThrusters = PartCount("Thruster");
        if (!partEngine && !partEngineV2) { StopEngine(); return; }
        HandlePartChanges();
        engineRunning = true;
    }

    public void StopEngine() {
        //car.GetComponent<vroom>().brake = 1f;
        transform.GetComponent<CarSoundEffects>().ShutoffEngine();
        engineRunning = false;
        HandlePartChanges();
    }

    public void HandlePartChanges() {
        if (partEngine) {motorForce = 1000;}
        if (partEngineV2) {motorForce = 2500;}
        if (partSpringBox) { wheelHandler.springLength = 0.3f; }
        thrusterForce = numThrusters*2000;
        wheelHandler.UpdateWheelSettings();
    }

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
            rb.AddForce((transform.forward*thrusterForce)*throttle);
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

    public Transform HasPart(string pName) {
        return partContainer.Find("carPart_"+pName);
    }

    public int PartCount(string pName) {
        int ct = 0;
        foreach (Transform part in partContainer.GetComponentInChildren<Transform>()) {
            if (part.name == "carPart_"+pName) {ct++;}
        }
        return ct;
    }


    void OnEnable() { kbControls.Enable(); }
    void OnDisable() { kbControls.Disable(); }

    void Update() {
        Vector2 axis = kbControls.ReadValue<Vector2>();
        throttle = axis.y;
        steer = Mathf.Clamp(axis.x,-1,1);
        rb.centerOfMass = centerOfMass;
        partContainer.position = transform.position;
        partContainer.rotation = transform.rotation;
    }

    void FixedUpdate() {
        Steer();
        UpdateWheelPose();
        if (engineRunning) {
            Accelerate();
            Brake();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.rotation * centerOfMass, 0.075f);
    }
}
