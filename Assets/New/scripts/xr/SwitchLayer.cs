//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//this script includes the functions that get called to switch objects layers
//mainly the empathy objects that should still be in colour after entering grayscale mode, the non empathy objects which should just not be interavtive anymore and the screen so it can fade out normally

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLayer : MonoBehaviour
{
    public void NoPostLayer() //for everything being coloured in greyscale since if it happens before, users already can see the objects they need to get
    {
        Transform[] os = GetComponentsInChildren<Transform>();
        foreach (Transform o in os)
        {
            int LayerNoPost = LayerMask.NameToLayer("NoPost");
            o.gameObject.layer = LayerNoPost;

        }
       
    }

    //makes other objects non interactible too but not colored by putting them in another post layer
    public void HalfPostLayer() //for everything being coloured in greyscale since if it happens before, users already can see the objects they need to get
    {
        Transform[] os = GetComponentsInChildren<Transform>();
        foreach (Transform o in os)
        {
            int LayerNoPost = LayerMask.NameToLayer("HalfPost");
            o.gameObject.layer = LayerNoPost;

        }
       
    }

   

    public void DefaultLayer() //for the screen before fadeout (otherwise would look really bad since it wouldn't fade due to being on another layer)
    {
        Transform[] os = GetComponentsInChildren<Transform>();
        foreach (Transform o in os)
        {
            int LayerDefault = LayerMask.NameToLayer("Default");
            o.gameObject.layer = LayerDefault;

        }
    }
}
