using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private VoidEvent startEvent;
    [SerializeField] private VoidEvent loseEvent;
    
    [SerializeField] private PrefabsStorage prefabsStorage;

    [SerializeField] private float timeToSpawn = 1f;

    private float _timeToSpawn;
    private bool _canSpawn;
    private void Awake()
    {
        startEvent.Register(SetSpawn);
        loseEvent.Register(SetNoSpawn);
        _canSpawn = true;
    }

    private void OnDestroy()
    {
        startEvent.Unregister(SetSpawn);
        loseEvent.Unregister(SetNoSpawn);
    }

    private void Start()
    {
        _timeToSpawn = timeToSpawn;
    }

    private void Update()
    {
        CountTimeToSpawn();
    }

    private void SetSpawn() => _canSpawn = true;
    private void SetNoSpawn() => _canSpawn = false;
    
    private void CountTimeToSpawn()
    {
        if(!_canSpawn) return;
        _timeToSpawn -= Time.deltaTime;
        if (_timeToSpawn <= 0)
        {
            SpawnPipeGroup();
            _timeToSpawn = timeToSpawn;
        }
    }
    
    private void SpawnPipeGroup()
    {
        var spawnPos = new Vector3(7f, -3.5f, 0);
        var pipeGroup = SimplePool.Spawn(prefabsStorage.pipeGroup, spawnPos, Quaternion.identity);

        pipeGroup.Speed = 2f;
        pipeGroup.RangePipe = 3f;
        
        pipeGroup.SetupPipeGroup();
    }
}