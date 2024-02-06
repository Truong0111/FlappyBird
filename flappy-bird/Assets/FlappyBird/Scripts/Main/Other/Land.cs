using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Land : MonoBehaviour
{
    [SerializeField] private VoidEvent startEvent;
    [SerializeField] private VoidEvent loseEvent;
    
    public AutoScrollUV autoScrollUV;
    public float speedScroll = 0.2f;
    private void Awake()
    {
        startEvent.Register(StartScrollUV);
        loseEvent.Register(StopScrollUV);
    }

    private void OnDestroy()
    {
        startEvent.Unregister(StartScrollUV);
        loseEvent.Unregister(StopScrollUV);
    }

    private void StartScrollUV() => autoScrollUV.scrollSpeed = Vector2.right * speedScroll;
    private void StopScrollUV() => autoScrollUV.scrollSpeed = Vector2.zero;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Bird>(out var bird))
        {
            bird.SetDead();
            loseEvent.Raise();
        }
    }
}
