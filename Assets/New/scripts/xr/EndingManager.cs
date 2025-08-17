using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public int score;
    public bool nothingUsed = true;
    public NoLock noLock;
    public NoteCollide note;
    // Start is called before the first frame update


    //--------------------
    //endings
    public static bool goodEnding; //used also later to fade to white or black in ending
    public SceneFader fader;
    private bool isPlayed;
    public SwitchLayer layer;

    public VideoPlayer videoPlayer;
    public VideoClip clipGood;
    public VideoClip clipBad;

    public VideoPlayer videoPlayerFront;
    public VideoClip clipFrontGood;
    public VideoClip clipFrontBad;

    //check for candles if this is first run
    public static bool candles;

    //soul disappear after ending (so not in fase away)
    public GameObject Soul;

    void Start()
    {
        score = 0;
    }

    public void IncreaseScoreBy2()
    {
        score += 2;
        Debug.Log("Score is 2 more");
        nothingUsed = false;
    }

    public void IncreaseScoreBy1()
    {

        //notes
        
            score += 1;
            Debug.Log("Score is 1 more");
            nothingUsed = false;
          
        
        
    } 
    public void IncreaseDiaryBy1()
    {
        if (noLock.useOnce == true)
        {
            score += 1;
            Debug.Log("Score is 1 more");
            nothingUsed = false;
            noLock.useOnce = true;

        }
    }

    public void DecreaseScoreBy1()
    {
        score -= 1;
        Debug.Log("Score is 1 less");
        nothingUsed = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log(score);
        }
    }

    public void CheckForEnding()
    {
        //called when soul enters screen
        if(score >= 2)
        {
            GoodEnding();
        }
        else if(score < 2)
        {
            BadEnding();
        }
    }


    //reference in the greyscalescript the bools noteActivated and noLock. for noteActivated good dialogue about complimnents; for noLock activated bad dialogue about user not being trustworthy; trust dialogue about how user is trustworthy. if none activated && no empathy objects in basket (nothinUsed) -> dialogue about how no interest and bad ending
    public void GoodEnding()
    {
        goodEnding = true;
        videoPlayer.clip = clipGood;
        videoPlayer.Play();

        //for the front
        videoPlayerFront.clip = clipFrontGood;
        videoPlayerFront.Play();
        isPlayed = true;
        
        StartCoroutine(waitForVideoPlayer(true)); //good ending
        return;
    }

    public void BadEnding()
    {
        goodEnding = false;
        videoPlayer.clip = clipBad;
        videoPlayer.Play();

        //for the front
        videoPlayerFront.clip = clipFrontBad;
        videoPlayerFront.Play();
        isPlayed = true;
        
        StartCoroutine(waitForVideoPlayer(false)); //bad ending
        return;
    }

    public void BackBad()
    {
        layer.DefaultLayer();
        fader.FadeAndLoad("candles", 1f);
        //black screen, respawn to graves with candles out
        candles = true;
    }

    public void BackGood()
    {
        //inhere
        layer.DefaultLayer();
        
        fader.FadeAndLoad("candles", 1f);

        candles = true;

        //white screen, respawn to graves with candles out
    }

    IEnumerator waitForVideoPlayer(bool good)
    {
        Debug.Log("routinestart");
        Debug.Log("Waiting before checking video state...");
        yield return new WaitForSeconds(15f); // <-- Wait 15 seconds first for good measure (otherwise it'll fade out immediately)
        yield return new WaitUntil(() => !videoPlayer.isPlaying); //waits till video in the back is ended (since one front video is always looping due to time constraints  no full animation
        yield return new WaitForSeconds(3f); // prevent immediate sceneswitch
        Debug.Log("coroutine end");
        isPlayed = false;
        Soul.SetActive(false);

        if (good)
            BackGood();
        else
            BackBad();
    }
}
