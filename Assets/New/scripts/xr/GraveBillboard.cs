//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//Script that sets all graves in the foreground to face the user (so it looks cooler)
//is used at the start while the user is still in the tutorial so might change that at a later date

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

   
}
