using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PickUpsType
{
	Weapon,
	Consumible
}
public enum Consumibles
{
    None,
    Health,
    Ammo
}
public class pickUpsController : MonoBehaviour
{
	//Variables Expuestas
	[SerializeField] PickUpsType _type;
    [SerializeField]Weapons _weaponType;
    [SerializeField]Consumibles _consumibleType;
    //Variables
    //Componentes
    //Clases
	#region Eventos

	#endregion

	#region Metodos

	#endregion

	#region Propiedades
	public PickUpsType Type { get => _type;}
    public Weapons WeaponType { get => _weaponType; set => _weaponType = value; }
    public Consumibles ConsumibleType { get => _consumibleType; set => _consumibleType = value; }
    #endregion

}
