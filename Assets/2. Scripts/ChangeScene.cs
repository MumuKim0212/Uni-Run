using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string mainScene = "Main";
    int start;
    public Transform pen;
    private void FixedUpdate()
    {
        if(start == 1)
            pen.Translate(0.3f, 0, 0);
    }
    public void MainScene()
    {
        StartCoroutine(LoadScene(mainScene));
        start = 1;
    }
    IEnumerator LoadScene(string st)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(st);
    }
}
