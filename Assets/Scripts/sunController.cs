using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunController : MonoBehaviour
{
	public delegate void OnSunStateChangeEventHandler(SunStates State);
	public event OnSunStateChangeEventHandler OnSunStateChange;
	//Variables Expuestas
	[SerializeField] float _dayLightSpeed;
	[SerializeField] float _nightLightSpeed;
	//Variables
	SunStates _state;
	//Componentes
	//Clases

	void FixedUpdate()
	{
		float _rotacion = transform.rotation.eulerAngles.x;
		float _velocity = _rotacion > 190 && _rotacion < 360 ? _nightLightSpeed : _dayLightSpeed;
		SunStates _tempState=_state;
		_state = _velocity == 1 ? SunStates.Day : SunStates.Night;
        if (_tempState != _state)
        {
			OnSunStateChange(_state);
        }
		transform.Rotate(Vector3.right, _velocity * Time.deltaTime);
	}

	#region Eventos

	#endregion

	#region Metodos

	#endregion

	#region Propiedades
	public SunStates State { get => _state; }
	#endregion

}
