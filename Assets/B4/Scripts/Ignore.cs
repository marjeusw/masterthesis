using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//3 times because I have 3 scripts attached to 3 objects
public class Ignore : MonoBehaviour
{
    public UnityEvent OffCollider;
    public UnityEvent OnCollider;

    public GameObject soulOut;
    public Material materialIgnore;
    public Material soulNormal;

    bool isInteractable;
    bool isNotInteractable;
    bool waitingTimeGoing;
    bool ignoreDone;

    // Start is called before the first frame update
    void Start()
    {
        isInteractable = true;
        isNotInteractable = false;
        waitingTimeGoing = false;
        ignoreDone = false;
        //StartWaitingTime();
    }

    // Update is called once per frame
    void Update()
    {
        //StopCoroutine(WaitingTime());
        //Debug.Log("coroutine Stopped");
        if (!waitingTimeGoing)
        {
            Debug.Log("started new coroutine");
            StartCoroutine(WaitingTime());

        }
    }

    void OnTriggerEnter(Collider other)
    {
        //different collider cause otherwise script 3 times and now just 1 on empty gameobject
        //isInteractable = false;

        //this.GetComponent<Collider>().enabled = false;
        //hasNotInteracted = false; //changes nothing
        isInteractable = true;
        isNotInteractable = false;
        Debug.Log("plsWorkahh"); //little note: I don't disable collider after using (like other 3) because it just sets bools back 
        //IgnoreSpeech();
        //maybe do ignore speech if no collision happened here


    }

    public void StartWaitingTime()
    {
        //waitingTimeGoing = true; //might need that b4 though
        //if (!waitingTimeGoing)
        //{
        //StopCoroutine(WaitingTime());
        //Debug.Log("coroutine Stopped");
        //if (!waitingTimeGoing)
        //{
            StartCoroutine(WaitingTime());

        //}
        //waitingTimeGoing = true; //might need that b4 though
        //}
    }

    void IgnoreSpeech()
    {

        //know if other scripts did the same and if theres at least on option 2 then don't
        if (isNotInteractable && !isInteractable) //kinda redundant to ask here again but meh
        {
            Debug.Log("Ignore");
            OffCollider.Invoke();
            soulOut.GetComponent<SkinnedMeshRenderer>().material = materialIgnore;
        }
        //StopCoroutine(WaitingTime());
        //StartWaitingTime();
    }

    IEnumerator WaitingTime()
    {
        waitingTimeGoing = true;
        Debug.Log("waiting time started");
        //maybe set is interactable to true?
        if (isInteractable)
        {
            isInteractable = false; //immediately set false to get collision to change to true before time is up
            isNotInteractable = true;
            //make collider appear here?
            Debug.Log("Ignore?");
            if (!ignoreDone)
            {
                ColliderInteractable();
            }
            else
            {
                yield return new WaitForSeconds(8);
                ignoreDone = false;
                waitingTimeGoing = false;
                OnCollider.Invoke();
                //soulOut.GetComponent<SkinnedMeshRenderer>().material = soulNormal;
                isInteractable = true;
                Debug.Log("Collider interactable again");
            }

            yield return new WaitForSeconds(8);
            Debug.Log("this works");           
        }
        Debug.Log("Waiting Time ended");
        waitingTimeGoing = false;

        Decision();
        //if (isNotInteractable && !isInteractable)
        //{
        //    IgnoreSpeech();
        //    Debug.Log("option1");
        //}
        //else
        //{
        //    StartWaitingTime();
        //    Debug.Log("option2");
        //}
        //maybe there needs to be something that says turn blue here
        //last step make it repeat
        yield break;
    }

    public void ColliderInteractable()
    {
        OnCollider.Invoke();
        soulOut.GetComponent<SkinnedMeshRenderer>().material = soulNormal;
        Debug.Log("Collider interactable");
        isInteractable = true;
        isNotInteractable = false; //challenge: turns blue after coloured reaction anyways
    }

    public void Decision()
    {
        Debug.Log("Goes to decision");
        if (isNotInteractable && !isInteractable)
        {
            Debug.Log("option1");
            waitingTimeGoing = true;
            IgnoreSpeech(); //should only do it once for now
            //for later
            waitingTimeGoing = false;
            isNotInteractable = false;
            isInteractable = true;
            Debug.Log("Done ignoring");
            ignoreDone = true;
        }
        else
        {
            waitingTimeGoing = false;
            ignoreDone = false; // if set to false or true here once then it will always be that
            //StartWaitingTime();
            Debug.Log("option2");
        }
    }
}
