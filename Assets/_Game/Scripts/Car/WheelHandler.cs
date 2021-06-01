using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelHandler : MonoBehaviour
{
    [Header("Wheel References:")]
    public WheelCollider[] wheels;
    [Header("Wheels:")]
    public float wheelMass = 20;
    public float wheelRadius = 0.38f;
    public float wheelDamping = 0.25f;
    public float forceAppPointDist = 0;
    public Vector3 wheelCenter = Vector3.zero;
    [Header("Suspention:")]
    public float springLength = 0.6f;
    public float springTargetPos = 0.5f;
    public float springDamper = 2000f;
    public float springPower = 35000f;
    [Header("Forward Friction:")]
    public float ffExtremiumSlip = 5f;
    public float ffExtremiumValue = 3f;
    public float ffAsymptoteSlip = 0.8f;
    public float ffAsymptoteValue = 0.5f;
    public float ffStiffness = 1f;
    [Header("Sideway Friction:")]
    public float sfExtremiumSlip = 0.2f;
    public float sfExtremiumValue = 1f;
    public float sfAsymptoteSlip = 0.5f;
    public float sfAsymptoteValue = 0.75f;
    public float sfStiffness = 1f;

    public void UpdateWheelSettings() {
        foreach (WheelCollider wheel in wheels) {
            JointSpring springSettings = new JointSpring();
            springSettings.damper = springDamper;
            springSettings.spring = springPower;
            springSettings.targetPosition = springTargetPos;

            WheelFrictionCurve forwardFriction = new WheelFrictionCurve();
            forwardFriction.extremumSlip = ffExtremiumSlip;
            forwardFriction.extremumValue = ffExtremiumValue;
            forwardFriction.asymptoteSlip = ffAsymptoteSlip;
            forwardFriction.asymptoteValue = ffAsymptoteValue;
            forwardFriction.stiffness = ffStiffness;

            WheelFrictionCurve sidewaysFriction = new WheelFrictionCurve();
            sidewaysFriction.extremumSlip = sfExtremiumSlip;
            sidewaysFriction.extremumValue = sfExtremiumValue;
            sidewaysFriction.asymptoteSlip = sfAsymptoteSlip;
            sidewaysFriction.asymptoteValue = sfAsymptoteValue;
            sidewaysFriction.stiffness = sfStiffness;

            wheel.suspensionSpring = springSettings;
            wheel.forwardFriction = forwardFriction;
            wheel.sidewaysFriction = sidewaysFriction;

            wheel.mass = wheelMass;
            wheel.radius = wheelRadius;
            wheel.center = wheelCenter;
            wheel.wheelDampingRate = wheelDamping;
            wheel.suspensionDistance = springLength;
            wheel.forceAppPointDistance = forceAppPointDist;
        }
    }

    private void Start() {
        UpdateWheelSettings();
    }

}
