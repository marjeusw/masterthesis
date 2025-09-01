//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//Script for the Door in the soul room 
//plays tiny door flick open animation for triggering the handle once, afterwards it makes the soul object appear and talk for the next stage
//if the door isn't triggered it keeps playing the idle animation of being closed


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorBehaviour : MonoBehaviour
{
    public EndingManager endingManager;
    public GrayScaleMode grayscale;
    public Animator animator;
    public bool isSure = false;
    public GameObject soul;
    public ParticleSystem particleSoul;

    public AudioSource soulAudio;
    public AudioClip sure;
    

    public void OnTriggerEnter(Collider other)
    {
        if (isSure == false)
        {
            if (other.gameObject.CompareTag("Hand"))
            {
                animator.Play("doorOpenHalf");
                //function for dialogue saying "are you sure?"
                isSure = true; //since afterwards they know they can't go back
                Debug.Log("try1");
                soulAudio.clip = sure;
                soulAudio.Play();
            }
            else
            {
                animator.Play("doorClosed");
                isSure = false;
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Hand"))
            {
                animator.Play("doorClosed");
                grayscale.EnterGreyscale();
                //function for dialogue saying "are you sure?"
                isSure = true; //since afterwards they know they can't go back
                Debug.Log("Try2");

                //soul appear
                soul.SetActive(true);
                particleSoul.Play();
               
            }
            else
            {
                animator.Play("doorClosed");
                isSure = true;
            }
        }
    }

  
}
