//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//Script that turns the scene into gray scale and changes all empathy objects to a layer where they can still be colourful and non interactive as well as the non empathy objects also to a layer that is non interactive but still gray
//its done with accessing 2 post processing volumes and a culling mask for the nopostcam which is in a stack on the main camera

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GrayScaleMode : MonoBehaviour
{
    //just so they don't get affected by the greyscale when its turned on (if already set to new layer, they aren't interactive and u can find them too easily since they clip through everything visually)
    public SwitchLayer switchLayer;
    public SwitchLayer switchLayerLock;
    public SwitchLayer switchLayer2;
    public SwitchLayer switchLayer3;
    public SwitchLayer switchLayer4;
    public SwitchLayer switchLayer4two;
    public SwitchLayer switchLayer4three;
    public SwitchLayer switchLayer4four;


    //for the other objects
    public SwitchLayer switchSpider;
    public SwitchLayer switchEyes;
    public SwitchLayer switchFlower;
    public SwitchLayer switchCross;
    //public SwitchLayer switchBubbles;
    public SwitchLayer switchChibi;


    public Volume postProcessing;

    public Volume postProcessingHalf;


    //mute radio
    public AudioSource radio;
    

    public void EnterGreyscale()
    {
        if(postProcessing.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            colorAdjustments.saturation.value = -100f;
            colorAdjustments.contrast.value = -13f;
            colorAdjustments.postExposure.value = -1.19f;
        }
        if (postProcessingHalf.profile.TryGet(out ColorAdjustments halfColorAdjustments)) //for the half post one (just so that the non empathy but still interactive objects are in another layer and non interactive but still accentuated)
        {
            halfColorAdjustments.saturation.value = -100f;
            halfColorAdjustments.contrast.value = -13f;
            halfColorAdjustments.postExposure.value = -1.19f;
        }
        ObjectsNewLayer();

        radio.Stop(); //don't want the radio playing over the witch explaining her sad backstory
    }

    public void ObjectsNewLayer()
    {
        switchLayer.NoPostLayer();
        switchLayerLock.NoPostLayer();
        switchLayer2.NoPostLayer();
        switchLayer3.NoPostLayer();
        switchLayer4.NoPostLayer();
        switchLayer4two.NoPostLayer();
        switchLayer4three.NoPostLayer();
        switchLayer4four.NoPostLayer();

        //for other objects
        switchSpider.HalfPostLayer();
        switchEyes.HalfPostLayer();
        switchFlower.HalfPostLayer();
        switchCross.HalfPostLayer();
        switchChibi.HalfPostLayer();
    }

    
    //this one didn't work as intended but I'll keep it here as reference on how to access nested children recursively through script
    //recursive algorithm to search for grandchildren and stuff
    //realized that I can just attach all visible children to the oarent and only reference children (not grandchildren)
    //easy peasy
    //void FindAllNestedChildren(Transform objectTransform)
    //{
    //    allObjects.Add(objectTransform);

    //    int n = objectTransform.childCount;

    //    if(n > 0)
    //    {
    //        for(int i = 0; i < n; i++)
    //        {
    //            FindAllNestedChildren(objectTransform.GetChild(i));
    //        }
    //    }
    //}

    
}
