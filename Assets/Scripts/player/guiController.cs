using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guiController : MonoBehaviour
{
	//Variables Expuestas
	[SerializeField] Text _actionText;
	[SerializeField] Text _ammoText;
    //Variables
    //Componentes
    //Clases
    weaponController _currentWeapon;
    weaponsController _weaponsController;
    void Awake()
    {
        _weaponsController = GetComponent<weaponsController>();
    }
    void Update()
    {
        
    }
    private void OnEnable()
    {
        _weaponsController.OnWeaponChange += OnWeaponChange;
        if (_currentWeapon != null)
        {
            _currentWeapon.OnAmmoStatusChange += OnAmmoStatusChange;
        }
    }

    #region Eventos
    private void OnWeaponChange(weaponController NewWeapon)
    {
        _currentWeapon = NewWeapon;
        _currentWeapon.OnAmmoStatusChange += OnAmmoStatusChange;
        OnAmmoStatusChange(_currentWeapon.CartridgeCaseRemaningBullets, _currentWeapon.BulletsCount);
    }
    private void OnAmmoStatusChange(int CartridgeRemainigBullets, int BulletsCount)
    {
        string _info = CartridgeRemainigBullets + "/" + BulletsCount;
        _ammoText.text = _info;
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
