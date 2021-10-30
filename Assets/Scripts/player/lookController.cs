using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class lookController : MonoBehaviour
{
	//Variables Expuestas
    [Header("Sensibilidad del mouse")]
	[SerializeField] Vector2 _mouseSensitivity;
    [Header("Sensibilidad del teclado")]
    [SerializeField] Vector2 _gamepadSensitivity;
    [SerializeField] float _verticalBounds;
    //Variables
    Vector2 _velocity;
    float _verticalAngle;
    //Componentes
	 Transform _camera;
    //Clases
    GameActions _playerActions;
    CharacterController _characterController;
    void Awake()
    {
        _playerActions = new GameActions();
		_camera = Camera.main.transform;
    }
    private void OnEnable()
    {
        if (_playerActions == null)
        {
            _playerActions = new GameActions();
        }
        _playerActions.Enable();
        _playerActions.playerActions.LookX.performed += LookX;
        _playerActions.playerActions.LookX.canceled += mouseInput =>  _velocity.x = mouseInput.ReadValue<float>() * 0;
        _playerActions.playerActions.LookY.performed += LookY;
        _playerActions.playerActions.LookY.canceled += mouseInput => _velocity.y = mouseInput.ReadValue<float>() * 0 ;
        Application.focusChanged += Application_focusChanged;
    }
    private void OnDisable()
    {
        _playerActions.playerActions.LookX.performed -= LookX;
        _playerActions.playerActions.LookX.canceled -= mouseInput => _velocity.x = mouseInput.ReadValue<float>() * 0;
        _playerActions.playerActions.LookY.performed -= LookY;
        _playerActions.playerActions.LookY.canceled -= mouseInput => _velocity.y = mouseInput.ReadValue<float>() * 0;
        Application.focusChanged -= Application_focusChanged;
        _playerActions.Disable();
    }
    void FixedUpdate()
    {

        #region Giro Horizontal
        transform.Rotate( Vector3.up *_velocity.x*  Time.deltaTime);
        #endregion

        #region Giro Vertical
        _verticalAngle -= _velocity.y * Time.deltaTime;
        _verticalAngle = Mathf.Clamp(_verticalAngle, -_verticalBounds, _verticalBounds);
        _camera.localRotation = Quaternion.Euler(_verticalAngle, 0, 0);
        #endregion
    }

    #region Inputs
    private void LookY(InputAction.CallbackContext obj)
    {
        _velocity.y = obj.ReadValue<float>();
        switch (gameManager._lastInputDeviceUsed)
        {
            case ControlType.Keyboard:
                _velocity.y *= _mouseSensitivity.y;

                break;
            case ControlType.Gamepad:
                _velocity.y *= _gamepadSensitivity.y;
                break;
        }
    }

    private void LookX(InputAction.CallbackContext obj)
    {
        _velocity.x = obj.ReadValue<float>();
        switch (gameManager._lastInputDeviceUsed)
        {
            case ControlType.Keyboard:
                _velocity.x *= _mouseSensitivity.x;

                break;
            case ControlType.Gamepad:
                _velocity.x *= _gamepadSensitivity.x;
                break;
        }
    }
    #endregion

    #region Eventos
    private void Application_focusChanged(bool obj)
    {
        if (obj)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    #endregion

    #region Metodos

    #endregion

    #region Propiedades
    public GameActions PlayerActions { get => _playerActions;}
    #endregion

}
