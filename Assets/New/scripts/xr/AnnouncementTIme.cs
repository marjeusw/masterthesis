using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncementTIme : MonoBehaviour
{
    public AudioSource LimboJam;
    public AudioSource LimboAnnouncement;
    public AudioClip announcementClip;
    public PickSoul soul;
    public bool isAnnounced;
    public bool announcementFinished = false;

    //turn off hands
    public Material originalHand;
    public Material standbyHands;

    public Renderer rend;
    public Renderer rendr;
    // Start is called before the first frame update

   
    public void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("Hand") && EndingManager.candles == false)
        {
            Debug.Log("Triggered");
            OverseerAnnouncement();
        }
    }

    //for audio of explaining
    public void OverseerAnnouncement()
    {
        //if (LimboAnnouncement.isPlaying || isAnnounced) return;
        if (isAnnounced) return; // check this first!

        isAnnounced = true; // lock immediately!
        if (LimboAnnouncement.isPlaying)
        {
            Debug.Log("Tried to announce, but already playing");
            return;
        }

        Debug.Log("NoMore");
        LimboJam.Pause();
        rend.material = standbyHands;
        rendr.material = standbyHands;
        //hands set to standby until end of announcement (play coroutine) and then back to original
        //isAnnounced = true;

        LimboAnnouncement.clip = announcementClip;
        LimboAnnouncement.Play();
        

        StartCoroutine(waitForDialogue());

    }

    IEnumerator waitForDialogue()
    {
        Debug.Log("routinestart");
        yield return new WaitUntil(() => !LimboAnnouncement.isPlaying);
        yield return new WaitForSeconds(0.5f); // prevent immediate re-trigger
        Debug.Log("coroutine end");
        //isAnnounced = false; // allow new dialogue on next trigger
                             // DO NOT call Dialogue() again here!
        rend.material = originalHand;
        rendr.material = originalHand;

        announcementFinished = true; //for picksoulscript to do scenechange after
    }
}
