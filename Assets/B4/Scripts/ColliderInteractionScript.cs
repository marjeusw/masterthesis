using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderInteractionScript : MonoBehaviour
{
    public GameObject soulOut;
    public Material materialInteraction;

    
    //public Material materialIgnore; //doesn't need collider; just coroutine
    public Material soulNormal;

    bool isTalking;
    //public bool hasNotInteracted;
    //public bool isInteractable;

    public UnityEvent OffCollider;
    public UnityEvent OnCollider;

    // Start is called before the first frame update
    void Start()
    {
        ////hasNotInteracted = true;
        //isInteractable = true;
        
        //if (/*hasNotInteracted*/ isInteractable)
        //{
        //    WaitingTime();
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    //void WaitingTime()
    //{
    //    if (/*hasNotInteracted*/ isInteractable)
    //    {
    //        StartCoroutine(WaitingForInteraction());
    //    }
    //    else
    //    {
    //        Debug.Log("notIgnored");
    //        speech();
    //        isInteractable = false; //might be in speech already but whatevr
    //    }
    //}

    void OnTriggerEnter(Collider other)
    {
        //isInteractable = false;
        Debug.Log("interaction");
        soulOut.GetComponent<SkinnedMeshRenderer>().material = materialInteraction;
        OffCollider.Invoke();
        //this.GetComponent<Collider>().enabled = false;
        //hasNotInteracted = false; //changes nothing

        speech();

        

    }

    //don't need cause trigger already disabled
    //void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("interaction Ended");
    //    soulOut.GetComponent<MeshRenderer>().material = soulNormal;
    //}

    public void speech()
    {
        StartCoroutine(TalkingCoroutine());

        

        // check when audio is done

        //do the on material ignore thingy here
        //isinteractable bool

        //Debug.Log("try appearing");
        //if (Input.GetKeyDown(KeyCode.KeypadEnter))
        //{ // just to see if input works
        //speak
        //speak end do this

        //this.GetComponent<Collider>().enabled = true;
        //OnCollider.Invoke();
        //soulOut.GetComponent<MeshRenderer>().material = soulNormal; //oesn't wanna do this
        //Debug.Log("appeared");
        //}
    }

    public void ColliderAppear()
    {
        OnCollider.Invoke();
        //soulOut.GetComponent<MeshRenderer>().material = soulNormal;
        Debug.Log("appeared");
    }

    IEnumerator TalkingCoroutine()
    {
        Debug.Log("Talking started");
        isTalking = true;
        //isInteractable = false;

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(8);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Talking");
        isTalking = false; //immediately goes into this
        Debug.Log("istalking false");

        if (!isTalking)
        {
            Debug.Log("nowinteractable");
            soulOut.GetComponent<SkinnedMeshRenderer>().material = soulNormal;
            ColliderAppear();
            //isInteractable = true;
        }
        yield break; //don't know if that does something
    }

    //IEnumerator WaitingForInteraction()
    //{
    //    yield return new WaitForSeconds(5); 
        
    //    //if interacted in that time hopefully jumps to ontriggerenter
    //    //if (/*hasNotInteracted*/ isInteractable) //turns blue the first time //even after reaction; doesn't turn blue the other times //need help with logic 
    //    //{
    //    //    soulOut.GetComponent<MeshRenderer>().material = materialIgnore;
    //    //    OffCollider.Invoke();
    //    //    Debug.Log("Ignored");
    //    //    speech();
    //    //    isInteractable = false;
    //    //}
    //    //else if(/*!hasNotInteracted*/ !isInteractable)
    //    //{
    //    //    Debug.Log("notIgnored");
    //    //    speech();
    //    //    isInteractable = false; //might be in speech already but whatevr
    //    //}

    //    if (/*hasNotInteracted*/ isInteractable) //turns blue the first time //even after reaction; doesn't turn blue the other times //need help with logic 
    //    {
    //        soulOut.GetComponent<MeshRenderer>().material = materialIgnore;
    //        OffCollider.Invoke();
    //        Debug.Log("Ignored");
    //        speech();
    //        isInteractable = false;
    //    }

    //    //yield break; //don't know if that does something
    //}
}
