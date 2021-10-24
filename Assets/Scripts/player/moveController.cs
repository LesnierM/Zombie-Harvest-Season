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
    //Variables Expuestas
    [Header("Chequeo de piso")]
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _sphereRadious;
    [SerializeField] float _groundCheckOffetStanding;
    [SerializeField] float _groundCheckOffetCrouching;
    [Header("Velocidades")]
    [SerializeField] float _walkSpeed;
    [SerializeField] float _runMultiplier;
    [SerializeField] float _lateralSpeed;
    [SerializeField] float _backSpeed;
    [SerializeField] float _crouchSpeed;
    [SerializeField] float _aimingSpeed;
    [SerializeField] float _airSpeedmultiplier;
    [Header("Gravedad")]
    [SerializeField] float _gravityOnGrounded;
    /// <summary>
    /// Para ajustar la gravedad sin modificar el valor original
    /// </summary>
    [SerializeField] float _gravitymultiplier;
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

    float _gravity = -9.8f;
    float _groundCheckOffet;
    bool _diagonalMovement;//velocidad cuando s e corre endiagonal
    float _yVelocity;
    Vector2 _input;
    //Componentes
    AudioSource _soundPlayer;
    //Clases
    GameActions _playerActions;
    CharacterController _characterController;
    weaponsController _weaponsController;
    void Awake()
    {
        _groundCheckOffet = _groundCheckOffetStanding;//establecer el offset del grounddetector
        _weaponsController = GetComponent<weaponsController>();
        _soundPlayer = GetComponent<AudioSource>();
        _characterController = GetComponent<CharacterController>();
    }
    void FixedUpdate()
    {
        #region Ground Check
        _isGrounded = Physics.CheckSphere(transform.position + (Vector3.down * (_characterController.height / 2+_groundCheckOffet)), _sphereRadious, _groundLayer);
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
            _zSpeed *= _airSpeedmultiplier;
            _xSpeed *= _airSpeedmultiplier;
        }
        #endregion

        #region Gravedad

        if (!_isGrounded)
        {
            _yVelocity += (_gravity* Time.deltaTime*_gravitymultiplier) * Time.deltaTime;
        }
        else
        {
            _yVelocity = _gravityOnGrounded * Time.deltaTime;
        }
        #endregion

        #region Salto
        if (_isGrounded && _jumpPressed && !_isCrouch)
        {
            _yVelocity = Mathf.Sqrt(-2 * (_gravity *_gravitymultiplier)*_jumpHigh) * Time.deltaTime;
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3.down * (GetComponent<CharacterController>().height / 2 + _groundCheckOffet)), _sphereRadious);
    }
    private void OnEnable()
    {
        if (_playerActions == null)
        {
            _playerActions = new GameActions();
        }
        _playerActions.Enable();
        _playerActions.playerActions.Move.performed += data => _input = data.ReadValue<Vector2>();
        _playerActions.playerActions.Move.canceled += data => _input = data.ReadValue<Vector2>();
        _playerActions.playerActions.Run.performed += _ => _runPressed = true;
        _playerActions.playerActions.Run.canceled += _ => _runPressed = false;
        _playerActions.playerActions.Jump.performed += OnJump;
        _playerActions.playerActions.Crouch.performed += OnCrouch;
        _playerActions.playerActions.Crouch.canceled += OnCrouch;
    }
    private void OnDisable()
    {
        _playerActions.Disable();
        _playerActions.playerActions.Move.performed -= data => _input = data.ReadValue<Vector2>();
        _playerActions.playerActions.Move.canceled -= data => _input = data.ReadValue<Vector2>();
        _playerActions.playerActions.Run.performed -= _ => _runPressed = true;
        _playerActions.playerActions.Run.canceled -= _ => _runPressed = false;
        _playerActions.playerActions.Jump.performed -= OnJump;
        _playerActions.playerActions.Crouch.performed -= OnCrouch;
        _playerActions.playerActions.Crouch.canceled -= OnCrouch;
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
            _groundCheckOffet = _groundCheckOffetCrouching;
        }
        else if ((!obj.performed && !_isCrouchToggled) || (_isCrouchToggled && obj.performed))
        {
            _groundCheckOffet = _groundCheckOffetStanding;
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
    #endregion

}
