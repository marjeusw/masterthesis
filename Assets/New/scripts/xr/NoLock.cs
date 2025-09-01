//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//script that handles the diary logic
//if the lock is touched then with the poke interactor that has the tag hand on it, it plays the animation and decreses score, if not but still foun (detected) then it just increases it

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class NoLock : MonoBehaviour
{
    
    public EndingManager endingManager;
    //for audio
    public GrabAudio audio;
    //
    public GameObject Lock;
    public Animator animator;
    public bool noLock = false;
    public bool trust = false;
    public bool useOnce =false;
    



    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            
            
            NoLockFunction();
            noLock = true;
        }
    }

    public void NoLockFunction()
    {
       
        Lock.SetActive(false);
        animator.Play("Book_open");
        audio.AudioDiaryOpened();
        endingManager.DecreaseScoreBy1();

    }

    public void Detected()
    {
        //increase score in ending manager already done in inspector through grab interactable event
        trust = true;
        if (useOnce == false)
        {
            useOnce = true;
            endingManager.IncreaseDiaryBy1();
        }
        else
        {
            useOnce = true;
        }
        Debug.Log("detected");
        
    }
}
