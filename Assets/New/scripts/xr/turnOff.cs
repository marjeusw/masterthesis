//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//this script is just for the 2nd loop
//it checks if the user is done with the first loop and if so turns off the candles and soul model on the grave as well as it turns on the last text to show credits

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOff : MonoBehaviour
{
    
    public GameObject candlesAndSoul;
    public GameObject DemoText;
    public GameObject Canvas;

    public nextLine line;
    // Start is called before the first frame update
    void Awake()
    {
        if(EndingManager.candles == true)
        {
            candlesAndSoul.SetActive(false);

            Canvas.transform.GetChild(0).gameObject.SetActive(false);
            DemoText.SetActive(true);

            //in normal gameplay transform user to grave scene (turn off staging+ball and sphere, and turn on terrain) in here instead (because they don't need to redo the tutorial)
        }
    }

}
