using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour {
    
    public int MaxSeed;
    public LevelMenu levelMenu;
    public VictoryMenu victoryMenu;
    public DefeatMenu defeatMenu;
    

    private AudioSource _audio;
    public AudioClip bombSFX;
    int CurrenSeedNumber = 0;

    void Start()
    {

    }

    public void SetWaterFloat(float value)
    {
        GetComponent<Text>().text = ((int)value).ToString();
    }
    
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            levelMenu.ShowLevelMenu();
        }
        
    }
}
