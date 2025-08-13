using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Teleport_to_basket : MonoBehaviour
{
    public Transform basket;
    //just for funsies
    //public Collider objectCollider;
    // Start is called before the first frame update
    void Start()
    {
        //objectCollider = this.GetComponent<Collider>();
        EventManager.ExampleEvent += TeleportToBasket;
    }

    public void TeleportToBasket()
    {
        this.transform.position = basket.position;
        //objectCollider.enabled = false;
    }

    private void OnDisable()
    {
        EventManager.ExampleEvent -= TeleportToBasket;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
