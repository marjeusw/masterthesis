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
    //public Transform parentObject;
    //public List<Transform> allObjects;
    // Start is called before the first frame update
    void Start()
    {
        //FindAllNestedChildren(parentObject);
    }

    public void EnterGreyscale()
    {
        if(postProcessing.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            colorAdjustments.saturation.value = -100f;
            colorAdjustments.contrast.value = -13f;
            colorAdjustments.postExposure.value = -1.19f;
        }
        if (postProcessingHalf.profile.TryGet(out ColorAdjustments halfColorAdjustments)) //for the half post one (just so that the objects are in another layer and non interactive but still accentuated)
        {
            halfColorAdjustments.saturation.value = -100f;
            halfColorAdjustments.contrast.value = -13f;
            halfColorAdjustments.postExposure.value = -1.19f;
        }
        ObjectsNewLayer();

        radio.Stop();
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
        //switchBubbles.HalfPostLayer();
        switchChibi.HalfPostLayer();
    }

    

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
