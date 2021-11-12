using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionController : MonoBehaviour
{
    enum SphereCheckPositions
    {
        Top,
        Middle,
        Bottom
    }
   
    public delegate void NoParameters();
    public delegate void WaterParameters(WaterLevels WaterLevel);

    public delegate void BoolParameter(bool isinWater);
    public delegate void GroundDataParameterEventHandler(GroundData GroundData);
    public delegate void onPickedUpWeaponEventHandler(Weapons Weapon);
    public delegate void ActionableObjects(global::ActionableObjects ActionableObject, ActionsTypes Action);
    public delegate void onPickedUpConsumibleEventHandler(Consumibles Consumible);
    public event onPickedUpWeaponEventHandler OnWeaponPickedUp;
    public event ActionableObjects onActionableObjectStay;
    public event NoParameters onActionableObjectLeave;
    public event WaterParameters OnWaterStateChange;
    public event GroundDataParameterEventHandler OnGroundedStateChange;
    //Variables Expuestas
    [SerializeField] float _gatesRotationSpeed;
    [Header("Capas")]
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] LayerMask _waterLayer;
    [Header("Chequeo de piso")]
    [SerializeField] float _sphereRadious=0.3f;
    [SerializeField] float _groundCheckOffset=-0.2f;
    [Header("parametros de fuerza y rotacion aplicada,")]
    [SerializeField] float _forceMultiplierOAffectableObjects;
    [SerializeField] float _forceAppliedToBigobjects;
    [Header("")]
    [SerializeField] float _inWaterShpereOffset=.12f;
    [SerializeField] float _halfInWaterShpereOffset=.12f;
    //Variables
    private bool _executeAction;


    GroundData _groundedData;

    Collider[] _colliders=new Collider[0];

    WaterLevels _currentWaterLevel;
    //Componentes
    CharacterController _characterController;
    //Clases
    GameActions _playerActions;
    guiController _guiController;
    moveController _moveController;
    private void Awake()
    {
        _guiController = GetComponent<guiController>();
        _moveController = GetComponent<moveController>();
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        #region Ground Check
        //la cindicion es para cuando se salta pegado a un objeto con tag ground poder terminar el salto
        if (_characterController.velocity.y <= 0)
        {
            checkGroundedData();
        }
        #endregion

        #region Water Check
        WaterLevels _isWaterTemp = _currentWaterLevel;
        _currentWaterLevel = checkLayer(_waterLayer,SphereCheckPositions.Top).Colliosined?WaterLevels.InWater:WaterLevels.None;
        if (_currentWaterLevel == WaterLevels.None)
        {
        _currentWaterLevel = checkLayer(_waterLayer,SphereCheckPositions.Middle).Colliosined?WaterLevels.HalfInWater:WaterLevels.None;
        }
        if (_currentWaterLevel == WaterLevels.None)
        {
        _currentWaterLevel = checkLayer(_waterLayer,SphereCheckPositions.Bottom ).Colliosined?WaterLevels.OnWater:WaterLevels.None;
        }
        if (_isWaterTemp != _currentWaterLevel)
        {
            OnWaterStateChange(_currentWaterLevel);
        }
        #endregion
    }
   
    private void OnEnable()
    {
        if (_playerActions == null)
        {
            _playerActions = new GameActions();
        }
        _playerActions.Enable();
        _playerActions.playerActions.Action.performed += _ => _executeAction = true;
        _playerActions.playerActions.Action.canceled += _ => _executeAction = false;
    }
    private void OnDisable()
    {
        _playerActions.Disable();
        _playerActions.playerActions.Action.performed -= _ => _executeAction = true;
        _playerActions.playerActions.Action.canceled -= _ => _executeAction = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Item")
        {
            pickUpsController _puc =other.GetComponent<pickUpsController>();
            string _pickedUpObjectname=default;
            switch (_puc.Type)
            {
                case PickUpsType.Weapon:
                    _pickedUpObjectname ="una "+ _puc.WeaponType.ToString();
                    OnWeaponPickedUp(_puc.WeaponType);
                    break;
                case PickUpsType.Consumible:
                    _pickedUpObjectname = _puc.ConsumibleType.ToString();
                    break;
            }
            _guiController.showInfotext("Has recojido " + _pickedUpObjectname,10);
            Destroy(_puc.gameObject);
        }
       
    }
    private void OnTriggerStay(Collider other)
    {
        #region Objetos Accionables
        if (other.gameObject.layer == 14)
        {
            float dot = Vector3.Dot(other.transform.forward, transform.forward);
            if (other.tag == "Well" || other.tag == "Box" || (other.tag == "Doors" && ((dot < -.9f && other.name == "entry") || (dot > .9f && other.name == "exit"))))
            {
                actionableObjects _controller = !other.TryGetComponent<actionableObjects>(out _controller) ? other.transform.parent.GetComponent<actionableObjects>() : _controller;
                if (_controller.CurrentState != ObjectStates.Moving)
                {
                    if (_executeAction)
                    {
                        _executeAction = false;
                        if (_controller.CurrentState == ObjectStates.Close)
                        {
                            _controller.startAction();
                        }
                        else
                        {
                            _controller.finishAction();
                        }
                        onActionableObjectLeave();
                    }
                    else if (onActionableObjectStay != null)
                    {
                        global::ActionableObjects _object;
                        if (other.tag == "Doors" || other.tag == "Box")
                        {
                            _object = global::ActionableObjects.Openable;
                        }
                        else
                        {
                            _object = global::ActionableObjects.Manipulable;

                        }
                        onActionableObjectStay(_object, converter.getActionFromDoorState(_controller.CurrentState));
                    }
                }
                else
                {
                    onActionableObjectLeave();
                }
            }
            else
            {
                onActionableObjectLeave();
            }
        }
        #endregion

        #region Rejas
        else if (other.tag == "Gates")
        {
            float _velocity = _gatesRotationSpeed;
            if (other.name == "entry")
            {
                _velocity *= -1;
            }
            else
            {
                _velocity *= 1;
            }
            other.transform.parent.Rotate(Vector2.up, _velocity * Time.deltaTime, Space.Self);
        }
            #endregion
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer==14)
        {
            onActionableObjectLeave();
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //objetos que son modificados por fuerza
        if (hit.gameObject.layer == 15)
        {
            Vector3 dif = (hit.transform.position - transform.position).normalized;
            Vector3 _direction = new Vector3(dif.x,.01f, dif.z);
            Debug.Log(hit.controller.velocity.magnitude);
            if (hit.rigidbody.velocity.magnitude == 0)
            {
                hit.transform.GetComponent<AudioSource>().Play();
            }
            if (hit.gameObject.tag == "TinyObjects")
            {
                hit.rigidbody.AddForceAtPosition(_direction * hit.controller.velocity.magnitude * _forceMultiplierOAffectableObjects, hit.point);
            }
            else if (hit.gameObject.tag == "BigObjects")
            {
                hit.rigidbody.AddForceAtPosition(_direction  *_forceAppliedToBigobjects *_forceMultiplierOAffectableObjects, hit.point);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + (Vector3.down * (GetComponent<CharacterController>().height / 2 + _groundCheckOffset)), _sphereRadious);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position+Vector3.up*_halfInWaterShpereOffset, .05f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Camera.main.transform.position + Vector3.up * _inWaterShpereOffset, .05f);
    }

    #region Eventos

    #endregion

    #region Metodos
    public void checkGroundedData()
    {
        bool _tempGrounded = _groundedData.Colliosined;
        _groundedData = checkLayer(_groundLayer,SphereCheckPositions.Bottom);
        if (_tempGrounded != _groundedData.Colliosined)
        {
            OnGroundedStateChange(_groundedData);
        }
    }
    GroundData checkLayer(LayerMask Layer,SphereCheckPositions Position)
    {
        GroundData _groundData=new GroundData(false,"");
        Vector3 _spherePosition=default;
        switch (Position)
        {
            case SphereCheckPositions.Top:
                _spherePosition = Camera.main.transform.position + Vector3.up * _inWaterShpereOffset;
                break;
            case SphereCheckPositions.Middle:
                _spherePosition = transform.position + Vector3.up * _halfInWaterShpereOffset;
                break;
            case SphereCheckPositions.Bottom:
                _spherePosition = transform.position + (Vector3.down * ((_characterController.height / 2) + _groundCheckOffset));
                break;
        }
        //return Physics.OverlapSphere(transform.position + (Vector3.down * ((_characterController.height / 2) + _groundCheckOffset)), _sphereRadious, Layer);
        _colliders =Physics.OverlapSphere(_spherePosition,_sphereRadious,Layer);
        if (_colliders.Length != 0)
        {
            _groundData = new GroundData(true,_colliders[0].gameObject.tag);
        }
        return _groundData;
    }

    #endregion

    #region Propiedades
    public GroundData GroundedData { get => _groundedData; }
    public WaterLevels CurrentWaterLevel { get => _currentWaterLevel; }
    #endregion
}
