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
        Application.focusChanged += Application_focusChanged;
    }

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
    private void OnEnable()
    {
        _playerActions = new GameActions();
        _playerActions.Enable();

        #region mouse
        _playerActions.playerActions.MouseXMove.performed += mouseInput => _velocity.x = mouseInput.ReadValue<float>()*_mouseSensitivity.x;
        _playerActions.playerActions.MouseXMove.canceled += mouseInput => _velocity.x = mouseInput.ReadValue<float>()*0;
        _playerActions.playerActions.MouseYMove.performed += mouseInput => _velocity.y = mouseInput.ReadValue<float>()*_mouseSensitivity.y;
        _playerActions.playerActions.MouseYMove.canceled += mouseInput => _velocity.y = mouseInput.ReadValue<float>()*0;
        #endregion

        #region Gamepad
        _playerActions.playerActions.GamepadXMove.performed += gamepadInput => _velocity.x = gamepadInput.ReadValue<float>() * _gamepadSensitivity.x;
        _playerActions.playerActions.GamepadXMove.canceled += gamepadInput => _velocity.x = gamepadInput.ReadValue<float>() * 0;
        _playerActions.playerActions.GamepadYMove.performed += gamepadInput => _velocity.y = gamepadInput.ReadValue<float>() * _gamepadSensitivity.y;
        _playerActions.playerActions.GamepadYMove.canceled += gamepadInput => _velocity.y = gamepadInput.ReadValue<float>() * 0;
        #endregion
    }
    private void OnDisable()
    {
        _playerActions.Disable();
    }

    #region Eventos

    #endregion

    #region Metodos

    #endregion

    #region Propiedades
    public GameActions PlayerActions { get => _playerActions;}
    #endregion

}
