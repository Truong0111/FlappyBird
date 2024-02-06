using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    public VoidEvent menuEvent;
    public VoidEvent loseEvent;

    public Button menuButton;
    
    private void Awake()
    {
        loseEvent.Register(ShowLoseUI);
        menuButton.onClick.AddListener(BackToMenu);
        
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        loseEvent.Unregister(ShowLoseUI);
    }

    private void ShowLoseUI()
    {
        gameObject.SetActive(true);
    }

    private void BackToMenu()
    { 
        menuEvent.Raise();
        gameObject.SetActive(false);
    }
}
