using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffectsController : MonoBehaviour
{
    [SerializeField] AudioClip[] _jumpSounds;
    [SerializeField] AudioClip[] _dirtStepSoundEffects;
	[SerializeField] AudioClip[] _waterSplash;
    //Variables
    List<int> _playedStepSounds = new List<int>();
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
        _moveController.OnPlayerJump += OnPlayerJump;
        _moveController.OnGroundedStateChange += OnGroundedStateChange;
        _collisionController.OnWaterStateChange += OnWaterStateChange;
    }

   

    private void OnDisable()
    {
        _moveController.OnGroundedStateChange -= OnGroundedStateChange;
        _collisionController.OnWaterStateChange -= OnWaterStateChange;
    }

    #region Eventos
    private void OnPlayerJump()
    {
        _soundPlayer.PlayOneShot(_jumpSounds[Random.Range(0, _jumpSounds.Length)]);
    }
    private void OnGroundedStateChange(moveController.GroundData GroundData)
    {
        if (GroundData.Colliosined)
        {

        }
    }
    private void OnWaterStateChange()
    {
        _soundPlayer.PlayOneShot(_waterSplash[0]);
    }
    #endregion

    #region Metodos
    public void OnStep(GroundStepsSoundTypes Type)
    {
        AudioClip[] _stepSounds;
        switch (Type)
        {
            case GroundStepsSoundTypes.None:
                break;
            case GroundStepsSoundTypes.Dirt:
                _stepSounds = _dirtStepSoundEffects;
                break;
        }
        int _nextSound;
        do
        {
            _nextSound = Random.Range(0, _dirtStepSoundEffects.Length);
        } while (_playedStepSounds.Contains(_nextSound));
        _soundPlayer.PlayOneShot(_dirtStepSoundEffects[_nextSound]);

        if (_playedStepSounds.Count == 10)
        {
            _playedStepSounds[0] = _nextSound;
        }
        else
        {
            _playedStepSounds.Add(_nextSound);
        }
    }
    #endregion

    #region Propiedades

    #endregion

}
