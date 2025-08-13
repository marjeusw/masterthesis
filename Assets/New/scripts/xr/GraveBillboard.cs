using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveBillboard : MonoBehaviour
{

    public float lookAt;
    private Camera theCam;
    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
        transform.LookAt(theCam.transform);

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + lookAt, transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void LateUpdate() 
    {
        //transform.LookAt(theCam.transform);

        //transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + lookAt, transform.rotation.eulerAngles.z);
    }
}
