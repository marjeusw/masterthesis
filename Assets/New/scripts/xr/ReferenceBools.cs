//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//just a script for the grab interactible empathy objects Ash and Shmailor Shmoon so the functions in here can be easily called when they are equipped

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
