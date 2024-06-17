using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string mainScene = "Main";
    int start;
    public AudioClip audioClip;
    public AudioSource audioSource;
    public Transform pen;

    private void FixedUpdate()
    {
        if (start == 1)
            pen.Translate(0.3f, 0, 0);
    }
    public void MainScene()
    {
        StartCoroutine(LoadScene(mainScene));
        start = 1;
        audioSource.PlayOneShot(audioClip);
        if (Music.instance != null)
            Music.instance.gameObject.SetActive(true);
    }
    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");

        audioSource.PlayOneShot(audioClip);
    }
    IEnumerator LoadScene(string st)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(st);
    }
}
