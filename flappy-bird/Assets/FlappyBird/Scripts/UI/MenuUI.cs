using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public VoidEvent menuEvent;
    public VoidEvent startEvent;
    public Button startButton; 
    
    private void Awake()
    {
        menuEvent.Register(ShowMenuUI);
        startButton.onClick.AddListener(StartGame);
    }

    private void OnDestroy()
    {
        menuEvent.Unregister(ShowMenuUI);
    }

    private void ShowMenuUI()
    {
        gameObject.SetActive(true);
        LevelManager.Instance.BackToMenu();
    }
    
    private void StartGame()
    {
        startEvent.Raise();
        gameObject.SetActive(false);
    }
}
