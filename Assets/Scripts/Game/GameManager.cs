using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    
    public enum Prefs
    {
        BestRoadLevel
    }
    public bool gameAcitve = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("BestRoadLevel") < Character.instance._levelOfRoad)
        {
            PlayerPrefs.SetInt("BestRoadLevel",  Character.instance._levelOfRoad);
        }

        if (Character.instance.dead)
        {
            Time.timeScale = 0.0f;
        }

        if (Data.getInstance().restartGame)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        gameAcitve = true;
    }
    
    
}
