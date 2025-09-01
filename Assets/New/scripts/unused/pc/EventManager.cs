using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action ExampleEvent;

    public static event Action EmpathyObjectE;

    public static event Action PickSoulEvent;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (ExampleEvent != null)
                ExampleEvent();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (EmpathyObjectE != null)
                EmpathyObjectE();
        }
    }

    public static void StartPickSoulEvent()
    {
        if (PickSoulEvent != null)
            PickSoulEvent();
    }
}
