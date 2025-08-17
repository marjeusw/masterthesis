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

    //for after 1st run
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        //LimboDimension.SetActive(true); not yet
    }

    public void LimboGone() //since this ones annoying
    {
        LimboDimension.SetActive(false);
        TutorialDimension.SetActive(true);
        LimboJam.Pause();
    }

    public void LimboBack()
    {
        LimboDimension.SetActive(true);
        TutorialDimension.SetActive(false);
        LimboJam.Play();
    }

    //just for funsies after the 1st loop

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
