using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffectsController : MonoBehaviour
{
    [SerializeField] int _diferentSoundsConsecutiveCount;
    [SerializeField] AudioClip[] _jumpSounds;
    [SerializeField] AudioClip[] _dirtStepSoundEffects;
    [SerializeField] AudioClip[] _woodStepSoundEffect;
	[SerializeField] AudioClip[] _onWaterStepSoundEffects;
	[SerializeField] AudioClip[] _inWaterStepSoundEffects;
    [SerializeField] AudioClip _inWaterSoundEffect;
    //Variables
    List<int> _playedStepSounds = new List<int>();
    int _currentStepSoundIndex;
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
        if (_collisionController.CurrentWaterLevel != WaterLevels.InWater)
        {
            _soundPlayer.PlayOneShot(_jumpSounds[Random.Range(0, _jumpSounds.Length)]);
        }
    }
    private void OnGroundedStateChange(GroundData GroundData)
    {
        if (GroundData.Colliosined)
        {
            OnStep();
        }
    }
    private void OnWaterStateChange(WaterLevels WaterLevel)
    {
        if (WaterLevel == WaterLevels.InWater)
        {
            _soundPlayer.loop = true;
            _soundPlayer.clip = _inWaterSoundEffect;
            _soundPlayer.Play();
        }
        else
        {
            _soundPlayer.loop = false;
            _soundPlayer.Stop();
        }
    }
    #endregion

    #region Metodos
    public void OnStep()
    {
        AudioClip[] _stepSounds = default;
        switch (_collisionController.CurrentWaterLevel)
        {
            case WaterLevels.None:
                switch (converter.getGroundStepSoundTypesFromString(_collisionController.GroundedData.Tag))
                {
                    case GroundStepsSoundTypes.Dirt:
                        _stepSounds = _dirtStepSoundEffects;
                        break;
                    case GroundStepsSoundTypes.Wood:
                        _stepSounds = _woodStepSoundEffect;
                        break;
                }
                break;
            case WaterLevels.OnWater:
                _stepSounds = _onWaterStepSoundEffects;
                break;
            case WaterLevels.HalfInWater:
            case WaterLevels.InWater:
                _stepSounds = _inWaterStepSoundEffects;
                break;
        }

        int _nextSound;
        do
        {
            _nextSound = Random.Range(0, _stepSounds.Length);
        } while (_playedStepSounds.Contains(_nextSound));
        _stepsPlayer.PlayOneShot(_stepSounds[_nextSound]);
        
        if (_playedStepSounds.Count ==_diferentSoundsConsecutiveCount)
        {
            _playedStepSounds[_currentStepSoundIndex] = _nextSound;
            _currentStepSoundIndex++;
            if (_currentStepSoundIndex == _diferentSoundsConsecutiveCount)
            {
                _currentStepSoundIndex = 0;
            }
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
