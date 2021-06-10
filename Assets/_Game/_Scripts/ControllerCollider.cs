using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCollider : MonoBehaviour
{
    public Transform controllerTransform;

    private void FixedUpdate() {
        transform.position = controllerTransform.position;
        transform.rotation = controllerTransform.rotation;
    }
}
