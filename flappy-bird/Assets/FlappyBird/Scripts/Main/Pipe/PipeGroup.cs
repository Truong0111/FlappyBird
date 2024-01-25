using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGroup : MonoBehaviour
{
    [SerializeField] private Pipe pipe1;
    [SerializeField] private Pipe pipe2;
    [SerializeField] private BoxCollider2D scoreCollider;
    [SerializeField] private float rangePipe;
    [SerializeField] private float rotateZ;

    private const float XScale = 0.52f;
    private const float YScale = 3.2f;
    private const float ZScale = 1f;

    private float PipeScaleX => XScale * pipe1.transform.localScale.x;
    private float PipeScaleY => YScale * pipe1.transform.localScale.y;
    private float PipeScaleZ => ZScale * pipe1.transform.localScale.z;
    private void OnValidate()
    {
        //Update scoreCollider
        scoreCollider.size = new Vector2(PipeScaleX, rangePipe);
        scoreCollider.offset = new Vector2(0, (rangePipe + PipeScaleY) / 2);

        //Update range pipe
        pipe2.transform.position = pipe1.transform.position + Vector3.up * (rangePipe + PipeScaleY);

        //Update rotate pipe
        pipe1.transform.rotation = Quaternion.Euler(pipe1.transform.eulerAngles.x, 0, rotateZ);

        var eulerAnglesPipe1 = pipe1.transform.eulerAngles;
        var rotateForPipe2 = new Vector3(eulerAnglesPipe1.x + 180f, 0, eulerAnglesPipe1.z);

        pipe2.transform.rotation = Quaternion.Euler(rotateForPipe2);
    }
}