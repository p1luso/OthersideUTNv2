using System;
using UnityEngine;

// These videos take long to make so I hope this helps you out and if you want to help me out you can by leaving a like and subscribe, thanks!

public class Movement : MonoBehaviour
{
    //-------CAMERA AND PLAYER VARIABLES----------
    [SerializeField] Transform _playerCamera;

    //-------CAMERA MOVEMENT VARIABLES----------
    [SerializeField][Range(0.0f, 0.5f)] float _mouseSmoothTime = 0.03f;
    //[SerializeField] bool _cursorLock = true;
    [SerializeField] float _mouseSensitivity = 0.5f;

    //-------PLAYER MOVEMENT VARIABLES----------
    public float _speed;
    [SerializeField] float _walkSpeed = 6.0f;
    [SerializeField] float _runSpeed = 8.0f;
    [SerializeField][Range(0.0f, 0.5f)] float _moveSmoothTime = 0.3f;
    [SerializeField] float _gravity = -9.8f;
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _ground;

    public float _jumpHeight = 6f;
    float _velocityY;
    bool _isGrounded;
    public float crouchHeight = 1.0f;
    private float originalHeight;
    private float originalCenter;
    public bool crouch = false;

    float _cameraCap;

    //-------CAMERA MOVEMENT VARIABLES----------
    Vector2 _currentMouseDelta;
    Vector2 _currentMouseDeltaVelocity;

    //-------PLAYER MOVEMENT VARIABLES----------
    CharacterController _controller;
    Vector2 _currentDir;
    Vector2 _currentDirVelocity;
    private Vector3 originalScale; // La escala original del jugador
    Vector3 _velocity;

    private PlayerStamina _playerStamina;
    private SFX _sfx;
    private bool isRunningSoundPlaying = false;
    private bool isWalkingSoundPlaying = false;

    internal bool _isMoving;

    void Start()
    {

        _controller = GetComponent<CharacterController>();
        _playerStamina = GetComponent<PlayerStamina>();
        originalHeight = _controller.height;
        originalCenter = _controller.center.y;
        GameObject _player = GameObject.Find("Player");
        _sfx = _player.GetComponent<SFX>();
        

    }

    void Update()
    {
        CameraMovement();
        PlayerMovement();

        //if (_cursorLock)
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //}
    }


    //-------CAMERA MOVEMENT FUNCTION----------
    void CameraMovement()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta, ref _currentMouseDeltaVelocity, _mouseSmoothTime);

        _cameraCap -= _currentMouseDelta.y * _mouseSensitivity;

        _cameraCap = Mathf.Clamp(_cameraCap, -90.0f, 90.0f);

        _playerCamera.localEulerAngles = Vector3.right * _cameraCap;

        transform.Rotate(Vector3.up * _currentMouseDelta.x * _mouseSensitivity);
    }


    //-------PLAYER MOVEMENT FUNCTION----------
    void PlayerMovement()
    {
        _isMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
        _isGrounded = Physics.CheckSphere(_groundCheck.position, 0.2f, _ground);

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        targetDir.Normalize();

        _currentDir = Vector2.SmoothDamp(_currentDir, targetDir, ref _currentDirVelocity, _moveSmoothTime);

        _velocityY += _gravity * 2f * Time.deltaTime;

        Vector3 velocity = (transform.forward * _currentDir.y + transform.right * _currentDir.x) * _speed + Vector3.up * _velocityY;

        _controller.Move(velocity * Time.deltaTime);


        //-------JUMPING----------
        /*if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            _velocityY = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        if (_isGrounded! && _controller.velocity.y < -1f)
        {
            _velocityY = -8f;
        }*/

        //-------CROUCHING----------
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            PlayerCrouch();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            PlayerStand();
        }

        //-------RUNNING----------
        if (_isMoving && Input.GetKey(KeyCode.LeftShift) && !crouch && _playerStamina._canRun == true)
        {
            _speed = _runSpeed;
            if (!isRunningSoundPlaying)
            {
                _sfx.PlaySoundRun();
                isRunningSoundPlaying = true;
                isWalkingSoundPlaying = false;
            }
        }
        else if (_isMoving && !crouch)
        {
            _speed = _walkSpeed;
            if (!isWalkingSoundPlaying)
            {
                _sfx.PlaySoundWalk();
                isWalkingSoundPlaying = true;
                isRunningSoundPlaying = false;
            }
        }
        else
        {
            _sfx.FadeOutSound(_sfx._audioSource);
            isRunningSoundPlaying = false;
            isWalkingSoundPlaying = false;
        }
    }

    //-------CROUCHING----------
    void PlayerCrouch()
    {
        if (!crouch)
        {
            _speed = _walkSpeed / 2;
            _controller.height = crouchHeight;
            _controller.center = new Vector3(0, 0.3f, 0);
            crouch = true;
        }
    }

    //-------STANDING UP----------
    void PlayerStand()
    {
        if (crouch)
        {
            _speed = _walkSpeed;
            _controller.height = originalHeight;
            _controller.center = new Vector3(0, originalCenter, 0);

            crouch = false;
        }
    }
}