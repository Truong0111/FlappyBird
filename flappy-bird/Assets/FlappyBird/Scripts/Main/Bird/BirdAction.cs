using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAction : MonoBehaviour
{
    [SerializeField] private float zUp = 35f;
    [SerializeField] private float zDown = -35f;
    
    private Bird _bird;
    private Rigidbody2D _rigidbody2D;
    
    private bool CheckDead
    {
        get
        {
            _rigidbody2D.isKinematic = _bird.IsDead;
            if(_bird.IsDead) _rigidbody2D.velocity = Vector2.zero;
            return _bird.IsDead;
        }
    }
    
    private void Awake()
    {
        _bird = GetComponent<Bird>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // StartCoroutine()
    }

    private void Update()
    {
        if (CheckDead) enabled = false;
        ApplyGravity();
        ApplyJump();
    }
    
    private void ApplyJump()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody2D.velocity = Vector2.up * _bird.jumpForce;
        }
    }

    private void ApplyGravity()
    {
        _rigidbody2D.velocity += Vector2.down * (_bird.gravityScale * Time.deltaTime);
    }

    private bool A() => false;

    // private IEnumerator UpdateRotation(int frameUpdate, Action<Vector3> onUpdate = null, Action endUpdate = null,
    //     bool isDead = false)
    // {
    //     var currentFrame = 0;
    //
    //     while (true)
    //     {
    //         if (isDead) break;
    //         currentFrame++;
    //         onUpdate?.Invoke(currentFrame % frameUpdate);
    //         yield return null;
    //     }
    //
    //     endUpdate?.Invoke();
    // }
}
