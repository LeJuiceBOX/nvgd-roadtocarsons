using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCone : MonoBehaviour
{

    public string targetScene;
    public Transform tp;
    public Vector2 tpyMinMax;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (transform.position.y < tpyMinMax.x || transform.position.y > tpyMinMax.y) { transform.position = tp.position; rb.velocity = Vector3.zero; }
    }

    public void Use() {
        SceneManager.LoadScene(targetScene);
    }
}
