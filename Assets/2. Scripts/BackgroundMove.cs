using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    float tmpTimeSum = 0f;
    float moveSpeed = 20f;
    public bool isDamaged = false;

    private void Start()
    {
    }
    private void FixedUpdate()
    {
        if (isDamaged == true)
            ScoreAdd();
    }

    void ScoreAdd()
    {
        if (tmpTimeSum <= 2f)
        {
            gameObject.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            tmpTimeSum += 1f;
        }
        else if (tmpTimeSum <= 5f)
        {
            gameObject.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            tmpTimeSum += 1f;
        }
        else if (tmpTimeSum <= 8f)
        {
            gameObject.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            tmpTimeSum += 1f;
        }
        else
        {
            gameObject.transform.position = Vector3.zero;
            tmpTimeSum = 0;
            isDamaged = false;
        }
    }
}
