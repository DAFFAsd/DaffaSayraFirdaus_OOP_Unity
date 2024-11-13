using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LevelManager LevelManager { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        LevelManager = GetComponentInChildren<LevelManager>();

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("Main Camera"));
        DontDestroyOnLoad(GameObject.Find("Player"));
        DontDestroyOnLoad(GameObject.Find("Bullet"));
        DontDestroyOnLoad(GameObject.Find("Bullet(Clone)"));
        DontDestroyOnLoad(GameObject.Find("Bullet2(Clone)"));
    }
}
