using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovController : MonoBehaviour
{
    private GameObject _target;
    private Vector3 _startPos;
    [SerializeField] private float _speed = 2.0f;

    
    void Start()
    {
        _startPos = transform.position;
        _target = Character.instance.gameObject;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.gameAcitve) return;
        Vector3 posTarg = transform.position;
        posTarg.y = _target.transform.position.y + Camera.main.orthographicSize - 2.0f;
        
        transform.position = Vector3.Lerp(transform.position, posTarg, Time.deltaTime * _speed);
    }
}
