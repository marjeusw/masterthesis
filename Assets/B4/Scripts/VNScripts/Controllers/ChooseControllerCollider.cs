using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseControllerCollider : MonoBehaviour
{
    //public GameObject collider;
    public Ignore ignoreScript;
    public GameController gameController;
    //private Animator animator; //for later

    public void SetupChooseCollider(ChooseScene scene)
    {

        //DestroyColliders();
        //for (int index = 0; index < scene.labels.Count; index++)
        //{
            Ignore newCollider = Instantiate(ignoreScript.gameObject, transform).GetComponent<Ignore>();
            //Instantiate(collider); //might want to do the whole delete and reappear thingy to make it dependent on the text
        //}
    }

    public void PerformChooseColliders(StoryScene scene)
    {
        gameController.PlayScene(scene);
    }

    //private void DestroyColliders()
    //{
    //    foreach (Transform childTransform in transform)
    //    {
    //        Destroy(childTransform.gameObject);
    //    }
    //}
}
