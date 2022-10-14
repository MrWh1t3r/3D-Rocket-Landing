using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody _rb;
    [SerializeField] private float thrustPower = 1.0f;
    [SerializeField] private float rotationSpeed = 1.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        }
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
