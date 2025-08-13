using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndingScreen : MonoBehaviour
{
    public EndingManager manager;
    public GameObject screen;
    public GameObject House;
    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Punch"))
    //    {

    //        StartScreen();
    //    }

    //}

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Punch"))
    //    {

    //        StartScreen();
    //    }
    //}

    public void StartScreen()
    {
        House.SetActive(false);
        NewLayer();
        Debug.Log("ScreenEntered");
        manager.CheckForEnding();
    }

    public void NewLayer()
    {
        int LayerNoPost = LayerMask.NameToLayer("NoPost");
        screen.layer = LayerNoPost;
        Debug.Log("new layer");
    }
}
