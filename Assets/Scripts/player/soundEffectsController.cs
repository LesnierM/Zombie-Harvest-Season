using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffectsController : MonoBehaviour
{
	[SerializeField] AudioClip[] _waterSplash;
    //Variables
	//Componentes
	AudioSource _soundPlayer;
	collisionController _collisionController;
    //Clases
    moveController _moveController;
    void Awake()
    {
		_collisionController = GetComponent<collisionController>();
		_soundPlayer = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        if (_moveController == null)
        {
            _moveController = GameObject.FindObjectOfType<moveController>();
        }
        _moveController.OnGroundedStateChange += OnGroundedStateChange;
        _collisionController.OnWaterStateChange += OnWaterStateChange;
    }

    private void OnDisable()
    {
        _moveController.OnGroundedStateChange -= OnGroundedStateChange;
        _collisionController.OnWaterStateChange -= OnWaterStateChange;
    }

    #region Eventos
    private void OnGroundedStateChange(bool State)
    {
        if (State)
        {
            AkSoundEngine.PostEvent("Landing", gameObject);
        }
    }
    private void OnWaterStateChange()
    {
        _soundPlayer.PlayOneShot(_waterSplash[0]);
    }
    #endregion

    #region Metodos
    public void OnStep()
	{
        AkSoundEngine.PostEvent("Footstep", gameObject);
    }
	#endregion

	#region Propiedades

	#endregion

}
