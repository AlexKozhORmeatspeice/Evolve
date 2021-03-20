using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float speed = 2.0f;
    private Vector3 direction;
    private float _widthOfScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        _widthOfScreen = Camera.main.orthographicSize * Camera.main.aspect;
        direction = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    protected void Run()
    {
        if (transform.position.x >= _widthOfScreen - transform.localScale.x)
        {
            direction = Vector3.left;
        }
        if (transform.position.x <= -_widthOfScreen + transform.localScale.x)
        {
            direction = Vector3.right;
        }
        
        
        
        transform.position = Vector3.Lerp(transform.position, transform.position + direction, Time.deltaTime * speed);
    }
}
