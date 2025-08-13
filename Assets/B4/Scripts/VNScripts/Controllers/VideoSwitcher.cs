using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class VideoSwitcher : MonoBehaviour
{
    public bool isSwitched = false;
    public GameObject video1;
    public GameObject video2;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SwitchVideo(GameObject gameObject)
    {
        if (!isSwitched)
        {
            video2 = gameObject;
            video2.transform.GetChild(0).gameObject.SetActive(false);
            animator.SetTrigger("SwitchFirstVideo"); //maybe switch1
            //video2.SetActive(false);
            //video1.SetActive(true);

        }
        else
        {
            video1 = gameObject;
            video1.transform.GetChild(0).gameObject.SetActive(false);

            ////image1.sprite = sprite;
            animator.SetTrigger("SwitchSecondVideo");
            //video1.SetActive(false); //attempting to make the switch work
            //video2.SetActive(true);
        }
        isSwitched = !isSwitched;
    }

    public void SetVideo(GameObject gameObject)
    {
        if (!isSwitched)
        {
            //image1.sprite = sprite;
            video1 = gameObject;
        }
        else
        {
            video2 = gameObject;
            //image2.sprite = sprite;
        }
    }

    public GameObject GetVideo()
    {
        if (!isSwitched)
        {
            //return image1.sprite;
            return video1.gameObject;
        }
        else
        {
            //return image2.sprite;
            return video2.gameObject;
        }
    }
}
