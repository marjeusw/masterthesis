using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleScript : MonoBehaviour
{
    public ParticleSystem Particle;
    public PickUpScript pickUp;

    

    // Start is called before the first frame update
    public void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        EventManager.EmpathyObjectE += PlayParticle;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    EventManager.StartEmpathyEvent();
    //}
    //pc stuff don't care don't need if not in playercontroller
    public void PlayParticle()
    {
        if (pickUp.special)
        {
            if (Particle != null)
            {
                Particle.Play();
            }
            else
            {
                Debug.LogError("ParticleSystem Ref isn't assigned dumbo!");
            }
        }
        
    }

    private void OnDisable()
    {
        EventManager.EmpathyObjectE -= PlayParticle;
    }

 
}
