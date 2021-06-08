using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NailService : MonoBehaviour {
    public GameObject nailPrefab;
    public Transform partContainer;
    public Transform nailContainer;
    public Transform chassis;
    public string carPartTag;
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

    public void GenerateNail(Vector3 nailPos, Quaternion nailRot, Transform part, Transform surface) {
        if (part == null) { Debug.LogWarning("Tried nailing null."); return; }
        GameObject nailObject = Instantiate(nailPrefab, nailPos, nailRot, nailContainer);
        TogglePropFreeze(part.gameObject,true);
        nails.Add(new Nail(nailObject,part.gameObject,surface.gameObject));
        part.transform.SetParent(partContainer);
    }

    private void TogglePropFreeze(GameObject prop, bool state) {
        Rigidbody rb; XRNoSnapGrabInteractable interactable;
        bool gotRb = prop.TryGetComponent<Rigidbody>(out rb);
        bool gotInteractable = prop.TryGetComponent<XRNoSnapGrabInteractable>(out interactable);
        if (state) {
            // FREEZE
            if (gotRb) { rb.detectCollisions = false; rb.isKinematic = true; } // freeze the prop
            if (gotInteractable) { interactable.active = false; } // make prop non interactable
        } else {
            // UNFREEZE
            if (gotRb) { rb.isKinematic = false; rb.detectCollisions = true; } // unfreeze the prop
            if (gotInteractable) { interactable.active = true; } // make prop interactable
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
