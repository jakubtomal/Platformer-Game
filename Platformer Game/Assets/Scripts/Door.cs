using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] int SceneToLoad = 0 ;
    [SerializeField] float delay = 0;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown("up"))
        {
            StartCoroutine(LoadScene(SceneToLoad));
        }
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndex);
    }
}
