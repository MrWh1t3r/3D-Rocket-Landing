using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 1.5f;
    [SerializeField] private AudioClip crashAudio;
    [SerializeField] private AudioClip successAudio;
    [SerializeField] private ParticleSystem crashParticles;
    [SerializeField] private ParticleSystem successParticles;
    [SerializeField] private bool isLastLevel = false;
    
    private AudioSource _audioSource;

    private bool _isTransitioning = false;
    private bool _isCollisionEnabled = true;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        DebugKeys();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_isTransitioning || !_isCollisionEnabled)
            return;
        
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Start");
                break;
            case "Fuel":
                Debug.Log("Refill");
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                StartCrashSequence();
                break;
                    
        }
    }

    void StartCrashSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(crashAudio);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }
    
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void StartFinishSequence()
    {
        _isTransitioning = true;
        Debug.Log("Finished");
        _audioSource.Stop();
        _audioSource.PlayOneShot(successAudio);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", levelLoadDelay);
    }
    
    void NextLevel()
    {
        if (isLastLevel)
        {
            Win();
            return;
        }

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    void Win()
    {
        Debug.Log("Congrats! You completed the game");
    }
    
    //debug buttons
    void DebugKeys()
    {
        //Press L to skip current level
        if (Input.GetKey(KeyCode.L))
        {
            if (isLastLevel)
            {
                Debug.Log("It's last level");
                return;
            }
            NextLevel();
            Debug.Log("Level skipped");
        }
        
        //Press C to disable collision
        else if (Input.GetKey(KeyCode.C))
        {
            _isCollisionEnabled = !_isCollisionEnabled;
            Debug.Log("CollisionEnabled =  " + _isCollisionEnabled);
        }
        
        //Press R to reload current level
        else if (Input.GetKey(KeyCode.R))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}
 