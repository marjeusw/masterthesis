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
