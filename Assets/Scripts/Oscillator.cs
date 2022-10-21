using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector;
    [SerializeField] [Range(0,1)] private float movementFactor;
    [SerializeField] private float period = 2f;
    
    private Vector3 startingPosition;
    const float tau = Mathf.PI * 2; // constant value of 6.283

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if(period<= Mathf.Epsilon)
            return;
        
        float cycles = Time.time / period; //continually growing over time
        
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 0
        
        Debug.Log(rawSinWave);

        movementFactor = (rawSinWave + 1f) / 2; // going from 0 to 1
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
