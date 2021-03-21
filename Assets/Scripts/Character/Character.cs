using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    public static Character instance = null;
    
    private Vector3 direction;
    private float _widthOfScreen;
    
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _jumpPower = 3.0f;
    
    int extraJump = 1;
    private bool _isGrounded = false;

    private Rigidbody2D _rigidbody;

    private BoxCollider2D _collider;
    
    public int _levelOfRoad = 1;

    public int LevelOfRoad => _levelOfRoad;

    public float _newRoadTime = 0.0f;

    public bool dead = false;

    private AudioSource _audioSource;

    [SerializeField] private AudioClip[] _audioClips;
    // Start is called before the first frame update
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
        }

        dead = false;        
        _levelOfRoad = 1;
        _newRoadTime = Time.time;
        _collider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _widthOfScreen = Camera.main.orthographicSize * Camera.main.aspect;
        direction = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        if(dead) return;
        
        Run();
        if (Time.time - _newRoadTime >= 0.3f && GameManager.instance.gameAcitve) //if set new road need to make delay 
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            extraJump = 1;
        }
        if (Input.GetMouseButtonDown(0) && extraJump > 0)
        {
            _audioSource.clip = _audioClips[1];
            _audioSource.Play();
            _rigidbody.velocity = Vector2.up * _jumpPower;
            extraJump--;
        } 

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            Kill();
        }
        _isGrounded = collision.gameObject.name == "Ground";
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        _isGrounded = collision.gameObject.name != "Ground";
    }

    public void Kill()
    {
        _audioSource.clip = _audioClips[0];
        _audioSource.Play();
        dead = true;
    }

}
