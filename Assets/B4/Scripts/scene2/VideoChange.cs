using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoChange : MonoBehaviour
{
    public GameObject video1;
    public GameObject video2;

    private bool isVideo1;

   


    // Start is called before the first frame update
    void Start()
    {
        isVideo1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (isVideo1 == true)
        {
            video1.SetActive(false);
            video2.SetActive(true);
            isVideo1 = false;
        }
        else
        {
            video2.SetActive(false);
            video1.SetActive(true);
            isVideo1 = true;
        }
    }
}
