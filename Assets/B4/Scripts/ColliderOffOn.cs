//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ColliderOffOn : MonoBehaviour
//{
//    public GameObject collider1;
//    public GameObject collider2;
//    public GameObject collider3;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }


//    void OnTriggerEnter(Collider other)
//    {
//        Debug.Log("vanished");
//        collider1.SetActive(false);

//        speaking();


//    }

//    public void speaking()
//    {
//        if(Input.GetKeyDown(KeyCode.Space)){ // just to see if input works
//            //speak
//            //speak end do this
//            Debug.Log("appeared");
//            collider1.SetActive(true);
//        }
//    }

//}
