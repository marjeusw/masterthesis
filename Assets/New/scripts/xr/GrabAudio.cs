using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAudio : MonoBehaviour
{
    //dialogue stuff
    public AudioSource soulAudio;
    public AudioClip sailor;
    public AudioClip ash;
    public AudioClip sticky;
    public AudioClip diary;
    public AudioClip diaryOpened;
    public AudioClip stickyCompliment;

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
    public void AudioSticky()
    {
        if (hasBeenPlayed) return;
        soulAudio.clip = sticky;
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
                soulAudio.clip = diary;
                soulAudio.Play();
                break;
            case 2: /*3rd compliment*/
                print("Oh my u must have fallen for me if you're leaning into complimenting me this much ehehe");
                soulAudio.clip = ash;
                soulAudio.Play();
                break;
            case 1: /*2nd compliment*/
                print("Stop itt! but no really keep going");
                soulAudio.clip = sailor;
                soulAudio.Play();
                break;
            default: /*first compliment*/
                print("Haha you really know how to butter me up. But thank you. I think I needed that.");
                soulAudio.clip = stickyCompliment;
                soulAudio.Play();
                break;
        }

        soulAudio.Play();

        if (complimented < 3)
            complimented++;
    }

    IEnumerator ResetPlayFlagAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasBeenPlayed = false;
    }
}
