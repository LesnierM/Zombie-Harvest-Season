using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffectsController : MonoBehaviour
{
    [SerializeField] AudioClip[] _jumpSounds;
    [SerializeField] AudioClip[] _dirtStepSoundEffects;
    [SerializeField] AudioClip[] _woodStepSoundEffect;
	[SerializeField] AudioClip[] _waterSplash;
    //Variables
    List<int> _playedStepSounds = new List<int>();
	//Componentes
	AudioSource _soundPlayer;
    AudioSource _stepsPlayer;
	collisionController _collisionController;
    //Clases
    moveController _moveController;
    void Awake()
    {
		_collisionController = GetComponent<collisionController>();
        AudioSource[] players= GetComponents<AudioSource>();
        _soundPlayer = players[0];
        _stepsPlayer = players[1];
        
    }
    private void OnEnable()
    {
        if (_moveController == null)
        {
            _moveController = GameObject.FindObjectOfType<moveController>();
        }
        _moveController.OnPlayerJump += OnPlayerJump;
        _collisionController.OnGroundedStateChange += OnGroundedStateChange;
        _collisionController.OnWaterStateChange += OnWaterStateChange;
    }

    private void OnDisable()
    {
        _collisionController.OnGroundedStateChange -= OnGroundedStateChange;
        _collisionController.OnWaterStateChange -= OnWaterStateChange;
    }

    #region Eventos
    private void OnPlayerJump()
    {
        _soundPlayer.PlayOneShot(_jumpSounds[Random.Range(0, _jumpSounds.Length)]);
    }
    private void OnGroundedStateChange(GroundData GroundData)
    {
        if (GroundData.Colliosined)
        {
            OnStep();
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
       AudioClip[] _stepSounds=default;
        switch (converter.getGroundStepSoundTypesFromString(_collisionController.GroundedData.Tag))
        {
            case GroundStepsSoundTypes.None:
                return;
            case GroundStepsSoundTypes.Dirt:
                _stepSounds = _dirtStepSoundEffects;
                break;
            case GroundStepsSoundTypes.Wood:
                _stepSounds = _woodStepSoundEffect;
                break;
        }
        int _nextSound;
        do
        {
            _nextSound = Random.Range(0, _stepSounds.Length);
        } while (_playedStepSounds.Contains(_nextSound));
        _stepsPlayer.PlayOneShot(_stepSounds[_nextSound]);

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
