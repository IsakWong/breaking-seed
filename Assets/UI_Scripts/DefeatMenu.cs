﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class DefeatMenu : MonoBehaviour {

    public string LoadingSceneName;
    private Animator _animator;
    private AudioSource _audio;

    public static DefeatMenu Current;
    // Use this for initialization
    void Start()
    {
        if (Current == null)
            Current = this;
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _audio.loop = false;
        _audio.playOnAwake = true;
        _audio.volume = 0.3f;

    }
    public void ShowDefeatMenu()
    {
        _animator.SetBool("move_in", true);
        _animator.SetBool("move_out", false);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurOptimized>().enabled = true;
        transform.parent.GetChild(1).gameObject.SetActive(false);
    }

    public void HideDefeatMenu()
    {
        _animator.SetBool("move_in", false);
        _animator.SetBool("move_out", true);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurOptimized>().enabled = false;
        transform.parent.GetChild(1).gameObject.SetActive(false);
    }
    public void OnRestartClick()
    {
        SceneManager.LoadScene(LoadingSceneName);
    }
    public void OnBackToMenuClick()
    {
        SceneManager.LoadScene("MainScene");
    }
}
