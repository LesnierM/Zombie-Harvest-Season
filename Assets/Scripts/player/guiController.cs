using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guiController : MonoBehaviour
{
	//Variables Expuestas
    [Header("Referencias")]
	[SerializeField] Text _actionText;
	[SerializeField] Text _ammoRemainingInCartridgeText;
	[SerializeField] Text _totalAmmoRemainingText;
    [SerializeField]RawImage _weaponImage;
    [SerializeField] GameObject _actionButtonsSpawnPoints;
    [Header("Botones")]
    [SerializeField] GameObject _keyboardEButton;
    [SerializeField] GameObject _gamepadAButton;
    //Variables
    //Componentes
    //Clases
    weaponController _currentWeapon;
    weaponsController _weaponsController;
    collisionController _collisionController;
    gameManager _gameManager;
    void Awake()
    {
        _gameManager = GameObject.FindObjectOfType<gameManager>();
        _weaponsController = GetComponent<weaponsController>();
        _collisionController = GetComponent<collisionController>();
    }
    private void OnEnable()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.OnAmmoStatusChange += OnAmmoStatusChange;
        }
        if (_weaponsController != null)
        {
            _weaponsController.OnWeaponChange += OnWeaponChange;
        }
        _collisionController.onActionableObjectStay += onActionableObjectStay;
        _collisionController.onActionableObjectLeave += onActionableObjectLeave;
    }

    private void OnDisable()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.OnAmmoStatusChange -= OnAmmoStatusChange;
        }
        if (_weaponsController != null)
        {
            _weaponsController.OnWeaponChange -= OnWeaponChange;
        }
        _collisionController.onActionableObjectStay -= onActionableObjectStay;
        _collisionController.onActionableObjectLeave -= onActionableObjectLeave;
    }

    #region Eventos
    private void onActionableObjectLeave()
    {
        hideInfoText();
        if (_actionButtonsSpawnPoints.transform.childCount > 0)
        {
            Destroy(_actionButtonsSpawnPoints.transform.GetChild(0).gameObject);
        }
    }
    private void onActionableObjectStay(ActionableObjects ActionableObject, ActionsTypes Action)
    {
        switch (ActionableObject)
        {
            case ActionableObjects.Openable:
                switch (converter.getDoorActionFromActionType(Action))
                {
                    case ObjectStates.Idle:
                        break;
                    case ObjectStates.Close:
                        showInfotext("Cerrar", 0);
                        break;
                    case ObjectStates.Open:
                        showInfotext("Abrir", 0);
                        break;
                    case ObjectStates.Moving:
                        break;
                }
                break;
            case ActionableObjects.Manipulable:
                showInfotext("Manipular", 0);
                break;
            case ActionableObjects.None:
                break;
        }
        GameObject _button = null;
        switch (gameManager._lastInputDeviceUsed)
        {
            case ControlType.Keyboard:
                _button = _keyboardEButton;
                break;
            case ControlType.Gamepad:
                _button = _gamepadAButton;
                break;
        }
        if (_actionButtonsSpawnPoints.transform.childCount != 0)
        {
            if (!_actionButtonsSpawnPoints.transform.GetChild(0).name.Contains(_button.name))
            {
                Destroy(_actionButtonsSpawnPoints.transform.GetChild(0).gameObject);
                Instantiate(_button, _actionButtonsSpawnPoints.transform.position, Quaternion.LookRotation(Camera.main.transform.forward), _actionButtonsSpawnPoints.transform);
            }
        }
        else
        {
            Instantiate(_button, _actionButtonsSpawnPoints.transform.position, Quaternion.LookRotation(Camera.main.transform.forward), _actionButtonsSpawnPoints.transform);
        }
    }
    void OnWeaponChange(weaponController NewWeapon)
    {
        if (_currentWeapon != null)
        {
        _currentWeapon.OnAmmoStatusChange -= OnAmmoStatusChange;
        }
        _currentWeapon = NewWeapon;
        _currentWeapon.OnAmmoStatusChange += OnAmmoStatusChange;
        _weaponImage.texture = _currentWeapon.UiImage;
        if (!_weaponImage.enabled)
        {
            _weaponImage.enabled = true;
        }
        OnAmmoStatusChange(_currentWeapon.CartridgeCaseRemaningBullets, _currentWeapon.BulletsCount);
    }
    private void OnAmmoStatusChange(int CartridgeRemainigBullets, int BulletsCount)
    {
        _ammoRemainingInCartridgeText.text = CartridgeRemainigBullets.ToString();
        _totalAmmoRemainingText.text = BulletsCount.ToString();
    }
    #endregion

    #region Metodos
   
    public void showInfotext(string Text,float HideDelay)
    {
        _actionText.text = Text;
        _actionText.enabled = true;
        //0=ocultado manual
        if (HideDelay != 0)
        {
            Invoke("hideInfoText", HideDelay);
        }
    }
	public void hideInfoText()
    {
        _actionText.enabled = false;
    }
    #endregion

    #region Propiedades

    #endregion

}
