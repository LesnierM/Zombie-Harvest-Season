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
    [Header("Barra de oxigeno")]
    [SerializeField] float _oxigenDuration;
    [SerializeField] Transform _oxigenBar;
    [SerializeField] float _transitionDuration;
    //Variables
    float _remaingOxigenTime;
    float _diveStartTime;
    bool _isDiving;
    CanvasGroup _oxigenBarCanvasGroup;
    //Componentes
    //Clases
    weaponController _currentWeapon;
    weaponsController _weaponsController;
    collisionController _collisionController;
    gameManager _gameManager;
    void Awake()
    {
        _oxigenBarCanvasGroup = _oxigenBar.parent.GetComponent<CanvasGroup>();
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
        _collisionController.OnWaterStateChange += OnWaterStateChange;
        _collisionController.onInteractableActions += onInteractableActions;
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
        _collisionController.onInteractableActions -= onInteractableActions;
    }
    private void Update()
    {
        #region Barra de oxigeno
        if (_isDiving)
        {
            float _value = (Time.time - _diveStartTime) / _oxigenDuration;
            _oxigenBar.localScale = new Vector3(Mathf.Clamp(1 - _value, 0, 1), 1, 1);
            if (_oxigenBarCanvasGroup.alpha < 1)
            {
                _oxigenBarCanvasGroup.alpha += _transitionDuration * Time.deltaTime;
            }
        }
        else if (_oxigenBarCanvasGroup.alpha > 0)
        {
            _oxigenBarCanvasGroup.alpha -= _transitionDuration * Time.deltaTime;
        }
        if (!_isDiving&&_oxigenBar.localScale.x < 1)
        {
            float _value = (Time.time - _diveStartTime) / _oxigenDuration;
            _oxigenBar.localScale = new Vector3(Mathf.Clamp(_oxigenBar.localScale.x + _value, 0, 1), 1, 1);

        }

        #endregion
    }

    #region Eventos
    private void onInteractableActions(string ActionText)
    {
        showInfotext(ActionText);
        showInputActionButton();
    }
    private void OnWaterStateChange(WaterLevels WaterLevel)
    {
        if (WaterLevel == WaterLevels.InWater)
        {
            _diveStartTime = Time.time;
            _isDiving = true;
        }
        else if(WaterLevel==WaterLevels.HalfInWater)
        {
            _diveStartTime = Time.time;
            _isDiving = false;
        }
    }
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
                        showInfotext("Cerrar");
                        break;
                    case ObjectStates.Open:
                        showInfotext("Abrir");
                        break;
                    case ObjectStates.Moving:
                        break;
                }
                break;
            case ActionableObjects.Manipulable:
                showInfotext("Manipular");
                break;
            case ActionableObjects.None:
                break;
        }
        showInputActionButton();
    }
    private void showInputActionButton()
    {
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
   
    public void showInfotext(string Text,float HideDelay=0)
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
