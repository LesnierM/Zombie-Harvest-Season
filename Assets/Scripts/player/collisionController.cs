using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionController : MonoBehaviour
{
    public delegate void onPickedUpWeaponEventHandler(Weapons Weapon);
    public delegate void onPickedUpConsumibleEventHandler(Consumibles Consumible);
    public event onPickedUpWeaponEventHandler OnWeaponPickedUp;
    //Variables Expuestas
    //Variables
    //Componentes
    //Clases
    GameActions _playerACtion;
    guiController _guiController;
    private void Awake()
    {
        _guiController = GetComponent <guiController>();
    }
    private void OnEnable()
    {
        _playerACtion = new GameActions();
        _playerACtion.Enable();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Item")
        {
            pickUpsController _puc =other.GetComponent<pickUpsController>();
            string _pickedUpObjectname=default;
            switch (_puc.Type)
            {
                case PickUpsType.Weapon:
                    _pickedUpObjectname ="una "+ _puc.WeaponType.ToString();
                    OnWeaponPickedUp(_puc.WeaponType);
                    break;
                case PickUpsType.Consumible:
                    _pickedUpObjectname = _puc.ConsumibleType.ToString();
                    break;
            }
            _guiController.showInfotext("Has recojido " + _pickedUpObjectname,10);
            Destroy(_puc.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
      
    }

    #region Eventos

    #endregion

    #region Metodos

    #endregion

    #region Propiedades

    #endregion

}
