using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrustPower = 1.0f;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private AudioClip mainEngine;
    
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
            _rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        }
        else
            _audioSource.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            _rb.freezeRotation = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _rb.freezeRotation = true;
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
            _rb.freezeRotation = false;
        }
    }
}
