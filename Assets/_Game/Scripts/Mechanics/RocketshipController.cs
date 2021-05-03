using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketshipController : MonoBehaviour {

    public static readonly string[] shipEssentials = {"Thruster","PilotSeat","GasTank","Gyro"};
    public static readonly string EssentialsTag = "part_Essential"; 
    public static readonly string ExtrasTag = "part_Extra";

    [Header("Config:")]
    public float maxAngDrag = 8f;
    [Header("Data:")]
    public int weight;
    public float fuel;
    [Range(0f,100f)]
    public float gyroIntensity;
    [Header("Parts:")]
    public List<GameObject> shipParts = new List<GameObject>();
    public List<GameObject> partsEssential = new List<GameObject>();
    public List<GameObject> partsExtra = new List<GameObject>();


    [SerializeField]
    private List<GameObject> thrusters = new List<GameObject>();
    private Rigidbody rb;

    void OnStartup() {
        // look for engine part
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        GatherShipData();
    }


    void Update() {
        rb.angularDrag = maxAngDrag*(gyroIntensity/100);
    }

    private void GatherShipData() {
        partsEssential.Clear(); partsExtra.Clear();
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform part in children) {
            if (part.CompareTag(EssentialsTag)) {
                partsEssential.Add(part.gameObject);
                shipParts.Add(part.gameObject);
            } else if (part.CompareTag(ExtrasTag)) {
                partsExtra.Add(part.gameObject);
                shipParts.Add(part.gameObject);
            }
            // Get thrusters
            if (gameObject.name.Contains("part_Thruster")) {
                thrusters.Add(part.gameObject);
            }
        }
        GetComponent<RocketshipThrusters>().GetThrusters();
    }

    public List<GameObject> GetPartsByName(string name,bool essential) {
        List<GameObject> parts = new List<GameObject>();
        List<GameObject> l;
        if (essential) { l = partsEssential; } else { l = partsExtra; }
        foreach (GameObject obj in partsEssential) {
            string[] split = obj.name.Split('_');
            if (split.Length > 0 && split[1] == name) { parts.Add(obj); }
        }
        return parts;
    }
}
 