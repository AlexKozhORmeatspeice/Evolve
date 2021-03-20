using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    
    private float _widthOfScreen;

    private Vector3 direction;
    // Start is called before the first frame update
    void Awake()
    {
        direction = Vector3.right;
        _widthOfScreen = Camera.main.orthographicSize * Camera.main.aspect;

    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }
    
    
    private void Run()
    {
        if (transform.position.x >= _widthOfScreen - transform.localScale.x)
        {
            direction = Vector3.left;
        }
        if (transform.position.x <= -_widthOfScreen + transform.localScale.x)
        {
            direction = Vector3.right;
        }
        
        transform.position = Vector3.Lerp(transform.position, transform.position + direction, Time.deltaTime * _speed);
    }
}
