using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    [SerializeField] private AudioMixer _audioMixer;
    private float _startMusicVolume;
    [SerializeField] private GameObject _canvas;
    // Start is called before the first frame update
    void Start()
    {
        _audioMixer.GetFloat("Music", out _startMusicVolume);
    }

    // Update is called once per frame
    void Update()
    {
        _canvas.gameObject.SetActive(!GameManager.instance.gameAcitve);

        _scoreText.text = $"Your best score:\n{PlayerPrefs.GetInt("BestRoadLevel")}";
    }

    public void MusicOnOff(bool isOn)
    {
        if (!isOn)
        {
            _audioMixer.SetFloat("Music", -80.0f);
        }
        else
        {
            _audioMixer.SetFloat("Music", _startMusicVolume);
        }
    }
}
