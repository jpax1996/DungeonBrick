using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onGameStart;
    public void GameStart()
    {
        onGameStart?.Invoke();
    }
    
    public event Action onGameOver;
    public void GameOver()
    {
        onGameOver?.Invoke();
    }
    
    public event Action onGameRestart;
    public void GameRestart()
    {
        onGameRestart?.Invoke();
    }

    public event Action onLevelStart;
    public void LevelStart()
    {
        onLevelStart?.Invoke();
    }
    
    public event Action onLevelOver;
    public void LevelOver()
    {
        onLevelOver?.Invoke();
    }
    
    public event Action onAimingStart;
    public void AimingStart()
    {
        onAimingStart?.Invoke();
    }

    public event Action onThrowStart;
    public void ThrowStart()
    {
        onThrowStart?.Invoke();
    }
    
    public event Action onThrowOver;
    public void ThrowOver()
    {
        onThrowOver?.Invoke();
    }
    
    public event Action onXPUpdateStart;
    public void XpUpdateStart()
    {
        onXPUpdateStart?.Invoke();
    }
    
    public event Action onXPUpdateOver;
    public void XpUpdateOver()
    {
        onXPUpdateOver?.Invoke();
    }
    
    public event Action onLevelUpStart;
    public void LevelUpStart()
    {
        onLevelUpStart?.Invoke();
    }
    
    public event Action onLevelUpOver;
    public void LevelUpOver()
    {
        onLevelUpOver?.Invoke();
    }
}
