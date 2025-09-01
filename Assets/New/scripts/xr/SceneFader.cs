//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//Script that handles the fade in and out functions after the endings (good ending white fade in and out, bad end black fade in and out) that get called in the ending manager
//also handles the Title screen with the little logo in the beginning

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public Image imageBad;
    public Image imageGood;
    public Image titleScreen;


    private void Start()
    {
        TitleScreen();
    }

    //for the start Screen / after first playthrough
    public void TitleScreen()
    {
        if(EndingManager.candles == true)
        {
            if(EndingManager.goodEnding == true)
            {
                StartCoroutine(FadeOutGood());
             
            }
            else
            {
                StartCoroutine(FadeOutBad());
              
            }
        }
        else
        {
            StartCoroutine(FadeTitle());
        }
        
    }
    
    public void FadeAndLoad(string sceneName, float duration)
    {
        if (EndingManager.goodEnding == true)
        {
            StartCoroutine(FaderGood(sceneName, duration));
        }
        else 
        {
            StartCoroutine(FaderBad(sceneName, duration));
        }
    }

    IEnumerator FaderGood(string sceneName, float duration)
    {
        float t = 0;
        Color c = imageGood.color;
        while(t < duration)
        {
            t += Time.deltaTime;
            c.a = t / duration;
            imageGood.color = c;
            yield return null;
        }
        SceneManager.LoadScene("candles");
    }

    IEnumerator FadeOutGood()
    {
        float t = 0;
        Color c = imageGood.color;
        while (t < 1)
        {
            t += Time.deltaTime;
            c.a = 1f - (t / 1f);
            imageGood.color = c;
            yield return null;
        }
    }


    IEnumerator FaderBad(string sceneName, float duration)
    {
        float t = 0;
        Color c = imageBad.color;
        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = t / duration;
            imageBad.color = c;
            yield return null;
        }
        SceneManager.LoadScene("candles");
    }

    IEnumerator FadeOutBad()
    {
        float t = 0;
        Color c = imageGood.color;
        while (t < 1)
        {
            t += Time.deltaTime;
            c.a = 1f - (t / 1f);
            imageBad.color = c;
            yield return null;
        }
    }

    IEnumerator FadeTitle()
    {
        
        float t = 0;
        Color c = titleScreen.color;
        while (t < 1)
        {
            t += Time.deltaTime;
            c.a = 1f - (t / 1f);
            titleScreen.color = c;
            yield return null;
        }
    }
}
