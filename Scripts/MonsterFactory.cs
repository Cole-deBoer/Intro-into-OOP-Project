using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MonsterFactory : MonoBehaviour
{
    private GameObject _ghostMesh;
    private GameObject _skeletonMesh;
    private GameObject _goblinMesh;

    private float _amountSpawned = 1f;
    private float _frequencyOfSpawns = 15f;

    private int _selectMonster;
    
    private void Awake()
    {
        _ghostMesh = (GameObject)Resources.Load("Ghost");
        _ghostMesh.AddComponent<Ghost>();

        _skeletonMesh = (GameObject)Resources.Load("Skeleton");
        _skeletonMesh.AddComponent<Skeleton>();

        _goblinMesh = (GameObject)Resources.Load("Goblin");
        _goblinMesh.AddComponent<Goblin>();
    }
    
    private void FixedUpdate()
    {
        MonsterSpawner();

        _selectMonster = Random.Range(0, 5);
    }

    private float SetSpawnRate()
    {
        var dynamicSpawnTime = 15;
        
        foreach (var killed in GameController.enemiesDefeated)
        {
            if (_amountSpawned <= 10) _amountSpawned++;

            if (dynamicSpawnTime >= 4) dynamicSpawnTime--;
        }

        return dynamicSpawnTime;
    }

    private Vector3 SetSpawnLocations()
    {
        var randomRange = Random.Range(0, 5);

        var spawnPos = randomRange switch
        {
            0 => new Vector3(-12, 0, -3),
            1 => new Vector3(-12, 0, -13),
            2 => new Vector3(-15, 0, -13),
            3 => new Vector3(-2, 0, -3),
            4 => new Vector3(-2, 0, -10),
            5 => new Vector3(4, 0, -7),
            _ => default
        };

        return spawnPos;
    }
    
    private void MonsterSpawner()
    {
        _frequencyOfSpawns -= Time.deltaTime;
        
        if (!(_frequencyOfSpawns <= 0)) return;
        switch (_selectMonster)
        {
            case 0:
                Instantiate(_ghostMesh, SetSpawnLocations(), Quaternion.identity);
                _frequencyOfSpawns = SetSpawnRate();
                break;
            case 1:
                Instantiate(_skeletonMesh, SetSpawnLocations(), Quaternion.identity);
                _frequencyOfSpawns = SetSpawnRate();
                break;
            case 3:
                Instantiate(_goblinMesh, SetSpawnLocations(), Quaternion.identity);
                _frequencyOfSpawns = SetSpawnRate();
                break;
        }
    }

    private void OnApplicationQuit()
    {
        DestroyImmediate(_ghostMesh.GetComponent<Ghost>(), true);
        DestroyImmediate(_skeletonMesh.GetComponent<Skeleton>(), true);
        DestroyImmediate(_goblinMesh.GetComponent<Goblin>(), true);
    }
}
