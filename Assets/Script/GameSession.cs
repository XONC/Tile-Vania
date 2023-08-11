using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameSession : MonoBehaviour
{
    [Header("life")]
    [SerializeField] float lifeCount;
    [SerializeField] TextMeshProUGUI lifeCountText;

    [Header("coin")]
    [SerializeField] int coinCount = 0;
    [SerializeField] TextMeshProUGUI coinCountText;


    [Header("kill")]
    [SerializeField] int killCount;
    private void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession > 1)
        {
            // 移除新加的对象
            Destroy(gameObject);
        }
        else
        {
            // 保留旧的对象
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        lifeCountText.text = lifeCount.ToString();
        coinCountText.text = coinCount.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if (lifeCount > 1)
        {
            lifeCount--;
            lifeCountText.text = lifeCount.ToString();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            FindObjectOfType<GamePersist>().DestroyScene();
            SceneManager.LoadScene(0); // 返回场景一
            Destroy(gameObject);
        }
    }
    public void SetCoinCount(int baseSource)
    {
        coinCount += baseSource;
        coinCountText.text = coinCount.ToString();
    }
    public void SetKillCount()
    {
        killCount++;
    }

}
