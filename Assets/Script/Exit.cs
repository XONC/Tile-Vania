using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour
{
    [SerializeField] float waitTime;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(waitTime);
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == SceneManager.sceneCountInBuildSettings)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        FindObjectOfType<GamePersist>().DestroyScene();
        SceneManager.LoadScene(index);
    }
}
