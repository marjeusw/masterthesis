//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//Ending Manager Script in which the score for the ending logic is kept, as well as checking for which ending it is (when the soul object enters the screen),
//as well as what happens in that ending (playing the video and fading to white or black afterwards depending on which ending it is)
//Good ending if 2, bad ending if less than 2

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

    //check for candles (if this is the first run) - since the candles will be turned off if its not
    public static bool candles;

    //soul disappear after ending (so not in fade away)
    public GameObject Soul;

    void Start()
    {
        score = 0;
    }

    public void IncreaseScoreBy2() //for shmailor shmoon
    {
        score += 2;
        Debug.Log("Score is 2 more");
        nothingUsed = false;
    }

    public void IncreaseScoreBy1() //this interactive spot is also very unique so it gets its own function
    {

        //notes
        
            score += 1;
            Debug.Log("Score is 1 more");
            nothingUsed = false;
          
        
        
    } 
    public void IncreaseDiaryBy1() //had to add this since the diary is behaving differently due to its feature of being an interactive spot
    {
        if (noLock.useOnce == true)
        {
            score += 1;
            Debug.Log("Score is 1 more");
            nothingUsed = false;
            noLock.useOnce = true;

        }
    }

    public void DecreaseScoreBy1() //after diary is opened
    {
        score -= 1; //maybe later I'll make this -2 to be an actual punishment since rn its just making the diary not count as a found object
        Debug.Log("Score is 1 less");
        nothingUsed = false;
    }
    

    void Update() //lets me check the score for debugging and is just nice to have
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


    public void GoodEnding()
    {
        goodEnding = true;
        //for the back
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
        //for the back
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
        yield return new WaitForSeconds(3f); // prevent immediate scene switch
        Debug.Log("coroutine end");
        isPlayed = false;
        Soul.SetActive(false);

        if (good)
            BackGood();
        else
            BackBad();
    }
}
