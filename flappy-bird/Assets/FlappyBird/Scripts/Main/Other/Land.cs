using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Land : MonoBehaviour
{
    [SerializeField] private VoidEvent loseEvent;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Bird>(out var bird))
        {
            bird.SetDead();
            loseEvent.Raise();
        }
    }
}
