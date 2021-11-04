using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ObjectStates
{
    Idle,
    Close,
    Open,
    Moving
}
public enum DoorOpenDirections
{
    Positive,
    Negative
}
/// <summary>
/// Referencia de la ui para mostrar los mensajes.
/// </summary>
public enum ActionableObjects
{
    None,
    Openable,
    Manipulable
}
public enum ActionsTypes
{
    None,
    Perform,
    Cancel
}
public enum ControlType
{
    Keyboard,
    Gamepad
}
public enum ActionTypes
{
    YRotation,
    YTranslation,
    XRotation,
    ZRotation
}
public enum SoundType
{
    AudioSource,
    Door,
    Gate
}
public class gameManager : MonoBehaviour
{
    public delegate void NoParameters(ControlType Type);
    public event NoParameters oninputDiviceChanged;
    public static ControlType _lastInputDeviceUsed;
    GameActions _playerActions;
    private void Awake()
    {
        if (GameObject.FindObjectsOfType<gameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnEnable()
    {
        if (_playerActions == null)
        {
            _playerActions = new GameActions();
        }
        _playerActions.Enable();
        _playerActions.playerActions.Jump.performed += updateUsedDivice;
        _playerActions.playerActions.Action.performed += updateUsedDivice;
        _playerActions.playerActions.Aim.performed += updateUsedDivice;
        _playerActions.playerActions.ChangeWeapon.performed += updateUsedDivice;
        _playerActions.playerActions.Crouch.performed += updateUsedDivice;
        _playerActions.playerActions.LookX.performed += updateUsedDivice;
        _playerActions.playerActions.LookY.performed += updateUsedDivice;
        _playerActions.playerActions.Move.performed += updateUsedDivice;
        _playerActions.playerActions.Reload.performed += updateUsedDivice;
        _playerActions.playerActions.Run.performed += updateUsedDivice;
        _playerActions.playerActions.Shoot.performed += updateUsedDivice;
    }

    private void OnDisable()
    {
        _playerActions.Disable();
        _playerActions.playerActions.Jump.performed -= updateUsedDivice;
        _playerActions.playerActions.Action.performed -= updateUsedDivice;
        _playerActions.playerActions.Aim.performed -= updateUsedDivice;
        _playerActions.playerActions.ChangeWeapon.performed -= updateUsedDivice;
        _playerActions.playerActions.Crouch.performed -= updateUsedDivice;
        _playerActions.playerActions.LookX.performed -= updateUsedDivice;
        _playerActions.playerActions.LookY.performed -= updateUsedDivice;
        _playerActions.playerActions.Move.performed -= updateUsedDivice;
        _playerActions.playerActions.Reload.performed -= updateUsedDivice;
        _playerActions.playerActions.Run.performed -= updateUsedDivice;
        _playerActions.playerActions.Shoot.performed -= updateUsedDivice;
    }
    public void updateUsedDivice(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ControlType _temp = ControlType.Keyboard;
        if (obj.action.activeControl.ToString().Contains("/XInputControllerWindows/"))
        {
            _temp = ControlType.Gamepad;
        }
        if (_temp != _lastInputDeviceUsed)
        {
            if (oninputDiviceChanged != null)
            {
                oninputDiviceChanged(_temp);
            }
        }
        _lastInputDeviceUsed = _temp;
    }
}
public static class converter
{
    public static ActionableObjects convertLayerToActionableObject(int Layer)
    {
        switch (Layer)
        {
            case 14:
                return ActionableObjects.Openable;
            default:
                return ActionableObjects.None;
        }
    }
    public static ObjectStates getDoorActionFromActionType(ActionsTypes State)
    {
        switch (State)
        {
            case ActionsTypes.Perform:
                return ObjectStates.Open;
            case ActionsTypes.Cancel:
                return ObjectStates.Close;
            default:
                return ObjectStates.Idle;
        }
    }
    /// <summary>
    /// Convertir el valor de DoorState en ActionTypes.
    /// </summary>
    /// <param name="State">El estado actual de la puerta.</param>
    /// <returns>La accion que se debe tomar en caso de un estado expecifico.</returns>
    public static ActionsTypes getActionFromDoorState(ObjectStates State)
    {
        switch (State)
        {
            case ObjectStates.Close:
                return ActionsTypes.Perform;
            case ObjectStates.Open:
                return ActionsTypes.Cancel;
            default:
                return ActionsTypes.None;
        }
    }
}