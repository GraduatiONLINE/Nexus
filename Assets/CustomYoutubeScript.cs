﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;
using TMPro;
using Michsky.UI.ModernUIPack;
using UnityEngine.UI;

public class CustomYoutubeScript : MonoBehaviour
{
    private YoutubePlayer player;

    public string url;
    public bool autoPlay = true;
    private VideoPlayer videoPlayer;

    private int currentTime;
    private int enabled;
    private int fullscreen;
    private int isPaused;
    private float volume;
    private string youtubeUrl;

    private YoutubeSync youtubeSync;

    public GameObject linkInputObject;
    private TMP_InputField linkInput;

    private bool urlInControl;

    public GameObject screen;

    public GameObject enableButtonText;

    private int prevEnabled;

    public GameObject volumeSlider;

    public GameObject myVideoPlayer;


    private void Awake()
    {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        player = GetComponentInChildren<YoutubePlayer>();
        player.videoPlayer = videoPlayer;

        currentTime = 0;
        enabled = 0;
        fullscreen = 0;
        isPaused = 0;
        volume = 0.5f;
        youtubeUrl = "";
    }

    private void Start()
    {
        urlInControl = false;
        
        youtubeSync = GetComponent<YoutubeSync>();
        InvokeRepeating("UpdateModel", 0, 0.5f);
        InvokeRepeating("UpdateScreen", 0, 0.5f);

        Debug.Log("object:"+linkInputObject.ToString());
        // linkInputObject = GameObject.Find("LinkInput");
        linkInput = linkInputObject.GetComponent<TMP_InputField>();
        // PlayNew(Convert.ToInt32(youtubeSync.GetYoutubeParameter(3)));
        prevEnabled = Convert.ToInt32(youtubeSync.GetYoutubeParameter(0));
        volume = float.Parse(youtubeSync.GetYoutubeParameter(4));
    }

    public void SetNewUrl(){
        Debug.Log("url update");
        youtubeUrl = linkInput.text;
        currentTime = 0;
        fullscreen = 0;
        isPaused = 0;
    }

    public void ToggleEnableScreen(){
        enabled = 1 - enabled;
        Debug.Log(enabled);
    }

    private void UpdateModel(){
        youtubeSync.SetYoutube(YoutubeToString(enabled, fullscreen, isPaused, player.GetCurrentTime(), volume, youtubeUrl));
    }

    public void ChangeVolume(float f){
        volume = f;
    }

    private string prevUrl = "";
    private void UpdateScreen(){
        if(youtubeUrl != prevUrl){
            PlayNew(youtubeUrl);
            prevUrl = youtubeUrl;
        }
        if(enabled != prevEnabled){
            Debug.Log("switch from model!");
            if(enabled == 0){
                enableButtonText.GetComponent<TMP_Text>().text = "OFF";
                screen.GetComponent<MeshRenderer>().enabled = false;
            }
            else{
                enableButtonText.GetComponent<TMP_Text>().text= "ON";
                screen.GetComponent<MeshRenderer>().enabled = true;
            }
            prevEnabled = enabled;
        }
        myVideoPlayer.GetComponent<AudioSource>().volume = volume;
        volumeSlider.GetComponent<Slider>().value = volume;
        

    }

    public void ReceiveUpdate(int _enabled, int _fullscreen , int _isPaused, int _currentTime, float _volume, string _youtubeUrl){
        enabled = _enabled;
        currentTime = _currentTime;
        isPaused = _isPaused;
        volume = _volume;
        youtubeUrl = _youtubeUrl;
        fullscreen = _fullscreen;
    }

    public void UrlInControl(){
        Debug.Log("In control");
        urlInControl = true;
    }

    public void UrlOutControl(){
        Debug.Log("Out control");
        urlInControl = false;
    }

    private void Update(){
        
    }


    private string YoutubeToString(int _enabled, int _fullscreen , int _isPaused, int _currentTime, float _volume, string _youtubeUrl){
        return _enabled.ToString() + "_" + _fullscreen.ToString() + "_" + _isPaused.ToString() + "_" + _currentTime.ToString() 
        + "_" + _volume.ToString() + "_" + _youtubeUrl.ToString(); 
    }

    public void PlayNew(string url){
        player.Play(url);
    }

    // public void Play(int time)
    // {   
        
    //     player.Play(url);
    //     player.Seek(time);
        // if (fullscreen)
        // {
        //     videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        // }
        // player.autoPlayOnStart = autoPlay;
        // player.videoQuality = YoutubePlayer.YoutubeVideoQuality.STANDARD;


        // if(autoPlay)
        //     player.Play(url);
    // }

    // public void StartVideo(){
    //     player.Start();
    // }

    // public void PauseVideo(){
    //     player.Pause();
    // }
}
