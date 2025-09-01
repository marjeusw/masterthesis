//script by Marje-Alicia Harms
//768147 Expanded Media
//Project: LimboAssist - Master Thesis Prototype
//Script that enables user to restart ´the game if pressing secondary button on left controller (made for the user testing so I didn't need to restart the game every time)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class RestartGame : MonoBehaviour
{
    public InputActionReference restart;

    private void Awake()
    {
        restart.action.Enable();
        restart.action.performed += RestartGameFunction;
    }

    public void RestartGameFunction(InputAction.CallbackContext context)
    {
        EndingManager.candles = false;
        SceneManager.LoadScene("candles");
    }
}
