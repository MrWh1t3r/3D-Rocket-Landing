using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrustPower = 1.0f;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private ParticleSystem mainBoosterParticles;
    [SerializeField] private ParticleSystem leftBoosterParticles;
    [SerializeField] private ParticleSystem rightBoosterParticles;
    
    private Rigidbody _rb;
    private AudioSource _audioSource;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!_audioSource.isPlaying)
                _audioSource.PlayOneShot(mainEngine);
            
            if (!mainBoosterParticles.isPlaying)
                mainBoosterParticles.Play();
            
            _rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        }
        else
        {
            _audioSource.Stop();
            mainBoosterParticles.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
            if(!leftBoosterParticles.isPlaying)
                leftBoosterParticles.Play();
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
            if(!rightBoosterParticles.isPlaying)
                rightBoosterParticles.Play();
        }
        else
        {
            rightBoosterParticles.Stop();
            leftBoosterParticles.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        _rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rb.freezeRotation = false;
    }
}
