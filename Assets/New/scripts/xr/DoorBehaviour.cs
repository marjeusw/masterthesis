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
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
                grayscale.EnterGreyscale();//animator.Play("doorOpenFull"); //do this after greyscale scene ends instead
                //function for dialogue saying "are you sure?"
                isSure = true; //since afterwards they know they can't go back
                Debug.Log("Try2");

                //soul appear
                soul.SetActive(true);
                particleSoul.Play();
                //endingManager.CheckForEnding(); //also do this after greyscale
            }
            else
            {
                animator.Play("doorClosed");
                isSure = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
