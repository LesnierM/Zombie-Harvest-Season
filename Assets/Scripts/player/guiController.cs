using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guiController : MonoBehaviour
{
	//Variables Expuestas
	[SerializeField] Text _actionText;
	[SerializeField] Text _ammoRemainingInCartridgeText;
	[SerializeField] Text _totalAmmoRemainingText;
    [SerializeField]RawImage _weaponImage;
    //Variables
    //Componentes
    //Clases
    weaponController _currentWeapon;
    weaponsController _weaponsController;
    void Awake()
    {
        _weaponsController = GetComponent<weaponsController>();
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
    }
    private void OnDisable()
    {
        _weaponsController.OnWeaponChange -= OnWeaponChange;
        _currentWeapon.OnAmmoStatusChange -= OnAmmoStatusChange;
    }

    #region Eventos
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
