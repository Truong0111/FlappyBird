using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class BirdAction : MonoBehaviour
{
    [Title("Value")] [SerializeField] private float zChange = 35f;
    [SerializeField] private float timeRotateUp = 0.1f;
    [SerializeField] private float timeRotateDown = 0.25f;

    private Bird _bird;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _bird = GetComponent<Bird>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(UpdateRotation(ApplyRotate));
    }

    private bool IsKinematic
    {
        get
        {
            var isKinematic = !_bird.IsStart || _bird.IsDead;
            if (isKinematic) _rigidbody2D.velocity = Vector3.zero;
            return isKinematic;
        }
    }

    private void Update()
    {
        _rigidbody2D.isKinematic = IsKinematic;
        if (_bird.IsDead) return;
        if (!_bird.IsStart) return;
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
        if (rotation.z > 0)
        {
            _isLerp = false;
            transform.DOKill();
        }
        
        if(_isLerp) return;
        _isLerp = true;
        transform.DORotateQuaternion(endValue: Quaternion.Euler(rotation),
                duration: rotation.z > 0 ? timeRotateUp : timeRotateDown)
            .SetEase(Ease.Linear)
            .OnComplete(() => _isLerp = false);
    }

    private IEnumerator UpdateRotation(Action<Vector3> onUpdate = null, Action endUpdate = null)
    {
        while (!_bird.IsDead)
        {
            if (_isLerp) yield return null;

            var yUp = _rigidbody2D.velocity.y;
            var rotate = Vector3.zero;

            if (!_bird.IsStart)
            {
                //None
            }
            else if (yUp == 0) yield return null;
            else
            {
                rotate = Vector3.forward * ((yUp > 0 ? 1 : -1) * zChange);
            }

            onUpdate?.Invoke(rotate);
            yield return null;
        }

        endUpdate?.Invoke();
    }
}