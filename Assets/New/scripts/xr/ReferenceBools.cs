using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceBools : MonoBehaviour
{
    public bool ash = false;
    public bool sailor = false;
   

    public void SailorMoonEquipped()
    {
        sailor = true;
    }

    public void AshEquipped()
    {
        ash = true;
    }

  
}
