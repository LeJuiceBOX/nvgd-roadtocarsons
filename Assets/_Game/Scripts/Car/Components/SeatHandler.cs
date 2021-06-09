using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatHandler : MonoBehaviour
{
    [Header("Data:")]
    public bool occupied;
    [Header("Config:")]
    public Transform car;
    public Transform rig;
    public Transform rigCamera;
    public Vector3 offset;

    private bool occuChanged;

    private void Start() {
        Enter();
    }



    public void Enter() {
        occupied = true;
        rig.position = transform.position+offset;
        rig.position += rigCamera.localPosition;
        rig.SetParent(transform);
    }

    public void Exit() {
        occupied = false;
        rig.SetParent(null);
    }

}
