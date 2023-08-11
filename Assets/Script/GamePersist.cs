using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GamePersist : MonoBehaviour
{
    private void Awake()
    {
        int numGamePersist = FindObjectsOfType<GamePersist>().Length;
        if (numGamePersist > 1)
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
    public void DestroyScene()
    {
        Destroy(gameObject);
    }

}
