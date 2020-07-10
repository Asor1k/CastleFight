using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Timer",menuName = "Settings/Timer",order = 0)]
public class TimerConfig : ScriptableObject
{
    [SerializeField] private float spawnTime;

    public float SpawnTime => spawnTime;
    
}
