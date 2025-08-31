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


    public GameObject reloadSpider, reloadFlower, reloadEyeballs, reloadChibi, reloadCross, reloadSailor, reloadAsh, reloadDiary /*reloadKettle, reloadSticky*/;

    //bool for objects to not play twice
    private bool noMoreSpider, noMoreFlower, noMoreEyeballs, noMoreChibi, noMoreCross, noMoreSailor, noMoreAsh, noMoreDiary/*, noMoreKettle, noMoreSticky*/;

    //make audio not play for some time
    //private bool hasBeenPlayed = false;

    //had chatgpt help make this script way cleaner
    private bool audioLocked = false;
    private bool opened = false; //just for the diary after being opened so the sound repeats as opened audio but won't wait until the dialogue is finished anymore
    private int complimented = 0;


    private void PlayAudio(AudioClip clip, float lockDuration = -1f)
    {
        if (audioLocked) return;

        soulAudio.clip = clip;
        soulAudio.Play();

        if (lockDuration > 0f)
            StartCoroutine(UnlockAfterSeconds(lockDuration));
    }

    public void AudioAsh() => PlayAudio(ash, 4f);
    public void AudioSailor() => PlayAudio(sailor, 4f);
    public void AudioDiary()
    {
        if (opened == false)
        {
            PlayAudio(diary, 4f);
        }
        else if(opened == true)
        {
            PlayAudio(diaryOpened);
        }
    }
    public void AudioSticky() => PlayAudio(kettle, 4f);
    public void AudioSpider() => PlayAudio(spider, 4f);
    public void AudioFlower() => PlayAudio(flower, 4f);
    public void AudioEyeballs() => PlayAudio(eyeballs, 4f);
    public void AudioKettle() => PlayAudio(kettle, 4f);
    public void AudioChibi() => PlayAudio(chibi, 4f);
    public void AudioCross() => PlayAudio(cross, 4f);

    public void AudioNoteCompliment()
    {
        //if (audioLocked) return; //(since I want the compliments to play instantky)

        AudioClip complimentClip = complimented switch
        {
            0 => stickyCompliment1,
            1 => stickyCompliment2,
            2 => stickyCompliment3,
            _ => stickyCompliment4
        };

        soulAudio.clip = complimentClip;
        soulAudio.Play();
        // Only increment if user hasn't reached the last compliment
        if (complimented < 3)
            complimented++;
    }

    public void AudioDiaryOpened()
    {
        StopAllCoroutines();      // Cancel any unlock timers
        //if (audioLocked) return;
        audioLocked = true;      // Reset lock

        //audioLocked = true; // Lock *everything*
        soulAudio.Stop();         // Stop currently playing audio
        soulAudio.clip = diaryOpened;
        soulAudio.Play();
        StartCoroutine(UnlockAfterDiaryOpened());
    }

    private IEnumerator UnlockAfterDiaryOpened()
    {
        // Wait until this specific audio finishes
        yield return new WaitUntil(() => !soulAudio.isPlaying);
        yield return new WaitForSeconds(0.5f); // Optional gap
        audioLocked = false;
        opened = true; //for the diary after being opened
    }

    private IEnumerator UnlockAfterSeconds(float seconds)
    {
        audioLocked = true;
        yield return new WaitForSeconds(seconds);
        audioLocked = false;
    }


    //for one time use for objects

    public void AudioAshObject() //make reload appear
     {
        if (noMoreAsh == false)
        {
            PlayAudio(ash, 2f);
            reloadAsh.SetActive(true);
            noMoreAsh = true;
        }
    }
    public void AudioSailorObject()
    {
        if (noMoreSailor == false)
        {
            PlayAudio(sailor, 2f);
            reloadSailor.SetActive(true);
            noMoreSailor = true;
        }
    }
    public void AudioDiaryObject()
    {
        if (noMoreDiary == false)
        {
            
            if (opened == false)
            {
                PlayAudio(diary, 2f);
            }
            else if (opened == true)
            {
                PlayAudio(diaryOpened);
            }
            reloadDiary.SetActive(true);
            noMoreDiary = true;
        }
    }

    //sticky maybe work for later
    //public void AudioStickyObject()
    //{
    //    if (noMoreKettle == false)
    //    {
    //        PlayAudio(kettle, 1f);
    //        reloadKettle.SetActive(true);
    //        noMoreKettle = true;
    //    }
    //}
    public void AudioSpiderObject()
    {
        if (noMoreSpider == false)
        {
            PlayAudio(spider, 1f);
            reloadSpider.SetActive(true);
            noMoreSpider = true;
        }
    }
    public void AudioFlowerObject()
    {
        if (noMoreFlower == false)
        {
            PlayAudio(flower, 1f);
            reloadFlower.SetActive(true);
            noMoreFlower = true;
        }
    }
    public void AudioEyeballsObject()
    {
        if (noMoreEyeballs == false)
        {
            PlayAudio(eyeballs, 1f);
            reloadEyeballs.SetActive(true);
            noMoreEyeballs = true;
        }
    }

    //kettle maybe work for later as with sticky
    //public void AudioKettleObject()
    //{
    //    if (noMoreKettle == false)
    //    {
    //        PlayAudio(kettle, 1f);
    //        reloadKettle.SetActive(true);
    //        noMoreKettle = true;
    //    }
    //}
    public void AudioChibiObject()
    {
        if (noMoreChibi == false)
        {
            PlayAudio(chibi, 1f);
            reloadChibi.SetActive(true);
            noMoreChibi = true;
        }
    }
    public void AudioCrossObject()
    {
        if (noMoreCross == false)
        {
            PlayAudio(cross, 1f);
            reloadCross.SetActive(true);
            noMoreCross = true;
        }
    }
}

    //public void AudioAsh()
    //{
    //    if (hasBeenPlayed) return;
    //    soulAudio.clip = ash;
    //    soulAudio.Play();
    //    hasBeenPlayed = true;
    //    StartCoroutine(ResetPlayFlagAfterDelay(10f));
    //}

    //public void AudioSailor()
    //{
    //    if (hasBeenPlayed) return;

    //    soulAudio.clip = sailor;
    //    soulAudio.Play();
    //    hasBeenPlayed = true;
    //    StartCoroutine(ResetPlayFlagAfterDelay(10f));

    //}


    //public void AudioDiary()
    //{
    //    if (hasBeenPlayed) return;
    //    soulAudio.clip = diary;
    //    soulAudio.Play();
    //    hasBeenPlayed = true;
    //    StartCoroutine(ResetPlayFlagAfterDelay(10f));
    //}
    //public void AudioSticky() //this should be the kettle one
    //{
    //    if (hasBeenPlayed) return;
    //    soulAudio.clip = kettle;
    //    soulAudio.Play();
    //    hasBeenPlayed = true;
    //    StartCoroutine(ResetPlayFlagAfterDelay(10f));
    //}

    //public void AudioDiaryOpened()
    //{

    //    soulAudio.clip = diaryOpened;
    //    soulAudio.Play();
    //    hasBeenPlayed = true;
    //    StartCoroutine(DiaryOpened());
    //}
    //public void AudioNoteCompliment()
    //{
    //    switch (complimented)
    //    {

    //        case 3: /*4th comliment*/
    //            print("no fr now. stop.");
    //            soulAudio.clip = stickyCompliment1;
    //            soulAudio.Play();
    //            break;
    //        case 2: /*3rd compliment*/
    //            print("Oh my u must have fallen for me if you're leaning into complimenting me this much ehehe");
    //            soulAudio.clip = stickyCompliment2;
    //            soulAudio.Play();
    //            break;
    //        case 1: /*2nd compliment*/
    //            print("Stop itt! but no really keep going");
    //            soulAudio.clip = stickyCompliment3;
    //            soulAudio.Play();
    //            break;
    //        default: /*first compliment*/
    //            print("Haha you really know how to butter me up. But thank you. I think I needed that.");
    //            soulAudio.clip = stickyCompliment4;
    //            soulAudio.Play();
    //            break;
    //    }

    //    soulAudio.Play();

    //    if (complimented < 3)
    //        complimented++;
    //}


    ////non empathy
    //public void AudioSpider()
    //{
    //    if (hasBeenPlayed) return;

    //    soulAudio.clip = spider;
    //    soulAudio.Play();
    //    hasBeenPlayed = true;
    //    StartCoroutine(ResetPlayFlagAfterDelay(10f));

    //}

    //public void AudioFlower()
    //{
    //    if (hasBeenPlayed) return;

    //    soulAudio.clip = flower;
    //    soulAudio.Play();
    //    hasBeenPlayed = true;
    //    StartCoroutine(ResetPlayFlagAfterDelay(10f));

    //}

    //public void AudioEyeballs()
    //{
    //    if (hasBeenPlayed) return;

    //    soulAudio.clip = eyeballs;
    //    soulAudio.Play();
    //    hasBeenPlayed = true;
    //    StartCoroutine(ResetPlayFlagAfterDelay(10f));

    //}

    //public void AudioKettle() //decided to stick with when user takes the sticky notes instead of approaching kette
    //{
    //    if (hasBeenPlayed) return;

    //    soulAudio.clip = kettle;
    //    soulAudio.Play();
    //    hasBeenPlayed = true;
    //    StartCoroutine(ResetPlayFlagAfterDelay(10f));

    //}

    //public void AudioChibi()
    //{
    //    if (hasBeenPlayed) return;

    //    soulAudio.clip = chibi;
    //    soulAudio.Play();
    //    hasBeenPlayed = true;
    //    StartCoroutine(ResetPlayFlagAfterDelay(10f));

    //}

    //public void AudioCross()
    //{
    //    if (hasBeenPlayed) return;

    //    soulAudio.clip = cross;
    //    soulAudio.Play();
    //    hasBeenPlayed = true;
    //    StartCoroutine(ResetPlayFlagAfterDelay(10f));

    //}

    //IEnumerator ResetPlayFlagAfterDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    hasBeenPlayed = false;
    //}


    //IEnumerator DiaryOpened()
    //{
    //    yield return new WaitUntil(() => !soulAudio.isPlaying);
    //    yield return new WaitForSeconds(0.5f);
    //    hasBeenPlayed = false;
    //}
//}