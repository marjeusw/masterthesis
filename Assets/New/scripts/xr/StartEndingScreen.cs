//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//script that initiates the check for ending fuction from the ending manager so that the screens play the good or bad ending, makes the house vanish after the soul enters the screen (after walking out)
//also sets both screens to a new layer so that they are still in colour (thats why the hands can't be seen through it)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndingScreen : MonoBehaviour
{
    public EndingManager manager;
    public GameObject screen;
    public GameObject screenFront;
    public GameObject House;
   

    public void StartScreen()
    {
        House.SetActive(false);
        NewLayer();
        Debug.Log("ScreenEntered");
        manager.CheckForEnding();
    }

    public void NewLayer()
    {
        //for back screen
        int LayerNoPost = LayerMask.NameToLayer("NoPost");
        screen.layer = LayerNoPost;
        Debug.Log("new layer");

        //for front screen
        int LayerNoPostFront = LayerMask.NameToLayer("NoPost");
        screenFront.layer = LayerNoPostFront;
        Debug.Log("new layer Front");
    }
}
