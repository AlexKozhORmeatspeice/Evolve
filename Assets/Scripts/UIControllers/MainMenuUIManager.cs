using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private float _startMusicVolume = 0.1f;
    [SerializeField] private GameObject _canvas;
    // Start is called before the first frame update
    void Awake()
    {
        _musicToggle.isOn = PlayerPrefs.GetInt("MusicStatus") == 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("MusicStatus", _musicToggle.isOn ? 0 : 1);

        Debug.Log(_startMusicVolume);        
        
        _audioMixer.SetFloat("Music", PlayerPrefs.GetInt("MusicStatus") == 0 && 
                                      !Character.instance.dead ? _startMusicVolume : -80.0f);
        _canvas.gameObject.SetActive(!GameManager.instance.gameAcitve);

        _scoreText.text = $"Your best score:\n{PlayerPrefs.GetInt("BestRoadLevel")}";
    }

    
}
