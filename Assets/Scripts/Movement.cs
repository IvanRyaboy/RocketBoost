using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
[SerializeField] float mainThrust = 100; 
[SerializeField] AudioClip mainEngine;
[SerializeField] float sideThrust = 5;
[SerializeField] ParticleSystem mainThrustParticle;
Rigidbody rb;
AudioSource audioSource;
bool isThrust = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTrust();
        ProcessRotation();
    }

    void ProcessTrust()
    {
        StartThrusting();
    }

    void StartThrusting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainThrustParticle.isPlaying)
            {
                mainThrustParticle.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainThrustParticle.Stop();
        }
    }


    private void RightRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            RotationSpeed(sideThrust);
        }
    }

    void ProcessRotation()
    {
        LeftRotation();
        RightRotation();
    }

    void LeftRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotationSpeed(-sideThrust);
        }
    }

    public void RotationSpeed(float SideOfRotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * SideOfRotation * Time.deltaTime);
        rb.freezeRotation = false;
    }
    
}
