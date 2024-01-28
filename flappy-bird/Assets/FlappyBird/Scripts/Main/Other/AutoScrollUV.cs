using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScrollUV : MonoBehaviour
{
    public int targetMaterialSlot;
    public string propertyName = "_MainTex";
    public Vector2 scrollSpeed;

    private Renderer _rend;
    private Vector2 _offset = Vector2.zero;

    private void Start()
    {
        _rend = GetComponent<Renderer>();
        _offset = _rend.materials[targetMaterialSlot].GetTextureOffset(propertyName);
    }

    private void Update()
    {
        _offset.x += Time.deltaTime * scrollSpeed.x;
        _offset.y += Time.deltaTime * scrollSpeed.y;

        _rend.materials[targetMaterialSlot].SetTextureOffset(propertyName, _offset);
    }
}
