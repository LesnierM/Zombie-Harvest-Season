using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffectsController : MonoBehaviour
{
	//Variables Expuestas
	[Header("Pasos")]
	[SerializeField] AudioClip[] _stepSoundConcrete;
	//Variables
	int _index;
	//Componentes
	AudioSource _soundPlayer;
	//Clases
    void Awake()
    {
		_soundPlayer = GetComponent<AudioSource>();
    }

    public void OnStep()
    {
		_soundPlayer.PlayOneShot(_stepSoundConcrete[_index]);
		_index = _index == _stepSoundConcrete.Length-1 ? 0 : _index+1;
    }
    void Update()
    {
        
    }		
				
	#region Eventos
	
	#endregion
			
	#region Metodos
	
	#endregion
				
	#region Propiedades
	
	#endregion
	    
}
