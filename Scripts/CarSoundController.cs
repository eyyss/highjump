using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundController : MonoBehaviour
{
    private Rigidbody rb;


    public AudioSource carSource;
    public AudioClip crashClip;

    public float minSpeed;
    public float maxSpeed;
    private float currentSpeed;

    public float minPitch;
    public float maxPitch;
    private float pitchFromCar;



    private void Start()
    {
        rb=GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        MotorSound();

    }

    private void MotorSound()
    {
        currentSpeed = rb.velocity.magnitude;
        pitchFromCar = rb.velocity.magnitude / 20;
        if (currentSpeed < minSpeed)
        {
            carSource.pitch = minPitch;
        }
        if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            carSource.pitch = minPitch + pitchFromCar;
        }
        if (currentSpeed > maxSpeed)
        {
            carSource.pitch = maxPitch;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.magnitude>1f)
        {
            AudioSource.PlayClipAtPoint(crashClip, transform.position,rb.velocity.normalized.magnitude/2);
        }
    }
}
