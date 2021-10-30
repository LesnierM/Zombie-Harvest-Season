using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class actionableObjects : MonoBehaviour
{
    //Variables Expuestas
    [SerializeField] float _speed;
    [SerializeField] float _maxDistance;
    [SerializeField] DoorOpenDirections _direction;
    [SerializeField] ActionTypes _type;
    [SerializeField] SoundType _soundType;
    [SerializeField] Transform _ObjectToManipulate;
    [Header("Sonidos")]
    [SerializeField] AudioClip _openDoorSound;
    [SerializeField] AudioClip _closeDoorSound;
    [SerializeField] float _closeSoundDelay;
    /// <summary>
    /// Indica que el objeto solo se manipulara en un sentido;
    /// </summary>
    [SerializeField] bool _onDirection;
    //Variables
    ObjectStates _Action = ObjectStates.Idle;
    ObjectStates _currentState=ObjectStates.Close;
    ObjectStates _lastActionPerformed;
    float _distanceMoved;
    //Componentes
    AudioSource _soundPlayer;
    //Clases
    private void Awake()
    {
        _soundPlayer = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if (_Action == ObjectStates.Idle)
        {
            return;
        }
        if (_distanceMoved < _maxDistance)
        {
            float _velocity = _speed;
            if (_Action == ObjectStates.Open)
            {
                _velocity *= _direction == DoorOpenDirections.Positive ? 1 : -1;

            }
            else if (_Action == ObjectStates.Close&&!_onDirection)
            {
                _velocity *= _direction == DoorOpenDirections.Positive ? -1 : 1;

            }
            _distanceMoved += Mathf.Abs(_velocity *= Time.deltaTime);
            switch (_type)
            {
                case ActionTypes.YRotation:
                    transform.Rotate(Vector3.up, _velocity, Space.Self);
                    break;
                case ActionTypes.YTranslation:
                    _ObjectToManipulate.transform.Translate(Vector2.up * _velocity, Space.Self);
                    break;
                case ActionTypes.XRotation:
                    _ObjectToManipulate.Rotate(_ObjectToManipulate.right, _velocity , Space.World);
                    break;
                case ActionTypes.ZRotation:
                    _ObjectToManipulate.Rotate(_ObjectToManipulate.forward, _velocity , Space.World);
                    break;
            }
        }
        else
        {
            _Action = ObjectStates.Idle;
            _currentState = _lastActionPerformed;
            _distanceMoved = 0;
        }
    }

    #region Eventos

    #endregion

    #region Metodos
    public void startAction()
    {
        playOpenSound();
        _currentState = ObjectStates.Moving;
        _lastActionPerformed = ObjectStates.Open;
        _Action = ObjectStates.Open;
    }
    public void finishAction()
    {
        playCloseSound();
        _currentState = ObjectStates.Moving;
        _lastActionPerformed = ObjectStates.Close;
        _Action = ObjectStates.Close;
    }
    void playOpenSound()
    {
        switch (_soundType)
        {
            case SoundType.Door:
                _soundPlayer.PlayOneShot(_openDoorSound);
                break;
            case SoundType.Gate:
                break;
            case SoundType.AudioSource:
                _soundPlayer.Play();
                break;
        }
    }
    void playCloseSound()
    {
        switch (_soundType)
        {
            case SoundType.Door:
                _soundPlayer.clip = _closeDoorSound;
                break;
            case SoundType.Gate:
                break;
            case SoundType.AudioSource:
                break;
        }
       Invoke("playSound",_closeSoundDelay);
    }
    void playSound()
    {
        _soundPlayer.Play();
    }
    #endregion

    #region Propiedades
    public ObjectStates CurrentState { get => _currentState; set => _currentState = value; }
    #endregion

}
