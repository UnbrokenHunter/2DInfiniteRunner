using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    public Gamemodes gamemode;

    private void Awake()
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

    public bool IsFirstLevelLoad { get; private set; } = true;

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
            IsFirstLevelLoad = true;

        else
            StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        IsFirstLevelLoad = false;
    }

}

public enum Gamemodes
{
    Casual, 
    Supersized,
    Speedy,
}