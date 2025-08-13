using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneChange : MonoBehaviour
{

    public void ButtonNewScene()
    {
        //SceneManager.LoadScene("SampleScene 1"); //lz switch between scenes
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
       // SceneManager.LoadScene("SampleScene");

    }
}
