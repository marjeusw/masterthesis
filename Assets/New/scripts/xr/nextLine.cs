//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//Script that manages the tutorial elements like the line and text appearing after some time and what happens when interacting with the sphere and cube as well as what happens after returning there from the 1st loop

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class nextLine : MonoBehaviour
{
    public GameObject AnimaticLine;
    public GameObject PortalLine;
    public GameObject LimboDimension; //graves and stuff
    public GameObject TutorialDimension; //so the colliders won't affect anything when going back
    public GameObject Ball;
    public GameObject Dice;


    public GameObject Tutorial1;
    public GameObject Tutorial2;
    public GameObject Canvas;
    public GameObject PortalPlane;
    public GameObject CylinderVideo;

    //for audio
    public AudioSource LimboJam;


    //for line appear after video
    public VideoPlayer animatic;

  
    
    public void LineToAnimatic()
    {
        if (EndingManager.candles == false)
        {


            //turn off ball and dice
            Ball.SetActive(false);
            Dice.SetActive(false);
            //turn off text and turn on next text
            Tutorial1.SetActive(false);
            Tutorial2.SetActive(true);
            //turn on line
            AnimaticLine.SetActive(true);
        }
        else
        {
            SecondLoop();
        }

    }

    public void LineToPortal()
    {
        PortalLine.SetActive(true);
        PortalPlane.SetActive(true);
        CylinderVideo.SetActive(false);
        
    }

    public void LimboGone() //When stepping through the portal this gets called (since otherwise the colliders of the terrain would be annoying)
    {
        LimboDimension.SetActive(false);
        TutorialDimension.SetActive(true);
        LimboJam.Pause();
    }

    public void LimboBack() //when stepping back through the portal this gets called (so colliders from this side don't interfere either)
    {
        LimboDimension.SetActive(true);
        TutorialDimension.SetActive(false);
        LimboJam.Play();
    }

   
    //for after 1st run
    public void SecondLoop()
    {
        //turn off ball and dice
        Ball.SetActive(false);
        Dice.SetActive(false);
        //turn off canvas image and children
        Canvas.SetActive(false);
        //turn on line
        AnimaticLine.SetActive(true);

    }

}
