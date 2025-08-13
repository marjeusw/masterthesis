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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(noLock == true)
        //{
        //    NoLockFunction();
        //}
    }



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
        //animator.GetComponent<AnimationClip>();
        //animator.Play("Book_open");
        
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
