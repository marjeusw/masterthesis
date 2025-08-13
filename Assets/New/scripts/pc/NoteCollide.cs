using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCollide : MonoBehaviour
{
    public EndingManager endingManager;
    //public Rigidbody rb;
    public PickUpScript pickUp;
    [HideInInspector]
    public bool noteActivated = false;


    //for audio
    public GrabAudio audio;
    

    [SerializeField]
    private GameObject noteSideOne;
    [SerializeField]
    private GameObject noteSideTwo;
    [SerializeField]
    private GameObject noteSideOne2;
    [SerializeField]
    private GameObject noteSideTwo2;
    [SerializeField]
    private GameObject noteSideOne3;
    [SerializeField]
    private GameObject noteSideTwo3;
    [SerializeField]
    private GameObject noteSideOne4;
    [SerializeField]
    private GameObject noteSideTwo4;
    [SerializeField]
    private ParticleSystem one;
    [SerializeField]
    private ParticleSystem two;
    [SerializeField]
    private ParticleSystem three;
    [SerializeField]
    private ParticleSystem four;

    private
    //public bool colliderHit;
    // Start is called before the first frame update
    void Start()
    {
        //rb = rb.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //pc logic
        //if (other.gameObject.tag == "StickyNote")
        //{
        //    //colliderHit = true;
        //    //rb.useGravity = true;
        //    if (pickUp.special)
        //    {
        //        //rb.useGravity = true;
        //        //rb.isKinematic = false;
        //        pickUp.GetComponent<Rigidbody>();
        //        pickUp.HeldObjRb.isKinematic = true;
        //        noteSideOne.SetActive(false);
        //        noteSideTwo.SetActive(false);
        //        Debug.Log("Cauldron entered");
        //    }
        //    else
        //    {
        //        //rb.useGravity = false;
        //        //rb.isKinematic = true;
        //        Debug.Log("Cauldron eexited");
        //    }
        //}

        if (other.gameObject.tag == "StickyNote")
        {
            Debug.Log("XRNoteVanish");
            noteSideOne.SetActive(false);
            noteSideTwo.SetActive(false);
            one.Play();
            audio.AudioNoteCompliment();
            if (noteActivated == false)
            {
                noteActivated = true;
                
            }
            else
            {
                noteActivated = false;
            }
            
        }

        if (other.gameObject.tag == "StickyNote2")
        {
            //2nd note
            noteSideOne2.SetActive(false);
            noteSideTwo2.SetActive(false);
            two.Play();
            audio.AudioNoteCompliment();
            if (noteActivated == false)
            {
                noteActivated = true;
            }
            else
            {
                noteActivated = false;
            }
        }

        if (other.gameObject.tag == "StickyNote3")
        {
            //3rd note
            noteSideOne3.SetActive(false);
            noteSideTwo3.SetActive(false);
            three.Play();
            audio.AudioNoteCompliment();
            if (noteActivated == false)
            {
                noteActivated = true;
            }
            else
            {
                noteActivated = false;
            }
        }

        if (other.gameObject.tag == "StickyNote4")
        {
            //4th note
            noteSideOne4.SetActive(false);
            noteSideTwo4.SetActive(false);
            four.Play();
            audio.AudioNoteCompliment();
            if (noteActivated == false)
            {
                noteActivated = true;
            }
            else
            {
                noteActivated = false;
            } //later for reference to count up the interactive spots (for ending); also making sure it counts up only once
        }

        if(noteActivated == true)
        {
            endingManager.IncreaseScoreBy1();
            //audio.AudioNoteCompliment(); if I just wanted it to be called once
            
        }
        else
        {
            noteActivated = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
