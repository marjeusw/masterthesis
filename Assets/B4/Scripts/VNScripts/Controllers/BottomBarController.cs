using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomBarController : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;

    private int sentenceIndex = -1;
    public StoryScene currentScene;
    private State state = State.COMPLETED;

    public Animator animator;
    public Animator animatorVideo;
    private bool isHidden = false;

    public Dictionary<Speaker, SpriteController> sprites; //turn into rendertexture
    public GameObject spritesPrefab;
    public Dictionary<Speaker, VideoController> videos;
    public GameObject videosPrefab;

    private enum State
    {
        PLAYING, COMPLETED
    }

    public void Start()
    {
        sprites = new Dictionary<Speaker, SpriteController>();
        videos = new Dictionary<Speaker, VideoController>();
        //animator = GetComponent<Animator>();
    }

    public void Hide()
    {
        if (!isHidden)
        {
            animator.SetTrigger("Hide");
            animatorVideo.SetTrigger("HideVideo");
            isHidden = true;
        }
    }

    public void Show()
    {
        animator.SetTrigger("Show");
        animatorVideo.SetTrigger("ShowVideo");
        isHidden = false;
    }

    public void ClearText() //has to be changed to not continue audio playing instead of text
    {
        barText.text = "";
    }

    public void PlayScene(StoryScene scene)
    {
        currentScene = scene;
        sentenceIndex = -1;
        PlayNextSentence();
    }
    public void PlayNextSentence()
    {
        StartCoroutine(TypeText(currentScene.sentences[++sentenceIndex].text));
        personNameText.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
        personNameText.color = currentScene.sentences[sentenceIndex].speaker.textColor;
        ActSpeakers();
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

   private IEnumerator TypeText(string text) //text gets displayed in new thread so the one b4 doesn't get blocked (bc of that use enum)
    {
        barText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            barText.text += text[wordIndex];
            yield return new WaitForSeconds(0.05f);
            if(++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }

    private void ActSpeakers()
    {
        List<StoryScene.Sentence.Action> actions = currentScene.sentences[sentenceIndex].actions;
        for(int i = 0; i < actions.Count; i++)
        {
            ActSpeaker(actions[i]);
        }
    }

    private void ActSpeaker(StoryScene.Sentence.Action action) //checks when sprite appears if already in dictonary and if not creates it
    {
        SpriteController controller = null;
        VideoController videoController = null;
        switch (action.actionType)
        {
            case StoryScene.Sentence.Action.Type.APPEAR:
                if (!sprites.ContainsKey(action.speaker))
                {
                    controller = Instantiate(action.speaker.prefab.gameObject, spritesPrefab.transform).GetComponent<SpriteController>();
                    sprites.Add(action.speaker, controller);
                }
                else
                {
                    controller = sprites[action.speaker];
                }

                //for video
                if (!videos.ContainsKey(action.speaker))
                {
                    videoController = Instantiate(action.speaker.prefabCharacter.gameObject, videosPrefab.transform).GetComponent<VideoController>();
                    videos.Add(action.speaker, videoController);
                }
                else
                {
                    videoController = videos[action.speaker]; //maybe not needed
                }
                controller.Setup(action.speaker.sprites[action.spriteIndex]);
                controller.Show(action.coords);
                //for video
                videoController.Setup(action.speaker.videos[action.spriteIndex]);
                videoController.Show(action.coords);

                return;
            case StoryScene.Sentence.Action.Type.MOVE:
                if (sprites.ContainsKey(action.speaker))
                {
                    controller = sprites[action.speaker];
                    controller.Move(action.coords, action.moveSpeed);
                }
                //for video
                if (videos.ContainsKey(action.speaker))
                {
                    videoController = videos[action.speaker];
                    videoController.Move(action.coords, action.moveSpeed); //don't need it so commentesd out
                }
                break;
            case StoryScene.Sentence.Action.Type.DISAPPEAR:
                if (sprites.ContainsKey(action.speaker))
                {
                    controller = sprites[action.speaker];
                    controller.Hide();
                }
                //for video
                if (videos.ContainsKey(action.speaker))
                {
                    videoController = videos[action.speaker];
                    videoController.Hide();
                }
                break;
            case StoryScene.Sentence.Action.Type.NONE:
                if (sprites.ContainsKey(action.speaker))
                {
                    controller = sprites[action.speaker];
                }
                //for video
                if (videos.ContainsKey(action.speaker))
                {
                    videoController = videos[action.speaker];
                }
                break;
        }
        if(controller != null)
        {
            controller.SwitchSprite(action.speaker.sprites[action.spriteIndex]);
        }
        //for video
        if (videoController != null)
        {
            videoController.SwitchVideo(action.speaker.videos[action.spriteIndex]);
        }
    }
}
