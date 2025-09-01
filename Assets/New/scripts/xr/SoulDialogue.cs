//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//TINY BIT OF HELP FROM CHATGPT HERE (I can into an infinite loop at the end of the script that I fixed with its help)
//Script that handles all the dialogue in the end for the objects the user found
//I decided to make the user poke the soul object each time for a new line to make it not as boring to just sit and wait while the witch is talking but I may change that at a later date
//Its basically a lot of if else statements that all go to a coroutine in which the hands are grayed out to show the user can't interact while the soul is talking
//it culminates with the soul walking out

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDialogue : MonoBehaviour
{
    public NoLock noLock;
    public ReferenceBools referenceSailorAndAsh;
    public NoteCollide note;
    public EndingManager ending;

    private bool end = false;
    public Animator animator;
    public Animator soulAnimator;
    //forsoulmodel
    public Animator anims;
    

    public GameObject sticky;
    public GameObject diary;
    public GameObject ash;
    public GameObject sailorMoon;

    public GameObject stickyReal;
    public GameObject stickyReal2;
    public GameObject stickyReal3;
    public GameObject stickyReal4;
    public GameObject diaryReal;
    public GameObject ashReal;
    public GameObject sailorMoonReal;
    //bool speaking;

    //audio dialogue stuff
    public AudioSource soulAudio;
    public AudioClip ashClip;
    public AudioClip sailorClip;
    public AudioClip diaryOpenClip;
    public AudioClip diaryClosedClip;
    public AudioClip stickyClip;
    public AudioClip nothinClip;
    public AudioClip okeyPokeyClip;
    public AudioClip getOutClip;
    private bool isPlayed;
    private bool hasPromptedUser = false;

    //handmat switch to indicate user has to wait and is on standby
   
    public Material originalHand;
    public Material standbyHands;

    public Renderer rend;
    public Renderer rendr;
    

    //for the switch statement lez do 0 for nothing dialogue (none activated && nothingUsed), 1 for ash (ash), 2 for sailor moon (sailor), 3 for notes compliments (noteActivated), 4 for diary unopened (bool trust), 5 for diary opened (noLock)
    //run through cases 1-6, pick one after checking, run through them again until all false
    //when all false go to final dialogue ("Welp Imma show you out of here, cmon follow me") and have soul leave room and open door fully animation, also after that do check ending function (when soul enters screen)
    // Start is called before the first frame update


    //make a funtion that wait for dialogue to end and then call this function again
    public void Dialogue()
    {
        if (end == false)
        {
            if (ending.nothingUsed == false && referenceSailorAndAsh.ash == false && referenceSailorAndAsh.sailor == false && note.noteActivated == false && noLock.trust == false && noLock.noLock == false)
            {
                if (soulAudio.isPlaying || isPlayed) return;

                rend.material = standbyHands;
                rendr.material = standbyHands;
                Debug.Log("Welp Imma show you out of here, cmon follow me");
                
                    soulAudio.clip = getOutClip;
                    soulAudio.Play();
                    
                    
                    //soulAudio.PlayOneShot(getOutClip);
                    isPlayed = true;

                    //other stuff
                    end = true;
                    EndDialogue();
                    //objects poof
                    sticky.SetActive(false);
                    diary.SetActive(false);
                    ash.SetActive(false);
                    sailorMoon.SetActive(false);

                    //make previous objects disappear too so they aren't seen through wall
                    stickyReal.SetActive(false);
                    stickyReal2.SetActive(false);
                    stickyReal3.SetActive(false);
                    stickyReal4.SetActive(false);
                    diaryReal.SetActive(false);
                    ashReal.SetActive(false);
                    sailorMoonReal.SetActive(false);
                    StartCoroutine(waitForDialogue());
                
           

        }
            else
            {
                if (soulAudio.isPlaying || isPlayed) return;

                if (!hasPromptedUser)
                {
                    rend.material = standbyHands;
                    rendr.material = standbyHands;
                    //happy maybe nodding soul animation play here (different animator from one above)
                    anims.Play("nod");
                    Debug.Log("you can poke me in case I forget something but lets review."); //for now just poking soul will trigger dialogue. maybe later let pillar which has a socket for user to place basket on.
                    soulAudio.clip = okeyPokeyClip;
                    soulAudio.Play();
                    isPlayed = true;
                    hasPromptedUser = true;
                    StartCoroutine(waitForDialogue());
                    return;
                }



                if (ending.nothingUsed == true)
                {
                    rend.material = standbyHands;
                    rendr.material = standbyHands;
                    //annoyed animation play here
                    anims.Play("angy");
                    Debug.Log("Oh wow. you really just wanted to get the hell out of here, huh? Well then, be my guest.");
                    //dialogue ended? then do if
                    
                    
                    
                    //soulAudio.PlayOneShot(nothinClip);
                    soulAudio.clip = nothinClip;
                    soulAudio.Play();
                    ending.nothingUsed = false;
                    isPlayed = true;
                    StartCoroutine(waitForDialogue());
                    return;


                }
                else if (noLock.noLock == true && noLock.trust == true)
                {
                    rend.material = standbyHands;
                    rendr.material = standbyHands;
                    //raging animation play here
                    anims.Play("rage");
                    Debug.Log("Oh so you think me saying ´don't touch that´ was just a suggestion? Okay mr. untrustworthy, Imma keep that in mind.");
                    
                    //soulAudio.PlayOneShot(diaryOpenClip);
                    soulAudio.clip = diaryOpenClip;
                    soulAudio.Play();
                    isPlayed = true;

                    //other stuff
                    diaryReal.SetActive(false);
                    //objects appear on top of soul when talked about
                    diary.SetActive(true);
                    noLock.noLock = false;
                    noLock.trust = false;
                    StartCoroutine(waitForDialogue());
                    return;


                }
                else if (referenceSailorAndAsh.ash == true)
                {
                    rend.material = standbyHands;
                    rendr.material = standbyHands;
                    //sad animation play here
                    anims.Play("sad");
                    //soulAudio.PlayOneShot(ashClip);
                    soulAudio.clip = ashClip;
                    soulAudio.Play();
                    isPlayed = true;

                    //other stuff
                    ashReal.SetActive(false);
                    ash.SetActive(true);
                    Debug.Log("I said I didn't want to talk about it. man you really can't read the room.. literally. Fine. Here's the story you so badly want to hear.");
                    referenceSailorAndAsh.ash = false;
                    StartCoroutine(waitForDialogue());
                    return;

                }
                else if (referenceSailorAndAsh.sailor == true)
                {
                    rend.material = standbyHands;
                    rendr.material = standbyHands;
                    //super happy animation play here
                    anims.Play("cheerHappy");
                    //soulAudio.PlayOneShot(sailorClip);
                    soulAudio.clip = sailorClip;
                    soulAudio.Play();
                    isPlayed = true;

                    //other stuff
                    sailorMoonReal.SetActive(false);
                    sailorMoon.SetActive(true);
                    Debug.Log("Heh. I love her. she's the best. got me through some real dark times. She showed me that witches can co exist with humans just fine nowadays. Maybe thats all the reassurance I needed. hm, maybe I am ready to move on after all");
                    referenceSailorAndAsh.sailor = false;
                    StartCoroutine(waitForDialogue());
                    return;

                }
                else if (note.noteActivated == true)
                {
                    rend.material = standbyHands;
                    rendr.material = standbyHands;
                    //bashful animation (maybe look around awkwardly) play here
                    anims.Play("bashful laugh"); 
                   
                    soulAudio.clip = stickyClip;
                    soulAudio.Play();
                    isPlayed = true;

                    //other stuff
                    stickyReal.SetActive(false);
                    stickyReal2.SetActive(false);
                    stickyReal3.SetActive(false);
                    stickyReal4.SetActive(false);
                    sticky.SetActive(true);
                    Debug.Log("Haha you really know how to butter me up. But thank you. I think I needed that.");
                    note.noteActivated = false;
                    StartCoroutine(waitForDialogue());
                    return;

                }
                else if (noLock.trust == true && noLock.noLock == false)
                {
                  
                    rend.material = standbyHands;
                    rendr.material = standbyHands;
                    //stuck in tracks stunned animation here
                    anims.Play("stunned");
                   
                    soulAudio.clip = diaryClosedClip;
                    soulAudio.Play();
                    isPlayed = true;

                    //other stuff
                    diaryReal.SetActive(false);
                    diary.SetActive(true);
                    Debug.Log("You actually respected my boundaries. I mean you would've been a total jerk if you didn't but I've encountered a lot of those in my lifetime. so.. thank you for not being one of them.");
                    noLock.trust = false;
                    noLock.noLock = false;
                    StartCoroutine(waitForDialogue());
                    return;

                }


                    // Default if nothing matched — poke reminder (will never be called but good to have back up)

                    rend.material = standbyHands;
                    rendr.material = standbyHands;
                    soulAudio.clip = okeyPokeyClip;
                    soulAudio.Play();
                    isPlayed = true;

                    StartCoroutine(waitForDialogue());
                    
                

                end = false;
            }
        }



        //wanted to make it a switch statement at first but realized I already have all the logic with booleans for if else so Imma do that instead
        //switch (found)
        //{
        //    case 5: /*audio file diary opened*/
        //        print("Oh so you think me saying ´don't touch that´ was just a suggestion? You fuckin fucker fuck you!");
        //        break;
        //    case 4: /*audio file diary not opened*/
        //        print("You actually respected my boundaries. I mean you would've been a total jerk if you didn't but I've encountered a lot of those in my lifetime. so.. thank you for not being one of them.");
        //        break;
        //    case 3: /*audio file sticky notes*/
        //        print("Haha you really know how to butter me up. But thank you. I think I needed that.");
        //        break;
        //    case 2: /*audio file shmailor boon*/
        //        print("Heh. I love her. she's the best. got me through some real dark times. She showed me that witches can co exist with humans just fine nowadays. Maybe thats all the reassurance I needed. hm, maybe I am ready to move on after all");
        //        break;
        //    case 1: /*audio file ash*/
        //        print("I said I didn't want to talk about it. man you really can't read the room.. literally. Fine. Here's the story you so badly want to hear.");
        //        break;
        //    default: /*audio file nothing found and interacted with*/
        //        print("Oh wow. you really just wanted to get the hell out of here, huh? Well then, be my guest.");
        //        break;

        //}
    }

    public void EndDialogue()
    {
        Debug.Log("WalkOut");
        animator.Play("doorOpenFull");
        soulAnimator.Play("WalkOut");
    }

   

    IEnumerator waitForDialogue()
    {
        Debug.Log("routinestart");
        yield return new WaitUntil(() => !soulAudio.isPlaying);
        yield return new WaitForSeconds(0.5f); // prevent immediate re-trigger
        Debug.Log("coroutine end");
        isPlayed = false; // allow new dialogue on next trigger
                          // DO NOT call Dialogue() again here!
        rend.material = originalHand;
        rendr.material = originalHand;
    }
    //enddialogue was fixed with the help of chatgpt since I created an infinite loop before. oopsie
    //only used it to look over it when I got stuck but did everything else myself
}
