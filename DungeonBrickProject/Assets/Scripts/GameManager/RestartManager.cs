using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onGameOver += EnableRestartScreen;
        DisableRestartScreen();
    }

    private void EnableRestartScreen()
    {
        gameObject.SetActive(true);
    }

    private void DisableRestartScreen()
    {
        gameObject.SetActive(false);
    }
}
