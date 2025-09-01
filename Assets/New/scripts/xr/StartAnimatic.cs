//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//Script that handles the animatic (Tutorial sequence)
//basically sets the tutorial writing on the canvas to false so that theres nothing attention grabbing besides the tutorial, starts the animatic, afterwards makes the screen go down,
//lastly makes the line to the now uncovered portal appear

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartAnimatic : MonoBehaviour
{
    public VideoPlayer animatic;
    public bool isPlayed = false;

    public nextLine line;

    public float time;

    //for going down
    public Animator animaticScreen;


    
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("collider entered");
        if (other.gameObject.CompareTag("Hand") && isPlayed == false)
        {
            Animatic();
        }

    }

    public void Animatic()
    {
        Debug.Log("animaticStart");
        line.Canvas.SetActive(false);
        animatic.Play();
        isPlayed = true;
        StartCoroutine(waitForAnimatic());
        return;
    }

    IEnumerator waitForAnimatic()
    {
        Debug.Log("routinestart");
        Debug.Log("Waiting before checking video state...");
        yield return new WaitForSeconds(time); //wait first for good measure (otherwise it'll fade out immediately)
        yield return new WaitUntil(() => !animatic.isPlaying);
        animaticScreen.Play("animaticdown");
        yield return new WaitForSeconds(3f); //prevents immediate sceneswitch
        Debug.Log("coroutine end");
        isPlayed = false;

        line.LineToPortal(); //sets line to the portal to true when the animatic is done
    }


   

}
