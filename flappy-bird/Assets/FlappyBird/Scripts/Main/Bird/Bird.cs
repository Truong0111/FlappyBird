using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float jumpForce = 5f;
    public float gravityScale = 2f;
    [ShowInInspector] public bool IsDead { get; set; }
}
