//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//Script that checks for triggers tagged Hand, Punch, and Fog
//with it the dialogue of the soul, the ending screen when the soul enters it and the Fog get started

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartReview : MonoBehaviour
{
    public SoulDialogue dialogue;
    public StartEndingScreen screen;
    public AudioSource Fog;
    // Start is called before the first frame update
    void Start()
    {
        Try();
    }


    public void OnTriggerEnter(Collider other)
    {
        //for next dialogue of soul
        if (other.gameObject.CompareTag("Hand"))
        {
            Try();
        }

        //for screen outside
        if (other.gameObject.CompareTag("Punch"))
        {
            screen.StartScreen(); 
        }

        if (other.gameObject.CompareTag("Fog"))
        {
            Fog.Play();
            
        }

    }

    public void Try()
    {
        //Debug.Log("ow you hit me");
        dialogue.Dialogue();
    }

    
}
