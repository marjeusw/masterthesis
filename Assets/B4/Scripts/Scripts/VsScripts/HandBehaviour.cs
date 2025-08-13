using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This scripts handles the animations of the hand models //
// Written by Victoria Amelunxen //

public class HandBehaviour : MonoBehaviour
{
    [SerializeField] private InputActionProperty gripAction;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        float gripValue = gripAction.action.ReadValue<float>();
        anim.SetFloat("Grip", gripValue);
    }
}
