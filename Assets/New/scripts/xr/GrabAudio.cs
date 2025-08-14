using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAudio : MonoBehaviour
{
    //dialogue stuff
    public AudioSource soulAudio;
    public AudioClip sailor;
    public AudioClip ash;
    public AudioClip diary;
    public AudioClip diaryOpened;
    public AudioClip stickyCompliment1;
    public AudioClip stickyCompliment2;
    public AudioClip stickyCompliment3;
    public AudioClip stickyCompliment4;

    //nonempathy
    public AudioClip spider;
    public AudioClip flower;
    public AudioClip eyeballs;
    public AudioClip kettle;
    public AudioClip chibi;
    public AudioClip cross;

    //make audio not play for some time
    private bool hasBeenPlayed = false;
    private int complimented = 0;



    public void AudioAsh()
    {
        if (hasBeenPlayed) return;
        soulAudio.clip = ash;
        soulAudio.Play();
        hasBeenPlayed = true;
        StartCoroutine(ResetPlayFlagAfterDelay(10f));
    }

    public void AudioSailor()
    {
        if (hasBeenPlayed) return;
        
        soulAudio.clip = sailor;
        soulAudio.Play();
        hasBeenPlayed = true;
        StartCoroutine(ResetPlayFlagAfterDelay(10f));

    }


    public void AudioDiary()
    {
        if (hasBeenPlayed) return;
        soulAudio.clip = diary;
        soulAudio.Play();
        hasBeenPlayed = true;
        StartCoroutine(ResetPlayFlagAfterDelay(10f));
    }
    public void AudioSticky() //this should be the kettle one
    {
        if (hasBeenPlayed) return;
        soulAudio.clip = kettle;
        soulAudio.Play();
        hasBeenPlayed = true;
        StartCoroutine(ResetPlayFlagAfterDelay(10f));
    }

    public void AudioDiaryOpened()
    {
       
        soulAudio.clip = diaryOpened;
        soulAudio.Play();
        hasBeenPlayed = true;
        StartCoroutine(ResetPlayFlagAfterDelay(10f));
    }
    public void AudioNoteCompliment()
    {
        switch (complimented)
        {

            case 3: /*4th comliment*/
                print("no fr now. stop.");
                soulAudio.clip = stickyCompliment1;
                soulAudio.Play();
                break;
            case 2: /*3rd compliment*/
                print("Oh my u must have fallen for me if you're leaning into complimenting me this much ehehe");
                soulAudio.clip = stickyCompliment2;
                soulAudio.Play();
                break;
            case 1: /*2nd compliment*/
                print("Stop itt! but no really keep going");
                soulAudio.clip = stickyCompliment3;
                soulAudio.Play();
                break;
            default: /*first compliment*/
                print("Haha you really know how to butter me up. But thank you. I think I needed that.");
                soulAudio.clip = stickyCompliment4;
                soulAudio.Play();
                break;
        }

        soulAudio.Play();

        if (complimented < 3)
            complimented++;
    }


    //non empathy
    public void AudioSpider()
    {
        if (hasBeenPlayed) return;

        soulAudio.clip = spider;
        soulAudio.Play();
        hasBeenPlayed = true;
        StartCoroutine(ResetPlayFlagAfterDelay(10f));

    }

    public void AudioFlower()
    {
        if (hasBeenPlayed) return;

        soulAudio.clip = flower;
        soulAudio.Play();
        hasBeenPlayed = true;
        StartCoroutine(ResetPlayFlagAfterDelay(10f));

    }

    public void AudioEyeballs()
    {
        if (hasBeenPlayed) return;

        soulAudio.clip = eyeballs;
        soulAudio.Play();
        hasBeenPlayed = true;
        StartCoroutine(ResetPlayFlagAfterDelay(10f));

    }

    public void AudioKettle() //decided to stick with when user takes the sticky notes instead of approaching kette
    {
        if (hasBeenPlayed) return;

        soulAudio.clip = kettle;
        soulAudio.Play();
        hasBeenPlayed = true;
        StartCoroutine(ResetPlayFlagAfterDelay(10f));

    }

    public void AudioChibi()
    {
        if (hasBeenPlayed) return;

        soulAudio.clip = chibi;
        soulAudio.Play();
        hasBeenPlayed = true;
        StartCoroutine(ResetPlayFlagAfterDelay(10f));

    }

    public void AudioCross()
    {
        if (hasBeenPlayed) return;

        soulAudio.clip = cross;
        soulAudio.Play();
        hasBeenPlayed = true;
        StartCoroutine(ResetPlayFlagAfterDelay(10f));

    }

    IEnumerator ResetPlayFlagAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasBeenPlayed = false;
    }
}
