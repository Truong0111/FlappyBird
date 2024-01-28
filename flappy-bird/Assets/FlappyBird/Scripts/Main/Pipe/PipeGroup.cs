using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PipeGroup : MonoBehaviour
{
    [Title("Event")] 
    [SerializeField] private VoidEvent startEvent;
    [SerializeField] private VoidEvent loseEvent;
    [SerializeField] private VoidEvent scoreUpdateEvent;
    
    [Title("Ref")]
    [SerializeField] private Pipe pipe1;
    [SerializeField] private Pipe pipe2;
    [SerializeField] private BoxCollider2D scoreCollider;
    [SerializeField] private float rotateZ;

    private const float XScale = 0.52f;
    private const float YScale = 3.2f;
    private const float ZScale = 1f;

    public float RangePipe { get; set; } = 3f;

    public float RotateZ
    {
        get => rotateZ;
        set => rotateZ = value;
    }

    public float Speed { get; set; } = 2f;

    private float PipeScaleX => XScale * pipe1.transform.localScale.x;
    private float PipeScaleY => YScale * pipe1.transform.localScale.y;
    private float PipeScaleZ => ZScale * pipe1.transform.localScale.z;

    private bool _canMove;
    private void Awake()
    {
        loseEvent.Register(SetNoMove);
        startEvent.Register(SetMove);
        SetMove();
    }

    private void OnDestroy()
    {
        loseEvent.Unregister(SetNoMove);
        startEvent.Unregister(SetMove);
    }

    private void Update()
    {
        ApplyMove();
    }

    private void OnValidate()
    {
        SetupPipeGroup();
    }

    private void ApplyMove()
    {
        if(!_canMove) return;
        transform.position -= Vector3.right * (Speed * Time.deltaTime);
    }

    private void SetMove() => _canMove = true;
    private void SetNoMove() => _canMove = false;
    
    public void SetupPipeGroup()
    {
        //Update range pipe
        pipe2.transform.position = pipe1.transform.position + Vector3.up * (RangePipe + PipeScaleY);

        //Update rotate pipe
        pipe1.transform.rotation = Quaternion.Euler(pipe1.transform.eulerAngles.x, 0, rotateZ);

        var eulerAnglesPipe1 = pipe1.transform.eulerAngles;
        var rotateForPipe2 = new Vector3(eulerAnglesPipe1.x + 180f, 0, eulerAnglesPipe1.z);

        pipe2.transform.rotation = Quaternion.Euler(rotateForPipe2);

        //Update scoreCollider
        var scaleX = PipeScaleX * Cos(rotateZ);
        var scaleY = PipeScaleY - PipeScaleY * Sin(90f - rotateZ) + RangePipe;
        scoreCollider.size = new Vector2(scaleX, scaleY);

        var posX = PipeScaleX / 2f * Cos(rotateZ) - PipeScaleY / 2f * Sin(rotateZ) - scaleX / 2f;
        var posY = PipeScaleX / 2f * Sin(rotateZ) + PipeScaleY / 2f * Cos(rotateZ) + scaleY / 2f;
        scoreCollider.offset = new Vector2(posX, posY);
    }

    private static float Sin(float rotate)
    {
        return Mathf.Sin(rotate * Mathf.Deg2Rad);
    }

    private static float Cos(float rotate)
    {
        return Mathf.Cos(rotate * Mathf.Deg2Rad);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.GetComponent<Bird>()) return;
        GameController.Instance.CurrentScore++;
        scoreUpdateEvent.Raise();
    }
}