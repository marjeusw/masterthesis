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
            //Respawn();


        }

    }



    IEnumerator SmokeEnd()
    {
        //mesh.enabled = false;
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            if (r.tag == "Poof")
            {
                r.enabled = false;
            }
            else
            {
                Debug.Log("nopoofs");
            }
        } 
     

        yield return new WaitForSeconds(2.0f);

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
        this.transform.Rotate(rX, rY, rZ); //just cause the raotaion isn't always right, to manually make it pretty customized to the object
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            r.enabled = true;
        }
        //for not nested children
        //mesh.enabled = true;
    }
        

    // Update is called once per frame
    void Update()
    {
        
    }
}
