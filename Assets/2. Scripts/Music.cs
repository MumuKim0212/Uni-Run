using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public static Music instance = null;
    AudioSource audioSource;

    public bool isSoundOn = true;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isSoundOn == false)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
        if (SceneManager.GetActiveScene().name == "Menu")
            audioSource.mute = true;
        else
            audioSource.mute = false;


    }
}
