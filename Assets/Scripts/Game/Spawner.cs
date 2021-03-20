using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Spawner: MonoBehaviour
{
    [SerializeField][Range(3.0f, 10.0f)] private float _distBetweenBlocks = 5.0f;
    [SerializeField] private int _maxGroupsOfBlocks = 7;
    
    private float _screenHight;
    
    private List<GameObject> _listOfGroupBlocks;
    private List<GameObject> _listOfEnemys;
    
    private Character _character;
    // Start is called before the first frame update
    void Awake()
    {
        _listOfEnemys = new List<GameObject>();
        _listOfGroupBlocks = new List<GameObject>();
        _character = FindObjectOfType<Character>();
        _screenHight = Camera.main.orthographicSize;
        
        SpawnBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        _listOfGroupBlocks[0].GetComponent<BoxCollider2D>().enabled = true;
        
        CheckIfBlocksOutOfBounds();
        CheckIfEnemyOutOfBounds();
    }

    private void CheckIfBlocksOutOfBounds()
    {
        if (_listOfGroupBlocks[0].transform.position.y < Camera.main.transform.position.y - _screenHight - 0.5f)
        {
            _listOfGroupBlocks[0].GetComponent<BoxCollider2D>().enabled = false;
            _listOfGroupBlocks[0].SetActive(false);
            _listOfGroupBlocks.Remove(_listOfGroupBlocks[0]);

            Vector3 pos = _listOfGroupBlocks[_maxGroupsOfBlocks - 1].transform.position;
            pos.y += _distBetweenBlocks;
            
            
            _listOfGroupBlocks.Add(Pooler.Instance.SpawnPoolObject("GroupOfBlocks", 
                pos, 
                Quaternion.identity));

            _listOfGroupBlocks[_maxGroupsOfBlocks - 1].GetComponent<BoxCollider2D>().enabled = false;

            _character._newRoadTime = Time.time; //to block jumps between roads
            _character._levelOfRoad++;

        }
    }

    private void SpawnBlocks()
    {
        Vector3 spawnPoint = Vector3.zero;
        _listOfGroupBlocks.Add(Pooler.Instance.SpawnPoolObject("GroupOfBlocks", spawnPoint, Quaternion.identity));
        _listOfGroupBlocks[0].GetComponent<BoxCollider2D>().isTrigger = false;
        for (int i = 1; i <= _maxGroupsOfBlocks; i++)
        {
            spawnPoint.y += _distBetweenBlocks;
            if (i >= 3)
            {
                Vector3 posOfEnem = spawnPoint;
                posOfEnem.y += 0.6f;            
                SpawnEnemy(posOfEnem);
            }
            
            _listOfGroupBlocks.Add(Pooler.Instance.SpawnPoolObject("GroupOfBlocks", spawnPoint, Quaternion.identity));
            _listOfGroupBlocks[i].GetComponent<BoxCollider2D>().enabled = false;
        }

    }

    private void SpawnEnemy(Vector3 pos)
    {
        float screenWitdh = Camera.main.orthographicSize * Camera.main.aspect;
        pos.x += Random.Range(-screenWitdh + 0.5f, screenWitdh - 0.5f);
        _listOfEnemys.Add(Pooler.Instance.SpawnPoolObject(Random.Range(0.0f, 1.0f) > 0.4f ? "IdleEnemy" : "MoveEnemy",
                                        pos,
                                        Quaternion.identity) );
    }

    private void CheckIfEnemyOutOfBounds()
    {
        if (_listOfEnemys[0].transform.position.y < Camera.main.transform.position.y - _screenHight - 0.5f)
        {
            _listOfEnemys[0].SetActive(false);
            _listOfEnemys.Remove(_listOfEnemys[0]);
            Vector3 posOfEnem = _listOfGroupBlocks[_maxGroupsOfBlocks - 1].transform.position;
            posOfEnem.y += 0.6f;            
            SpawnEnemy(posOfEnem);

        }
    }
}
