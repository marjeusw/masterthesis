using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoController : MonoBehaviour
{
    private VideoSwitcher switcher;
    private Animator animator;
    //private RectTransform rect; //needs to be the cylinder and rendertextures later

    private void Awake()
    {
        switcher = GetComponent<VideoSwitcher>();
        animator = GetComponent<Animator>();
        //rect = GetComponent<RectTransform>();
    }

    public void Setup(GameObject gameObject)
    {
        switcher.SetVideo(gameObject);
    }

    public void Show(Vector2 coords)
    {
        animator.SetTrigger("ShowVideo");
        Debug.Log("Videoshow");
        //rect.localPosition = coords;
    }

    public void Hide()
    {
        animator.SetTrigger("HideVideo");
    }

    public void Move(Vector2 coords, float speed) //moving the sprite (although not sure if needed)
    {
        //StartCoroutine(MoveCoroutine(coords, speed));
    }

    //private IEnumerator MoveCoroutine(Vector2 coords, float speed)
    //{
    //    //while (rect.localPosition.x != coords.x || rect.localPosition.y != coords.y)
    //    //{
    //    //    rect.localPosition = Vector2.MoveTowards(rect.localPosition, coords, Time.deltaTime * 1000 * speed);
    //    //    yield return new WaitForSeconds(0.01f);
    //    //}
    //}

    public void SwitchVideo(GameObject gameObject)
    {
        if (switcher.GetVideo() != gameObject)
        {
            switcher.SwitchVideo(gameObject);
        }
    }
}
