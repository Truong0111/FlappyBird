using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private VoidEvent scoreUpdateEvent;

    [SerializeField] private Image[] scoreImages;
    [SerializeField] private Sprite[] scoreSprites;

    private void Awake()
    {
        scoreUpdateEvent.Register(UpdateScore);
    }

    private void OnDestroy()
    {
        scoreUpdateEvent.Unregister(UpdateScore);
    }

    private void UpdateScore()
    {
        var score = ExtractDigits(GameController.Instance.CurrentScore);

        for (var i = 0; i < scoreImages.Length; i++)
        {
            scoreImages[i].gameObject.SetActive(i < score.Length);
        }

        for (var i = 0; i < score.Length; i++)
        {
            scoreImages[i].sprite = scoreSprites[score[i]];
        }
    }

    private static int CountDigits(int n)
    {
        if (n == 0) return 1;
        var count = 0;
        while (n != 0)
        {
            n /= 10;
            count++;
        }

        return count;
    }

    private static int[] ExtractDigits(int n)
    {
        if (n == 0) return new[] { 0 };
        
        var digitCount = CountDigits(n);
        var digits = new int[digitCount];
        
        for (var i = digitCount - 1; i >= 0; i--)
        {
            digits[i] = n % 10;
            n /= 10;
        }

        return digits;
    }
}
