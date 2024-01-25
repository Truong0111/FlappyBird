using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BirdAction : MonoBehaviour
{
    [SerializeField] private float zChange = 35f;
    [SerializeField] private int frameCountToUpdateUp = 1;
    [SerializeField] private int frameCountToUpdateDown = 10;
    [SerializeField] private float timeRotateUp = 0.1f;
    [SerializeField] private float timeRotateDown = 0.25f;

    private Bird _bird;
    private Rigidbody2D _rigidbody2D;

    private bool CheckDead
    {
        get
        {
            _rigidbody2D.isKinematic = _bird.IsDead;
            if (_bird.IsDead) _rigidbody2D.velocity = Vector2.zero;
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
        StartCoroutine(UpdateRotation(_isLerp, ApplyRotate));
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

    private bool _isLerp;

    private void ApplyRotate(Vector3 rotation)
    {
        _isLerp = true;
        transform.DORotateQuaternion(Quaternion.Euler(rotation), rotation.z > 0 ? timeRotateUp : timeRotateDown)
            .SetEase(Ease.Linear)
            .OnComplete(() => _isLerp = true);
    }

    private IEnumerator UpdateRotation(bool isDead, Action<Vector3> onUpdate = null, Action endUpdate = null)
    {
        var currentFrame = 0;

        while (true)
        {
            if (isDead) break;
            if (_isLerp) yield return null;

            var yUp = _rigidbody2D.velocity.y;

            var rotate = Vector3.zero;

            if (yUp == 0) yield return null;

            if (currentFrame % (yUp > 0 ? frameCountToUpdateUp : frameCountToUpdateDown) == 0)
            {
                rotate = Vector3.forward * ((yUp > 0 ? 1 : -1) * zChange);
            }

            onUpdate?.Invoke(rotate);
            yield return null;
            currentFrame++;
        }

        endUpdate?.Invoke();
    }
}