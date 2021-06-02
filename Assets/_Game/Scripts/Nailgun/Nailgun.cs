using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Nailgun : MonoBehaviour {
    public float dist;
    public AudioClip successAudio;
    public AudioClip failAudio;
    public Transform head;
    public NailService nailService; 
    public LayerMask nailableLayers;

    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot() {
        RaycastHit[] hits = Physics.RaycastAll(head.position,head.up,dist,nailableLayers);
        if (hits.Length < 2 || hits.Length > 3 ) { FailSound(); return; } 
        RaycastHit p0 = hits[0]; RaycastHit p1 = hits[1];
       /* // check for situations like nailing prop0 -> prop0_sibling -> prop1
        if (hits.Length == 3 && hits[1].transform.IsChildOf(hits[0].transform.parent) && !hits[2].transform.IsChildOf(hits[0].transform.parent))
            p0 = hits[0]; p1 = hits[2];*/
        // handle nail generation
        Vector3 pos = head.position+(head.up*dist)/2; 
        nailService.GenerateNail(pos,head.transform.rotation,p0.transform.gameObject,p1.transform.gameObject);
        SuccessSound();
    }

    public void SuccessSound() { audioSource.clip = successAudio; audioSource.Play(); }
    public void FailSound() { audioSource.clip = failAudio; audioSource.Play(); }

    /*

    public void NailProp(GameObject prop) {
        if (prop == null) return;
        Rigidbody r; XRGrabInteractable grabScript;
        if (prop.TryGetComponent<Rigidbody>(out r)) 
            r.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            r.isKinematic = true;
        if (prop.TryGetComponent<XRGrabInteractable>(out grabScript))
            grabScript.interactionLayerMask = 0;
        prop.layer = nailedLayer;
        foreach(Transform child in prop.GetComponentsInChildren<Transform>()) {
            child.gameObject.layer = nailedLayer;
        }
    }

    public void Shoot() {
        RaycastHit[] hits = Physics.RaycastAll(head.position,head.up,dist,nailableLayers);
        if (hits.Length != 2) { return; } 
        NailProp(hits[0].transform.gameObject);
        NailProp(hits[1].transform.gameObject);
        GameObject nail = Instantiate(nailPrefab, head.position+(head.up*dist)/2, head.transform.rotation);
        Nail n = nail.GetComponent<Nail>();
        n.obj0 = hits[0].transform.gameObject;
        n.obj1 = hits[1].transform.gameObject;
        audioSource.Play();
    }

    */

}
