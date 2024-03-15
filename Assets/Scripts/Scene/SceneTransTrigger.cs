
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransTrigger : MonoBehaviour
{ 
    public Animator transAnimator;
    public float transDelay;
    
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(StartSceneTrans());
                             
        }
    }

    IEnumerator StartSceneTrans() 
    {
        transAnimator.SetTrigger("Enter");

        yield return new WaitForSeconds(transDelay);
        FindObjectOfType<GameScene>().Dungeon();

        yield return null;
    }
}

