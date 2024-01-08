using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private float _cameraDistance;

    [SerializeField]
    private float _cameraSmoothing;

    public GameManager _gameManager { private get; set; }

    private Vector3 movement;

    private Rigidbody2D _rigidbody;
    private Vector2 _meowment;
    
    void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _camera.transform.position = Vector3.Slerp(_camera.transform.position, new Vector3(transform.position.x, transform.position.y, _cameraDistance), _cameraSmoothing);
        //DoMovement();
        
    }

    private void Update()
    {
        DoMovement();
        //GetInput();
    }

    private void GetInput()
    {
        if (_gameManager.stateHandler.CurrentState == GameStateHandler.GameState.Paused) return;

        transform.Translate(transform.position);
    }
    private void DoMovement()
    {
        if (_gameManager.stateHandler.CurrentState == GameStateHandler.GameState.Paused) return;

        _meowment.x = Input.GetAxisRaw("Horizontal");
        _meowment.y = Input.GetAxisRaw("Vertical");
        _meowment.Normalize();

        _rigidbody.velocity = _meowment * _moveSpeed; // I dont want to use Rigidbody so I will have to rework the whole movement later on I think.
    }
}
