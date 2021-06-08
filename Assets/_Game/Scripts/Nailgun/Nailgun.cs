using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Nailgun : MonoBehaviour {
    public float dist;
    public AudioClip successAudio;
    public AudioClip failAudio;
    public Transform head;
    public NailService nailService; 
    public LayerMask validLayers;
    public Transform chassis;
    public Transform carparts;
    public string carPartTag;

    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot() {
        RaycastHit[] hits = Physics.RaycastAll(head.position,head.up,dist,validLayers);
        if (hits.Length < 2 || hits.Length > 3 ) { FailSound(); return; } 
        RaycastHit p0 = hits[0]; RaycastHit p1 = hits[1];
        Vector3 pos = head.position+(head.up*dist)/2;
        RaycastHit part = p0;
        RaycastHit surface = p1;
        if (p1.transform.CompareTag(carPartTag)) { part = p1; surface = p0; }
        nailService.GenerateNail(pos,head.transform.rotation,part.transform,surface.transform);
        SuccessSound();
    }

    public void SuccessSound() { audioSource.clip = successAudio; audioSource.Play(); }
    public void FailSound() { audioSource.clip = failAudio; audioSource.Play(); }

    /*
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
