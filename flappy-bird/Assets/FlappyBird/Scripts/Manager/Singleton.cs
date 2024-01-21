/*
 * Singleton.cs
 * 
 * - Unity Implementation of Singleton template
 * 
 */

using System;
using UnityEngine;

/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    public virtual void Awake()
    {
#if UNITY_EDITOR
        if (Instance)
        {
            Debug.LogError($"Multiple instances of Singleton type {typeof(T)} is created.");
        }

        if (this is not T)
        {
            Debug.LogError($"T does not inherit from {typeof(Singleton<>)}.");
            return;
        }
#endif
        Instance = this as T;
    }
}