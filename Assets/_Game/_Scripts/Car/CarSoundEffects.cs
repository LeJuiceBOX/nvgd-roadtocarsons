using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundEffects : MonoBehaviour
{
    [Header("Data:")]
    public bool engineOn;
    public int gear;
    public AudioClip current;
    [Header("Config:")]
    public AudioSource audioSource;
    public vroom carScript;
    public Rigidbody carBody;
    public float topVelocity;
    public float pitchLimit;
    [Header("SoundConfig:")]
    public int StartupSoundId;
    public int LowSoundId;
    public int MedSoundId;
    public int HighSoundId;
    public AudioClip[] clips;

    private float pitch = 1f;

    public void StartEngine() {
        audioSource.pitch = 1f;
        SetPlay(MedSoundId);
        gear = 1;
        engineOn = true;
        audioSource.loop = true;
    }

    public void ShutoffEngine() {
        audioSource.pitch = 1f;
        audioSource.loop = false;
        gear = 0;
    }

    void Update() {
        if (engineOn) {
            pitch = Mathf.Clamp(carBody.velocity.magnitude/topVelocity,0f,pitchLimit);
            audioSource.pitch = pitch;
        }
    }

    void SetClip(int id) { audioSource.clip = clips[id]; }
    void SetPlay(int id) { audioSource.clip = clips[id]; audioSource.Play(); }
}
