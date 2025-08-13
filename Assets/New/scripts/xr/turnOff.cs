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
