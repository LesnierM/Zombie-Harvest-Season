using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSway : MonoBehaviour
{
    //Variables Expuestas
    [SerializeField] Transform _targetObject;
    //Variables
    Vector2 _mouseInput;
	Quaternion _originalRotation;
    //Componentes
    //Clases
    GameActions _playerActions;
    void Start()
    {
        _originalRotation = _targetObject.localRotation;
    }
    void Update()
    {
        Quaternion _xngleAdjestment = Quaternion.AngleAxis(_mouseInput.x * 1.45f, Vector3.up);
        Quaternion _yngleAdjestment = Quaternion.AngleAxis(_mouseInput.y * 1.45f, Vector3.right);
        Quaternion _targetRotation = _originalRotation * _xngleAdjestment * _yngleAdjestment;
        _targetObject.localRotation = Quaternion.Lerp(_targetObject.localRotation, _targetRotation, 10f);
    }
    private void OnEnable()
    {
        _playerActions = new GameActions();
        _playerActions.Enable();
        //_playerActions.playerActions.MouseXMove.performed += MouseData => _mouseInput.x = MouseData.ReadValue<float>();
        //_playerActions.playerActions.MouseXMove.canceled += MouseData => _mouseInput.x = MouseData.ReadValue<float>();
        //_playerActions.playerActions.MouseYMove.performed += MouseData => _mouseInput.y = MouseData.ReadValue<float>();
        //_playerActions.playerActions.MouseYMove.canceled += MouseData => _mouseInput.y = MouseData.ReadValue<float>();
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

    #endregion

}
