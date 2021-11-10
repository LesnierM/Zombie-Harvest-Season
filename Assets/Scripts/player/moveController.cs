using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class moveController : MonoBehaviour
{
    public delegate void StatesEventHandler(bool State);
    public event StatesEventHandler OnRunStateChange;
    public event StatesEventHandler OnWalkStateChange;
    public event StatesEventHandler OnidletateChange;
    public event StatesEventHandler OnGroundedStateChange;
    //Variables Expuestas

    [Header("Velocidades")]
    [SerializeField] float _walkSpeed;
    [SerializeField] float _runMultiplier;
    [SerializeField] float _lateralSpeed;
    [SerializeField] float _backSpeed;
    [SerializeField] float _crouchSpeed;
    [SerializeField] float _aimingSpeed;
    [SerializeField] float _airSpeedReducer;
    [SerializeField] float _waterSpeedReducer;
    [Header("Gravedad")]
    [SerializeField] float _gravityOnGrounded;
    [SerializeField] float _gravityModifierInWater;
    /// <summary>
    /// Para ajustar la gravedad sin modificar el valor original
    /// </summary>
    [SerializeField] float _gravityModifier;
    [Header("Alturas")]
    [SerializeField] float _jumpHigh;
    [SerializeField] float _crouchHeigh;
    [SerializeField] float _normalHeigh;
    [Header("opciones")]
    [SerializeField] bool _isCrouchToggled;
    //Variables
    bool _runPressed;
    bool _isRunning;
    bool _jumpPressed;
    bool _isCrouch;
    bool _isGrounded;
    bool _isIdle=true;
    bool _diagonalMovement;//velocidad cuando s e corre endiagonal

    float _gravity = -9.8f;
    float _yVelocity;
    float _tempGravityModifier;//guardar el modificador de gravedad en estado normal antes de editarlo para devolverlo al valor definido en el editor

    Vector2 _input;
    //Componentes
    AudioSource _soundPlayer;
    //Clases
    GameActions _playerActions;
    CharacterController _characterController;
    weaponsController _weaponsController;
    collisionController _collisionController;
    void Awake()
    {
        _weaponsController = GetComponent<weaponsController>();
        _soundPlayer = GetComponent<AudioSource>();
        _characterController = GetComponent<CharacterController>();
        _collisionController = GetComponent<collisionController>();
    }
    private void OnEnable()
    {
        if (_playerActions == null)
        {
            _playerActions = new GameActions();
        }
        _playerActions.Enable();
        _playerActions.playerActions.Move.performed += data => { _input = data.ReadValue<Vector2>(); };
        _playerActions.playerActions.Move.canceled += data => { _input = data.ReadValue<Vector2>(); };
        _playerActions.playerActions.Run.performed += data => { _runPressed = true;  };
        _playerActions.playerActions.Run.canceled += data => { _runPressed = false; };
        _playerActions.playerActions.Jump.performed+= OnJump;
        _playerActions.playerActions.Crouch.performed += OnCrouch;
        _playerActions.playerActions.Crouch.canceled += OnCrouch;

        _collisionController.OnWaterStateChange += OnWaterStateChange;
    }
    private void OnDisable()
    {
        _playerActions.Disable();
        _playerActions.playerActions.Move.performed -= data => {_input =  data.ReadValue<Vector2>(); };
        _playerActions.playerActions.Move.canceled -= data => { _input = data.ReadValue<Vector2>(); };
        _playerActions.playerActions.Run.performed -= data => { _runPressed = true; ;};
        _playerActions.playerActions.Run.canceled -= data => { _runPressed = false; ; };
        _playerActions.playerActions.Jump.performed -= OnJump;
        _playerActions.playerActions.Crouch.performed -= OnCrouch;
        _playerActions.playerActions.Crouch.canceled -= OnCrouch;

        _collisionController.OnWaterStateChange -= OnWaterStateChange;
    }
    void FixedUpdate()
    {
        #region Ground Check
        //prueba verificar solo si esta de acaida y n ode subid
        bool _tempGrounded = _isGrounded;
        if (_yVelocity < 0)
        {
            _isGrounded = _collisionController.checkGrounded();
        }
        if(_tempGrounded != _isGrounded)
        {
            OnGroundedStateChange(_isGrounded);
        }
        //Debug.Log(_isGrounded);
        #endregion

        #region Normalizar la velocidad diagonal
        if (_input.sqrMagnitude > 1)
        {
            _input = Vector2.ClampMagnitude(_input, 1f);
            _diagonalMovement = true;
        }
        #endregion

        #region Crear las velocidades de las direcciones
        float _zSpeed =_weaponsController.IsAiming?_input.y*_aimingSpeed*Time.deltaTime: calculateZVelocity();
        float _xSpeed = _weaponsController.IsAiming ? _input.x * _aimingSpeed * Time.deltaTime : calculateXVelocity();
        //velocidad cuando este en el aire
        if (!_isGrounded)
        {
            _zSpeed *= _airSpeedReducer;
            _xSpeed *= _airSpeedReducer;
        }
        #endregion

        #region Gravedad

        if (!_isGrounded)
        {
            _yVelocity += (_gravity* Time.deltaTime*_gravityModifier) * Time.deltaTime;
        }
        else
        {
            _yVelocity = _gravityOnGrounded * Time.deltaTime;
        }
        //Debug.Log(_yVelocity);
        #endregion

        #region Salto
        if (_isGrounded && _jumpPressed && !_isCrouch)
        {
            _yVelocity = Mathf.Sqrt(-2 * (_gravity *_gravityModifier)*_jumpHigh) * Time.deltaTime;
            _isGrounded = false;
            AkSoundEngine.PostEvent("Jump", gameObject);
        }
        _jumpPressed = false;
       
        #endregion

        #region Notificar estados de movimiento
        float _currentMovementVelocity =new Vector2(_characterController.velocity.x, _characterController.velocity.z).sqrMagnitude;
        if (_currentMovementVelocity > 70)
        {
            OnRunStateChange(true);
            OnWalkStateChange(false);
            OnidletateChange(false);
            _isIdle = false;
            _isRunning = true;
        }
        else if (_currentMovementVelocity < 70 && _currentMovementVelocity != 0)
        {
            OnRunStateChange(false);
            OnWalkStateChange(true);
            OnidletateChange(false);
            _isIdle = false;
            _isRunning = false;
        }
        else if (_currentMovementVelocity == 0)
        {
            OnRunStateChange(false);
            OnWalkStateChange(false);
            OnidletateChange(true);
            _isRunning = false;
            _isIdle = true;
        }
        #endregion

        _characterController.Move(transform.TransformDirection(new Vector3( _xSpeed, _yVelocity, _zSpeed)));
        //Debug.Log(new Vector2(_characterController.velocity.x, _characterController.velocity.z).sqrMagnitude);
    }

    #region Input
    private void OnJump(InputAction.CallbackContext obj)
    {
        _jumpPressed = obj.performed;
        if (_isCrouch)
        {
            stand();
        }
    }
    private void OnCrouch(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        StartCoroutine(performCrouch(obj));
    }

    #endregion

    #region Eventos
    private void OnWaterStateChange()
    {
        if (_collisionController.IsInWater)
        {
            _tempGravityModifier = _gravityModifier;
            _gravityModifier = _gravityModifierInWater;
        }
        else
        {
            _gravityModifier =_tempGravityModifier;
        }
    }
    private IEnumerator performCrouch(InputAction.CallbackContext obj)
    {
        if (!_isGrounded)
        {
           yield return null;
        }

        if (obj.performed && !_isCrouch)
        {
            _isCrouch = true;
            _characterController.height = _crouchHeigh;
        }
        else if ((!obj.performed && !_isCrouchToggled) || (_isCrouchToggled && obj.performed))
        {
            stand();
        }
        yield return new WaitForSeconds(.1f);
    }

    #endregion

    #region Metodos

    private float calculateZVelocity()
    {
        float velocity = _input.y;
        if (_isCrouch && !_runPressed)
        {
            velocity *= _crouchSpeed;
        }
        else if (_isCrouch && _runPressed&&_input.y>0)
        {
            stand();
            velocity *= _walkSpeed * _runMultiplier;
        }
        else if (_input.y > 0)
        {
            if (_runPressed)
            {
                velocity *= _walkSpeed * _runMultiplier;
            }
            else
            {
                velocity *= _walkSpeed;
            }
        }
        else
        {
            velocity *= _backSpeed;
        }
        if (_collisionController.IsInWater)
        {
            velocity *= _waterSpeedReducer;
        }
        return velocity * Time.deltaTime;
    }
    private float calculateXVelocity()
    {
        float velocity=_input.x;
        if (_isCrouch)
        {
            velocity *= _crouchSpeed;
        }
        else if(!_diagonalMovement)
        {
            velocity *= _lateralSpeed;
        }
        else if(_diagonalMovement)
        {
            velocity *= _lateralSpeed *_runMultiplier;
        }
        if (_collisionController.IsInWater)
        {
            velocity *= _waterSpeedReducer;
        }
        return velocity*Time.deltaTime;
    }
    private void stand()
    {
        _characterController.height = _normalHeigh;
        _isCrouch = false;
    }
   
    #endregion

    #region Propiedades
    public bool IsRunning { get => _isRunning; }
    public bool IsGrounded { get => _isGrounded; }
    public bool IsCrouch { get => _isCrouch; }
    public float Gravity { get => _gravity; set => _gravity = value; }
    #endregion

}
