using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugScript : MonoBehaviour
{
    public GameObject soulOut;
    public Material materialHug;
    public Material soulNormal;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Punched");
        soulOut.GetComponent<MeshRenderer>().material = materialHug;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Punch Ended");
        soulOut.GetComponent<MeshRenderer>().material = soulNormal;
    }
}
