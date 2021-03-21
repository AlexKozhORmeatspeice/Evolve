using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private Text _nowScore;
    [SerializeField] private Text _bestScore;
    [SerializeField] private GameObject deathScrene;
    [SerializeField] private GameObject _canvas;

    public void Start()
    {
        deathScrene.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        _canvas.gameObject.SetActive(GameManager.instance.gameAcitve);

        if (Character.instance.dead)
        {
            deathScrene.SetActive(true);
        }
        
        _nowScore.text = Character.instance._levelOfRoad.ToString();
        _bestScore.text = PlayerPrefs.GetInt("BestRoadLevel").ToString();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);

        Data.getInstance().restartGame = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
       
        Data.getInstance().restartGame = true;
    }


}
