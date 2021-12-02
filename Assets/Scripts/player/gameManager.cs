using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
/// 
public enum Materials
{
    Wood,
    Metal
}
public enum SunStates
{
    Day,
    Night
}
public struct GroundData
{
    public bool Colliosined;
    public string Tag;
    public GroundData(bool Grounded, string Tag)
    {
        this.Colliosined = Grounded;
        this.Tag = Tag;
    }
}
public enum WaterLevels
{
    None,
    OnWater,
    HalfInWater,
    InWater
}
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
public enum TagSoundTypes
{
    None,
    Dirt,
    Wood,
    Water,
    MetalOilTank,
    CarsLike,
    MetalGates,
    SmallTractorsLike,
    MetalBarrels,
    BullDozer,
    BigWheelTire,
    WheelAirTire,
    WelcomeSign,
    Glass,
    BigSilo,
    SmallSilo,
    GreenHouse

}
public enum Weapons
{
    None,
    Jericho_941,
}
[Serializable]
public struct WeaponsStruct
{
    public Weapons Weapon;
    public int BulletsTotalCount;
    public int BulletsInCartridge;
    public WeaponsStruct(int BulletsTotalCount, int BulletsInCartridge, Weapons Weapon)
    {
        this.BulletsTotalCount = BulletsTotalCount;
        this.BulletsInCartridge = BulletsInCartridge;
        this.Weapon = Weapon;
    }
}
public enum InteractionActions
{
    None,
    SaveGame
}

public class gameManager : MonoBehaviour
{
    [SerializeField] AudioClip _enviromentDaySound;
    [SerializeField] AudioClip _enviromentNightSound;
    public delegate void NoParameters(ControlType Type);
    public event NoParameters oninputDiviceChanged;
    public static ControlType _lastInputDeviceUsed;
    GameActions _playerActions;
    BinaryFormatter bf = new BinaryFormatter();
    AudioSource _enviromentSoundPlayer;
    sunController _sunState;
    SaveData _gameData;
    weaponsController _weaponsController ;
    public SaveData GameData { get => _gameData; }

    private void Awake()
    {
        _enviromentSoundPlayer = GetComponent<AudioSource>();
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
        if (_playerActions == null)
        {
            return;
        }
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
    private void OnSunStateChange(SunStates State)
    {
        _enviromentSoundPlayer.Stop();
        _enviromentSoundPlayer.clip = State == SunStates.Day ? _enviromentDaySound : _enviromentNightSound;
        _enviromentSoundPlayer.Play();
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
    public void addSunStateListener()
    {
        _sunState = GameObject.FindObjectOfType<sunController>();
        _sunState.OnSunStateChange += OnSunStateChange;
        if (_gameData != null)
        {
            _sunState.transform.rotation = Quaternion.Euler(GameData.SunPosition, 0, 0);
        }
    }
    public void saveGame()
    {
        if (_weaponsController == null)
        {
            _weaponsController = GameObject.FindObjectOfType<weaponsController>();
        }
        _gameData = _gameData == null? new SaveData() : _gameData;
        _weaponsController.updateAdquieredWeaponsToSave();
        _gameData.Weapons =_weaponsController.WeaponsAdquired;
        _gameData.SunPosition = GameObject.Find("Sun").transform.eulerAngles.x;
        _gameData.playerXPosition = _weaponsController.transform.position.x;
        _gameData.playerYPosition = _weaponsController.transform.position.y;
        _gameData.playerZPosition = _weaponsController.transform.position.z;
        FileStream fs = fs = File.Open(Application.persistentDataPath + "\\data.dat", FileMode.Create);
        bf.Serialize(fs, GameData);
    }
    public bool loadGame()
    {
        if (File.Exists(Application.persistentDataPath + "\\data.dat"))
        {
            _gameData =bf.Deserialize(File.Open(Application.persistentDataPath + "\\data.dat", FileMode.Open, FileAccess.Read)) as SaveData;
        }
        return _gameData != null;
    }
    public void newGame()
    {
        _gameData = null;
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
    public static TagSoundTypes getGroundStepSoundTypesFromString(string SoundType)
    {
        switch (SoundType)
        {
            case "Dirt":
                return TagSoundTypes.Dirt;
            case "Wood":
            case "BigObjects":
                return TagSoundTypes.Wood;
            case "Water":
                return TagSoundTypes.Water;
            case "MetalTank":
                return TagSoundTypes.MetalOilTank;
            case "CarsLike":
                return TagSoundTypes.CarsLike;
            case "MetalGates":
                return TagSoundTypes.MetalGates;
            case "SmallTractorsLike":
                return TagSoundTypes.SmallTractorsLike;
            case "MetalBarrels":
                return TagSoundTypes.MetalBarrels;
            case "BullDozer":
                return TagSoundTypes.BullDozer;
            case "BigWheelTire":
                return TagSoundTypes.BigWheelTire;
            case "WheelAirTire":
                return TagSoundTypes.WheelAirTire;
            case "WelcomeSign":
                return TagSoundTypes.WelcomeSign;
            case "Glass":
                return TagSoundTypes.Glass;
            case "BigSilo":
                return TagSoundTypes.BigSilo;
            case "SmallSilo":
                return TagSoundTypes.SmallSilo; case "GreenHouse":
                return TagSoundTypes.GreenHouse;
            default:
                return TagSoundTypes.None;
        }
    }
}
