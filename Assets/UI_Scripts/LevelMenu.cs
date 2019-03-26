using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class LevelMenu : MonoBehaviour
{

    public String LoadingSceneName;
    private Animator _animator;
    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowLevelMenu()
    {
        _animator.SetBool("move_in", true);
        _animator.SetBool("move_out", false);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurOptimized>().enabled = true;
        transform.parent.GetChild(1).gameObject.SetActive(false);
    }
    public void HideLevelMenu()
    {
        _animator.SetBool("move_in", false);
        _animator.SetBool("move_out", true);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurOptimized>().enabled = false;
        transform.parent.GetChild(1).gameObject.SetActive(false);
    }
    public void OnResumeClick()
    {
        _animator.SetBool("move_in", false);
        _animator.SetBool("move_out", true);

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurOptimized>().enabled = false;
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
