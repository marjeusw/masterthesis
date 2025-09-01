using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script for pc
public class PickUpTwo : MonoBehaviour
{
    public PickUpScript pickUp;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickUp.special)
        {
            pickUp.GetComponent<Rigidbody>();
            //pickUp.heldObjRb.isKinematic = true;
            //rb.constraints = RigidbodyConstraints.None;
        }
        else
        {
            pickUp.GetComponent<Rigidbody>();
            pickUp.HeldObjRb.isKinematic = true;
            //rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ;
        }
    }
}
