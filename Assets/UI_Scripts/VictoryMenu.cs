using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class VictoryMenu : MonoBehaviour {


    public String LoadingSceneName;
    public String NextSceneName;
    public AudioClip VictorySfx;

    private Animator _animator;
    private AudioSource _audio;
    
    

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _audio.loop = false;
        _audio.playOnAwake = true;
        _audio.volume = 0.3f;
        
    }
    public void ShowVictoryMenu()
    {
        _audio.PlayOneShot(VictorySfx);

        _animator.SetBool("move_in", true);
        _animator.SetBool("move_out", false);

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurOptimized>().enabled = true;

        transform.parent.GetChild(1).gameObject.SetActive(false);
    }
    public void OnNextLevelClick()
    {
        SceneManager.LoadScene(NextSceneName);
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
