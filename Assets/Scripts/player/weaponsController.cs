using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Weapons
{
    None,
    Jericho_941,
}
public struct WeaponsStruct
{ 
    public Weapons Weapon;
    public int BulletsTotalCount;
    public int BulletsInCartridge;
    public WeaponsStruct(int BulletsTotalCount,int BulletsInCartridge,Weapons Weapon)
    {
        this.BulletsTotalCount = BulletsTotalCount;
        this.BulletsInCartridge = BulletsInCartridge;
        this.Weapon = Weapon;
    }
}
public class weaponsController : MonoBehaviour
{
    //eventos
    public delegate void WeaponsEventhandler(weaponController NewWeapon);
    public event WeaponsEventhandler OnWeaponChange;
	//Variables Expuestas
	[SerializeField] Weapons _currentWeaponName=Weapons.None;
	[SerializeField] weaponController _currentWeapon;
    [SerializeField] GameObject _weaponsHolder;
    [SerializeField] float _defaultCameraFieldofViewValue=60;
    [Header("Coleccion de armas")]
    [SerializeField] weaponController[] _weaponsCollection;
    //Variables
    int _currentWeaponIndex=-1;
    //TODO en la version final eliminar la etiqueta seriealiza
	[SerializeField] List<WeaponsStruct> _weaponsAdquired=new List<WeaponsStruct>();
    /// <summary>
    /// Cunado este dejando de apuntar no reproducir la animacion de walk hasta que no termine la animacion stopAiming
    /// </summary>
    bool _isAiming;
    //Componentes
    Camera _mainCamera;
    //Clases
    collisionController _pickUps;
	GameActions _playerActions;
    Animator _animator;
    CharacterController _characterController;
    moveController _playerController;
    void Awake()
    {
        _mainCamera = Camera.main;
        _playerController = GetComponent<moveController>();
        _characterController = GetComponent<CharacterController>();
        _pickUps = GetComponent<collisionController>();
    }
    private void OnEnable()
    {
		_playerActions = new GameActions();
		_playerActions.Enable();
        _playerActions.playerActions.Shoot.performed += _ => { if (_currentWeapon != null)  _currentWeapon.shoot();};
        _playerActions.playerActions.Reload.performed += _ => { if (_currentWeapon != null)  _currentWeapon.Reload();};
        _playerActions.playerActions.Aim.performed += Aim;
        _playerActions.playerActions.Aim.canceled += Aim;
        _playerController.OnRunStateChange += OnRunningStateChange;
        _playerController.OnWalkStateChange += OnWalkStateChange;
        _playerController.OnidletateChange += OnidletateChange;
        _playerActions.playerActions.ChangeWeapon.performed += ChangeWeapon_performed;
        _pickUps.OnWeaponPickedUp += OnWeaponPickedUp;
        if (_currentWeapon != null)
        {
            _currentWeapon.OnReload += OnReload;
        }
    }
    private void OnDisable()
    {
		_playerActions.Disable();
    }

    #region Input
    private void ChangeWeapon_performed(InputAction.CallbackContext obj)
    {
        if (_animator!=null&&( _animator.GetCurrentAnimatorStateInfo(1).IsName("reloading") || _animator.GetCurrentAnimatorStateInfo(1).IsName("shoot")))
        {
            return;
        }
        switch (obj.action.activeControl.name)
        {
            case "right":
                if (_currentWeaponIndex < _weaponsAdquired.Count-1)
                {
                    changeWeapon(_weaponsAdquired[_currentWeaponIndex + 1].Weapon);
                }
                break;
            case "left":
                if (_currentWeaponIndex > 0)
                {
                    changeWeapon(_weaponsAdquired[_currentWeaponIndex-1].Weapon);
                }
                break;
            case "1":
            case "2":
            case "3":
            case "4":
                setSelectedWeaponByKeyboard(obj);
                break;
        }
    }
    private void Aim(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_currentWeapon == null || _animator.GetCurrentAnimatorStateInfo(0).IsName("startAiming") || _animator.GetCurrentAnimatorStateInfo(0).IsName("stopAiming")|| _animator.GetCurrentAnimatorStateInfo(1).IsName("reloading"))
        {
            return;
        }
        if (obj.performed&&!_animator.GetCurrentAnimatorStateInfo(0).IsName("aiming"))
        {
            _mainCamera.fieldOfView *= _currentWeapon.AimingZoomMultiplier;
            _animator.Play("startAiming", 0);
            _isAiming=true;
        }
        else if(_animator.GetCurrentAnimatorStateInfo(0).IsName("aiming"))
        {
            stopAiming();
        }
    }
    private void stopAiming()
    {
        _animator.Play("stopAiming");
        _mainCamera.fieldOfView = _defaultCameraFieldofViewValue;
        _isAiming = false;
    }
    #endregion

    #region Eventos
    private void OnidletateChange(bool State)
    {
        if (_currentWeapon==null|| isAiming())
        {
            return;
        }
        if (State && !_animator.GetCurrentAnimatorStateInfo(0).IsName("stopRunning"))
        {
            _animator.Play("idle");
        }
        else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("stopRunning"))
        {
            Invoke("startIdleAnimationAfterStopAiming", .4f);
        }
    }
    private void OnWalkStateChange(bool State)
    {
        if (!State || _currentWeapon == null||isAiming())
        {
            return;
        }
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("stopRunning"))
        {
            _animator.Play("walking");
        }
        else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("stopRunning"))
        {
            Invoke("startWalkAnimationAfterStopAiming", .4f);
        }
    }
    private void OnRunningStateChange(bool State)
    {
        if (_currentWeapon==null||isAiming())
        {
            return;
        }
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("walking") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("running"))
        {
            return;
        }
        if (State && !_animator.GetCurrentAnimatorStateInfo(0).IsName("running"))
        {
            _animator.Play("startRunning");
        }
        else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("running") && !State)
        {
            _animator.Play("stopRunning");
        }
    }
    private void OnReload()
    {
        if (_currentWeapon != null && _animator.GetCurrentAnimatorStateInfo(0).IsName("aiming"))
        {
            stopAiming();
        }
    }
    private void OnWeaponPickedUp(Weapons Weapon)
    {
        foreach(WeaponsStruct weapon in _weaponsAdquired)
        {
            if (weapon.Weapon == Weapon)
            {
                return;
            }
        }
          _weaponsAdquired.Add(_weaponsCollection[(int)Weapon-1].status);
          //equipar arma si no hay ninguna
        if (_currentWeapon == null)
        {
          _currentWeaponIndex++;
            changeWeapon(Weapon);
        }
    }
    #endregion

    #region Metodos
    
    private bool isAiming()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("startAiming") || _animator.GetCurrentAnimatorStateInfo(0).IsName("aiming") || _animator.GetCurrentAnimatorStateInfo(0).IsName("stopAiming");
    }
    void changeWeapon(Weapons NewWeapon)
    {
        //eliminr el arma actualemte equipada
        if (_weaponsHolder.transform.childCount == 1)
        {
            Destroy(_weaponsHolder.transform.GetChild(0).gameObject);
        }
        //gaurdr el estado del arma 
        if (_currentWeapon!=null)
        {
            _weaponsAdquired[_currentWeaponIndex] = _currentWeapon.status;
        }
        _currentWeaponIndex = (int)NewWeapon - 1;
        _currentWeapon= Instantiate(getWeapon(NewWeapon), _weaponsHolder.transform).GetComponent<weaponController>();
        //restaurar el estado del arma a mostrar
        _currentWeapon.status = _weaponsAdquired[_currentWeaponIndex];
        _currentWeapon.OnReload += OnReload;
        _currentWeaponName = NewWeapon;
        _animator = _currentWeapon.GetComponent<Animator>();
        OnWeaponChange(_currentWeapon);
    }
    weaponController getWeapon(Weapons CurrentWeapon)
    {
        return _weaponsCollection[(int)CurrentWeapon-1];
    }
    void startWalkAnimationAfterStopAiming()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            _animator.Play("walking");
        }
    }
    void startIdleAnimationAfterStopAiming()
    {
            _animator.Play("idle");
    }
    private void setSelectedWeaponByKeyboard(InputAction.CallbackContext obj)
    {
        int _newWeapon = int.Parse(obj.action.activeControl.name) - 1;
        if (_weaponsAdquired.Count > _newWeapon&&_newWeapon!=_currentWeaponIndex)
        {
            changeWeapon(_weaponsAdquired[_newWeapon].Weapon);
        }
    }
    #endregion

    #region Propiedades
    public weaponController CurrentWeapon { get => _currentWeapon; }
	public Weapons CurrentWeaponName { get => _currentWeaponName; }
    public bool IsAiming { get => _isAiming; }
    #endregion

}
