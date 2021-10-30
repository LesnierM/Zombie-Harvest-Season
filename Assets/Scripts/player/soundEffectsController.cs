using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffectsController : MonoBehaviour
{
    //Variables Expuestas
    [Header("Pasos")]
    [SerializeField] AudioClip[] _stepSoundConcrete;
	[SerializeField] AudioClip[] _waterSplash;
    //Variables
    int _index;
	//Componentes
	AudioSource _soundPlayer;
	collisionController _collisionController;
	//Clases
    void Awake()
    {
		_collisionController = GetComponent<collisionController>();
		_soundPlayer = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        _collisionController.OnWaterStateChange += OnWaterStateChange;
    }

    private void OnDisable()
    {
        _collisionController.OnWaterStateChange -= OnWaterStateChange;
    }

    #region Eventos
    private void OnWaterStateChange()
    {
        _soundPlayer.PlayOneShot(_waterSplash[0]);
    }
    #endregion

    #region Metodos
    public void OnStep()
	{
		_soundPlayer.PlayOneShot(_stepSoundConcrete[_index]);
		_index = _index == _stepSoundConcrete.Length - 1 ? 0 : _index + 1;
	}
	#endregion

	#region Propiedades

	#endregion

}
