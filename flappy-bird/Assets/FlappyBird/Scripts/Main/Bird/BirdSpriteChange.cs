using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class BirdSpriteChange : MonoBehaviour
{
    [SerializeField] private SpriteRenderer birdSprite;
    [SerializeField] private Sprite[] birdSprites;
    [SerializeField] private float timeSpriteUpdate;
    private Bird _bird;
    private int SpriteCount => birdSprites.Length;
    private VoidEvent _start;
    private Coroutine _coroutine;
    
    private void Awake()
    {
        _bird = GetComponent<Bird>();
    }

    private void Start()
    {
        _coroutine = StartCoroutine(UpdateSprite(SpriteCount, ChangeSprite, EndChangeSprite));
    }

    private void ChangeSprite(int currentSpriteIndex)
    {
        birdSprite.sprite = birdSprites[currentSpriteIndex];
    }

    private void EndChangeSprite()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator UpdateSprite(int frameUpdate, Action<int> onUpdate = null, Action endUpdate = null)
    {
        var currentFrame = 0;

        while (true)
        {
            if (_bird.IsDead) break;
            currentFrame++;
            onUpdate?.Invoke(currentFrame % frameUpdate);
            yield return new WaitForSeconds(timeSpriteUpdate);
        }
        endUpdate?.Invoke();
    }
}