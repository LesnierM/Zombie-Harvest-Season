using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffectsController : MonoBehaviour
{
    [SerializeField] int _diferentSoundsConsecutiveCount;
    [SerializeField] AudioClip[] _jumpSounds;
    [Header("Pasos")]
    [SerializeField] AudioClip[] _dirtStepSoundEffects;
    [SerializeField] AudioClip[] _woodStepSoundEffect;
	[SerializeField] AudioClip[] _onWaterStepSoundEffects;
	[SerializeField] AudioClip[] _inWaterStepSoundEffects;
    [SerializeField] AudioClip _inWaterSoundEffect;
    [Header("Impacto de balas")]
    [SerializeField] AudioClip _onMetalTankBulletImpactSound;
    [SerializeField] AudioClip _OnCarsLikeImpactSound;
    [SerializeField] AudioClip _OnMetalGatesImpactSound;
    [SerializeField] AudioClip _OnSmallTractorsLikeImpactSound;
    [SerializeField] AudioClip _OnMetalBarrelsLikeImpactSound;
    [SerializeField] AudioClip _OnBullDozerLikeImpactSound;
    [Header("Pitch de impacto")]
    [SerializeField] float _minPitch;
    [SerializeField] float _maxPitch;
    //Variables
    List<int> _playedStepSounds = new List<int>();
    int _currentStepSoundIndex;
	//Componentes
	AudioSource _soundPlayer;
    AudioSource _stepsSoundPlayer;
	collisionController _collisionController;
    //Clases
    moveController _moveController;
    void Awake()
    {
		_collisionController = GetComponent<collisionController>();
        AudioSource[] players= GetComponents<AudioSource>();
        _soundPlayer = players[0];
        _stepsSoundPlayer = players[1];
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
            randomPitch(_soundPlayer);
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
    public void OnBulletImpact(GameObject HittedObject)
    {
        AudioSource _audioSource;
        if (!HittedObject.TryGetComponent<AudioSource>(out _audioSource))
        {
            _audioSource = HittedObject.AddComponent<AudioSource>();
            //configuracion del audiosource
            _audioSource.spatialBlend = 1;
            _audioSource.rolloffMode = AudioRolloffMode.Linear;
            _audioSource.minDistance = 5.35f;
            _audioSource.maxDistance = 49.4f;
        }
        randomPitch(_audioSource);
        AudioClip _sound = default;
        switch (converter.getGroundStepSoundTypesFromString(HittedObject.tag))
        {
            case TagSoundTypes.None:
                break;
            case TagSoundTypes.Dirt:
                break;
            case TagSoundTypes.Wood:
                break;
            case TagSoundTypes.Water:
                break;
            case TagSoundTypes.MetalOilTank:
                _sound = _onMetalTankBulletImpactSound;
                break;
            case TagSoundTypes.CarsLike:
                _sound = _OnCarsLikeImpactSound;
                break;
            case TagSoundTypes.MetalGates:
                _sound = _OnMetalGatesImpactSound;
                break;  
            case TagSoundTypes.SmallTractorsLike:
                _sound = _OnSmallTractorsLikeImpactSound;
                break;
            case TagSoundTypes.MetalBarrels:
                _sound = _OnMetalBarrelsLikeImpactSound;
                break; 
            case TagSoundTypes.BullDozer:
                _sound = _OnBullDozerLikeImpactSound;
                break;
        }
        _audioSource.PlayOneShot(_sound);
    }

    private void randomPitch(AudioSource _audioSource)
    {
        _audioSource.pitch = 1 + Random.Range(_minPitch, _maxPitch);
    }

    public void OnStep()
    {
        AudioClip[] _stepSounds = default;
        randomPitch(_stepsSoundPlayer);
        switch (_collisionController.CurrentWaterLevel)
        {
            case WaterLevels.None:
                switch (converter.getGroundStepSoundTypesFromString(_collisionController.GroundedData.Tag))
                {
                    case TagSoundTypes.Dirt:
                        _stepSounds = _dirtStepSoundEffects;
                        break;
                    case TagSoundTypes.Wood:
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
        if (_stepSounds == null)
        {
            return;
        }
        int _nextSound;
        do
        {
            _nextSound = Random.Range(0, _stepSounds.Length);
        } while (_playedStepSounds.Contains(_nextSound));
        _stepsSoundPlayer.PlayOneShot(_stepSounds[_nextSound]);
        
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
