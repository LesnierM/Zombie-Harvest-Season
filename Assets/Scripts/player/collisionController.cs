using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionController : MonoBehaviour
{
    public delegate void NoParameters();
    public delegate void BoolParameter(bool isinWater);
    public delegate void onPickedUpWeaponEventHandler(Weapons Weapon);
    public delegate void ActionableObjects(global::ActionableObjects ActionableObject, ActionsTypes Action);
    public delegate void onPickedUpConsumibleEventHandler(Consumibles Consumible);
    public event onPickedUpWeaponEventHandler OnWeaponPickedUp;
    public event ActionableObjects onActionableObjectStay;
    public event NoParameters onActionableObjectLeave;
    public event NoParameters OnWaterStateChange;
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
    //Variables
    private bool _executeAction;
    bool _isInWater;
    //Componentes
    //Clases
    GameActions _playerActions;
    guiController _guiController;
    CharacterController _characterController;
    moveController _moveController;
    private void Awake()
    {
        _guiController = GetComponent<guiController>();
        _moveController = GetComponent<moveController>();
        _characterController = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        #region Water Check
        bool _isWaterTemp = _isInWater;
        _isInWater = checkLayer(_waterLayer);
        if (OnWaterStateChange != null&&_isWaterTemp!=_isInWater)
        {
            OnWaterStateChange();
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
        if (hit.gameObject.layer == 15)
        {
            Vector3 dif = (hit.transform.position - transform.position).normalized;
            Vector3 _direction = new Vector3(dif.x,.01f, dif.z);
            Debug.Log(hit.controller.velocity.magnitude);
            if (hit.rigidbody.velocity.magnitude == 0)
            {
                hit.transform.GetComponent<AudioSource>().Play();
            }
            hit.rigidbody.AddForceAtPosition( _direction* hit.controller.velocity.magnitude*_forceMultiplierOAffectableObjects,hit.point);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3.down * (GetComponent<CharacterController>().height / 2 + _groundCheckOffset)), _sphereRadious);
    }

    #region Eventos

    #endregion

    #region Metodos
    public bool checkGrounded()
    {
        return checkLayer(_groundLayer);
    }
    bool checkLayer(LayerMask Layer)
    {
        return Physics.CheckSphere(transform.position + (Vector3.down * ((_characterController.height / 2) + _groundCheckOffset)), _sphereRadious, Layer);
    }

    #endregion

    #region Propiedades
    public bool IsInWater { get => _isInWater; set => _isInWater = value; }
    #endregion
}
