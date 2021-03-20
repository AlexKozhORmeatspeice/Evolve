using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SpawnBlocksManager: MonoBehaviour
{
    [SerializeField][Range(3.0f, 10.0f)] private float _distBetweenBlocks = 5.0f;
    [SerializeField] private int _maxGroupsOfBlocksOnScrene = 7;


    private float _screenHight;
    private List<GameObject> _listOfGroupBlocks = new List<GameObject>();

    private Character _character;
    // Start is called before the first frame update
    void Start()
    {
        _character = FindObjectOfType<Character>();
        _screenHight = Camera.main.orthographicSize;
        
        Vector3 spawnPoint = Vector3.zero;
        _listOfGroupBlocks.Add(Pooler.Instance.SpawnPoolObject("GroupOfBlocks", spawnPoint, Quaternion.identity));
        _listOfGroupBlocks[0].GetComponent<BoxCollider2D>().isTrigger = false;
        for (int i = 1; i <= _maxGroupsOfBlocksOnScrene; i++)
        {
            spawnPoint.y += _distBetweenBlocks;
            _listOfGroupBlocks.Add(Pooler.Instance.SpawnPoolObject("GroupOfBlocks", spawnPoint, Quaternion.identity));
            _listOfGroupBlocks[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _listOfGroupBlocks[0].GetComponent<BoxCollider2D>().enabled = true;
        
        if (_listOfGroupBlocks[0].transform.position.y < Camera.main.transform.position.y - _screenHight - 0.5f)
        {
            _listOfGroupBlocks[0].GetComponent<BoxCollider2D>().enabled = false;
            _listOfGroupBlocks[0].SetActive(false);
            _listOfGroupBlocks.Remove(_listOfGroupBlocks[0]);

            Vector3 pos = _listOfGroupBlocks[_maxGroupsOfBlocksOnScrene - 1].transform.position;
            pos.y += _distBetweenBlocks;
            
            _listOfGroupBlocks.Add(Pooler.Instance.SpawnPoolObject("GroupOfBlocks", 
                                    pos, 
                                    Quaternion.identity));

            _listOfGroupBlocks[_maxGroupsOfBlocksOnScrene - 1].GetComponent<BoxCollider2D>().enabled = false;

            _character._newRoadTime = Time.time; //to block jumps between roads
            _character._levelOfRoad++;

        }
    }
}
