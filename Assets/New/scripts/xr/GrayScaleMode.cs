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

    public Volume postProcessing;
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
        ObjectsNewLayer();
       
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
