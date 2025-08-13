using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RealSceneChange : MonoBehaviour
{
    public InputActionReference nextScene = null;
    public InputActionReference previousScene = null;


    // Start is called before the first frame update
    void Awake()
    {
        nextScene.action.started += PressButtonNextScene;
        previousScene.action.started += PressButtonPreviousScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressButtonNextScene(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("JustInteraction");
    }

    public void PressButtonPreviousScene(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("SampleScene");
    }
    
}
