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
        //child.gameObject.layer = LayerMask.NameToLayer("NoPost");
    }


    public void HalfPostLayer() //for everything being coloured in greyscale since if it happens before, users already can see the objects they need to get
    {
        Transform[] os = GetComponentsInChildren<Transform>();
        foreach (Transform o in os)
        {
            int LayerNoPost = LayerMask.NameToLayer("HalfPost");
            o.gameObject.layer = LayerNoPost;

        }
        //child.gameObject.layer = LayerMask.NameToLayer("NoPost");
    }

    //make other objects non interactible too but not colore by putting them in another post layer

    public void DefaultLayer() //for the screen before fadeout (otherwise would look really bad)
    {
        Transform[] os = GetComponentsInChildren<Transform>();
        foreach (Transform o in os)
        {
            int LayerDefault = LayerMask.NameToLayer("Default");
            o.gameObject.layer = LayerDefault;

        }
    }
}
