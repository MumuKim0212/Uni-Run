using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance = null;

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
    private void Update()
    {
        if (isSoundOn == false)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
