using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private VoidEvent startEvent;
    [SerializeField] private VoidEvent scoreUpdateEvent;
    
    public Bird Player { get; set; }
    public int CurrentScore { get; set; }

    public override void Awake()
    {
        base.Awake();
        startEvent.Register(StartGame);
    }
    private void OnDestroy()
    {
        startEvent.Register(StartGame);
    }

    private void Start()
    {
        scoreUpdateEvent.Raise();
    }

    private void StartGame()
    {
        CurrentScore = 0;
        scoreUpdateEvent.Raise();
    }
}
