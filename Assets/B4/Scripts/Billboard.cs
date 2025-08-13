using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    public float lookAt;
    private Camera theCam;
    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate() //so that the camera has already moved when this action starts
    {
        transform.LookAt(theCam.transform);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + lookAt, transform.rotation.eulerAngles.y, 0f);
    }
}
