using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NailService : MonoBehaviour {
    public GameObject nailPrefab;
    public List<Nail> nails = new List<Nail>();
    public List<GameObject> nailedProps = new List<GameObject>();

    private void Start() {
    }

    public void RemoveNail(GameObject nailObj) {
        Nail n = GetNail(nailObj);
        if (n != null) { nails.Remove(n); }
        Destroy(nailObj);
        HandleUnNailedProps();
    }

    public void GenerateNail(Vector3 nailPos, Quaternion nailRot, GameObject prop0, GameObject prop1) {
        if (prop0 == null || prop1 == null) { Debug.LogWarning("Tried nailing null."); return; }
        GameObject nailObject = Instantiate(nailPrefab, nailPos, nailRot);
        TogglePropFreeze(prop0,true); TogglePropFreeze(prop1,true);
        nails.Add(new Nail(nailObject,prop0,prop1));
        if (!nailedProps.Contains(prop0)) { nailedProps.Add(prop0); }
        if (!nailedProps.Contains(prop1)) { nailedProps.Add(prop1); }
    }

    private void TogglePropFreeze(GameObject prop, bool state) {
        Rigidbody rb; XRNoSnapGrabInteractable interactable;
        bool gotRb = prop.TryGetComponent<Rigidbody>(out rb);
        bool gotInteractable = prop.TryGetComponent<XRNoSnapGrabInteractable>(out interactable);
        if (state) {
            if (gotRb) { rb.isKinematic = true; } // freeze the prop
            if (gotInteractable) {  } // make prop non interactable
        } else {
            if (gotRb) { rb.isKinematic = false; } // freeze the prop
            if (gotInteractable) { } // make prop non interactable
        }
    }

    private bool IsNailed(GameObject prop) {
        foreach (Nail n in nails) {
            if (n.prop0 == prop || n.prop1 == prop)
                return true;
        }
        return false;
    }

    private Nail GetNail(GameObject nailObj) {
        foreach (Nail n in nails) {
            if (n.nailObj == nailObj) 
                return n;
        }
        Debug.LogError("How the fuck did we get here...");
        return null;
    }

    private void HandleUnNailedProps() {
        foreach (GameObject p in nailedProps) {
            if (!IsNailed(p)) {
                TogglePropFreeze(p,false);
            }
        }
    }
}
