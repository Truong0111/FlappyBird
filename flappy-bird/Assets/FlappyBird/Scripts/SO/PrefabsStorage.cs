using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PrefabsStorage", menuName = "Data/PrefabsStorage", order = 2)]
public class PrefabsStorage : ScriptableObject
{
    public Bird bird;
    public Pipe pipe;
    public PipeGroup pipeGroup;
}