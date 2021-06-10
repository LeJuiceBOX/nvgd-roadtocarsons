
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SeatHandler : MonoBehaviour
{
    [Header("Data:")]
    public bool occupied;
    public bool active;
    [Header("Config:")]
    public float sittingMoveSpeed = 0.5f;
    public float standingMoveSpeed = 1.5f;
    public float exitDist = 1f;
    public Transform car;
    public Transform rig;
    public Transform rigCamera;
    public vroom carScript;
    public CarSoundEffects carSfx;
    public ActionBasedContinuousMoveProvider moveProvider;
    public Vector3 offset;

    private void OnTriggerEnter(Collider other) {
        if (!active) { return; }
        if (LayerMask.LayerToName(other.gameObject.layer) == "XR Rig") {
            Enter();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (LayerMask.LayerToName(other.gameObject.layer) == "XR Rig") {
            if (!occupied && !active) {
                active = true;
                print("Reactivated seat...");
            }
        }
    }

    public void FixedUpdate() {
        if (occupied) {
            if (Vector3.Distance(transform.position, rigCamera.position) > exitDist) 
                Exit();
        }
    }

    public void Enter() {
        print("Called");
        occupied = true;
        moveProvider.moveSpeed = sittingMoveSpeed;
        rig.GetComponent<CharacterController>().enabled = false;
        rig.localPosition += offset;
        //rig.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
        rig.SetParent(transform);
        carScript.StartEngine();
        print("Entered seat...");
    }

    public void Exit() {
        occupied = false;
        rig.SetParent(null);
        rig.GetComponent<CharacterController>().enabled = true;
        rig.eulerAngles = new Vector3(0,rig.eulerAngles.y,0);
        moveProvider.moveSpeed = standingMoveSpeed;
        carScript.StopEngine();
        print("Exited seat...");
    }

}
