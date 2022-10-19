using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrustPower = 1.0f;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private AudioClip mainEngineAudio;
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
            StartTrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StopThrusting()
    {
        _audioSource.Stop();
        mainBoosterParticles.Stop();
    }

    private void StartTrusting()
    {
        if (!_audioSource.isPlaying)
            _audioSource.PlayOneShot(mainEngineAudio);

        if (!mainBoosterParticles.isPlaying)
            mainBoosterParticles.Play();

        _rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
    }

    private void StopRotating()
    {
        rightBoosterParticles.Stop();
        leftBoosterParticles.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationSpeed);
        if (!rightBoosterParticles.isPlaying)
            rightBoosterParticles.Play();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!leftBoosterParticles.isPlaying)
            leftBoosterParticles.Play();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        _rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rb.freezeRotation = false;
    }
}
