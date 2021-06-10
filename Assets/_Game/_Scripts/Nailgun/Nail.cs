using UnityEngine;

[System.Serializable]
public class Nail {
    public GameObject nailObj;
    public GameObject prop0;
    public GameObject prop1;

    public Nail(GameObject n, GameObject p0, GameObject p1) {
        nailObj = n;
        prop0 = p0;
        prop1 = p1;
    }
}
/*
    public void RemoveNail() {
        Destroy(gameObject);
        UnNailProp(obj0);
        UnNailProp(obj1);
    }

    private void UnNailProp(GameObject obj) {
        Rigidbody r; XRGrabInteractable grabScript;
        if (obj.TryGetComponent<Rigidbody>(out r)) 
            r.constraints = RigidbodyConstraints.None;
            r.isKinematic = false;
        if (obj.TryGetComponent<XRGrabInteractable>(out grabScript))
            grabScript.interactionLayerMask = interactionMask;
        // set layer to "Prop"
        obj.layer = propLayer;
        foreach(Transform child in obj.GetComponentsInChildren<Transform>()) {
            child.gameObject.layer = propLayer;
        }
    }
*/