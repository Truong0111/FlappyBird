using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private VoidEvent loseEvent;
    
    public float jumpForce = 5f;
    public float gravityScale = 2f;
    [ShowInInspector] public bool IsDead { get; set; }

    public void SetDead()
    {
        IsDead = true;
        loseEvent.Raise();
    }
}
