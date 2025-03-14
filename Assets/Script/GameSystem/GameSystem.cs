using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    Queue,
    Fight
}

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; }
    public GameState CurrentState { get; private set; };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CurrentState = GameState.Queue;
    }
}
