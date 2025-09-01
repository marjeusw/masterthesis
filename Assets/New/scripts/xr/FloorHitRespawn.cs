//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//Script for objects that hit the floor poofing and respawning 
//object and its immediate (not nested) children with tag poof get their mesh renderer disabled when hitting the floor, then wait till the poof particle effect played for 2 seconds,
//then they respawn at their respective teleport points (rotation has to be manually enetered in inspector since somehow just resetting it didn't work so this is a quick and dirty fix)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHitRespawn : MonoBehaviour
{
    public Transform teleportPoint;
    public GameObject floor;
    public ParticleSystem smoke;

    public float rX = 0;
    public float rY = 0;
    public float rZ = 0;
    //public MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject.tag == "Floor")
        {
            
            smoke.Play();
            StartCoroutine("SmokeEnd");

        }

    }



    IEnumerator SmokeEnd()
    {
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            if (r.tag == "Poof") //immediate children of the object with tag poof will get their mesh renderers disabled
            {
                r.enabled = false;
            }
            else
            {
                Debug.Log("nopoofs");
            }
        } 
     

        yield return new WaitForSeconds(2.0f); //wait till smoke is over

        //return it to its normal position & rotation without the velocity
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        this.transform.position = Vector3.zero;
        this.transform.rotation = Quaternion.identity;

        Respawn();
    }


    public void Respawn()
    {
        this.transform.position = teleportPoint.position;
        this.transform.Rotate(rX, rY, rZ); //just cause the rotation isn't always right, to manually make it pretty customized to the object
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            r.enabled = true;
        }
        //for not nested children
        
    }
    
}
