using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private VoidEvent startEvent;
    [SerializeField] private VoidEvent loseEvent;
    
    public float jumpForce = 5f;
    public float gravityScale = 2f;
    
    
    public bool IsStart { get; private set; }
    public bool IsDead { get; private set; }

    private void Awake()
    {
        startEvent.Register(SetStart);
    }

    private void Start()
    {
        GameController.Instance.Player = this;
    }

    private void OnEnable()
    {
        IsStart = false;
        IsDead = false;
    }

    private void OnDestroy()
    {
        startEvent.Unregister(SetStart);
    }

    private void SetStart()
    {
        IsStart = true;
    }
    
    public void SetDead()
    {
        IsDead = true;
        loseEvent.Raise();
    }
}
