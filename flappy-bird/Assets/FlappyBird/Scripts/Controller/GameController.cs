using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private VoidEvent startEvent;
    [SerializeField] private VoidEvent scoreUpdateEvent;
    
    [ShowInInspector] public int CurrentScore { get; set; }

    public override void Awake()
    {
        base.Awake();
        startEvent.Register(StartGame);
        // scoreUpdateEvent.Register(UpdateScore);
    }

    private void OnDestroy()
    {
        startEvent.Register(StartGame);
        // scoreUpdateEvent.Unregister(UpdateScore);
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
