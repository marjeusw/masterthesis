using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameController : MonoBehaviour
{
    public GameScene currentScene;
    public BottomBarController bottomBar;
    public SpriteSwitcher backgroundController;
    public ChooseController chooseController;
    public ChooseControllerCollider chooseControllerCollider;


    private State state = State.IDLE;
    private enum State
    {
        IDLE, ANIMATE, CHOOSE
    }

   
    public InputActionReference nextSentence = null;



    // Start is called before the first frame update

    private void Awake()
    {
        nextSentence.action.started += PressButton;




    }


    void Start()
    {
        if (currentScene is StoryScene)
        {
            StoryScene storyScene = currentScene as StoryScene;
            bottomBar.PlayScene(storyScene);
            backgroundController.SetImage(storyScene.background);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) //make this to input action

        //nextSentence.action.started += PressButton;
        


        //{
        //    //nextSentence.action.started += LezGo();
        //    if (bottomBar.IsCompleted())
        //    {
        //        if (bottomBar.IsLastSentence())
        //        {
        //            currentScene = currentScene.nextScene;
        //            bottomBar.PlayScene(currentScene);
        //            backgroundController.SwitchImage(currentScene.background);
        //        }
        //        else
        //        {
        //            bottomBar.PlayNextSentence();
        //        }
        //    }
        //}
    }

    public void PressButton(InputAction.CallbackContext context)
    {
        
        if (state == State.IDLE && bottomBar.IsCompleted())
        {
            
            if (bottomBar.IsLastSentence())
            {
                Debug.Log("Qustion");
                PlayScene((currentScene as StoryScene).nextScene); 

               
            }
            else
            {
                Debug.Log("Qustion2");
                bottomBar.PlayNextSentence();
            }
        }
    }

   



    public void PlayScene(GameScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    private IEnumerator SwitchScene(GameScene scene)
    {
        state = State.ANIMATE;
        currentScene = scene;
        bottomBar.Hide();
        yield return new WaitForSeconds(1f);
        if (scene is StoryScene)
        {
            StoryScene storyScene = scene as StoryScene;
            backgroundController.SwitchImage(storyScene.background);
            yield return new WaitForSeconds(1f);
            bottomBar.ClearText();
            bottomBar.Show();
            yield return new WaitForSeconds(1f);
            bottomBar.PlayScene(storyScene);
            state = State.IDLE;
        } else if(scene is ChooseScene)
        {
            state = State.CHOOSE;
            chooseController.SetupChoose(scene as ChooseScene);
            chooseControllerCollider.SetupChooseCollider(scene as ChooseScene);
        }
    }
}
