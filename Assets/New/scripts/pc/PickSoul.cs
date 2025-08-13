using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PickSoul : MonoBehaviour
{
    private int sceneNum = 1;
    public AnnouncementTIme announcement;
    // Start is called before the first frame update
    private void Start()
    {
        //EventManager.PickSoulEvent += SceneChange;
    }

    private void OnTriggerEnter(Collider other)
    {
        //EventManager.StartPickSoulEvent();

        //set bool true in coroutine for this and when true, next time trigger here change scene
        if(announcement.isAnnounced == false)
        {
            SceneChange();
        }
        
    }

    public void SceneChange()
    {
        SceneManager.LoadScene(sceneNum);
    }

}
