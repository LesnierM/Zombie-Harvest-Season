using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementEffectController : MonoBehaviour
{
	//eventos
	//Variables Expuestas
	[SerializeField] bool _movementEffect;
	[SerializeField] Vector2 _intervals=new Vector2(2,1);
	[SerializeField] Vector2 _multiplier = new Vector2(.02f, .04f);
	[SerializeField] AnimationCurve _animationCurve=new AnimationCurve(new Keyframe(0,0),
		new Keyframe(.5f,1),new Keyframe(1,0),new	Keyframe(1.5f,-1),new Keyframe(2,0));
	[SerializeField] float _runningSpeedMultiplier=.75f;
	//Variables
	Vector3 _cameraInitialPosition=Vector3.zero;
	float _animationCurveMaxTime;
	float _lastFrameTime;
	Vector2 _movementEffectTime;
	//Componentes
    //Clases
	CharacterController _characterController;
	moveController _playerMoveController;
	soundEffectsController _soundEffectsController;
	Camera _camera;
    void Start()
    {
		_camera = Camera.main;
		_soundEffectsController=GetComponent<soundEffectsController>();
		_characterController = GetComponent<CharacterController>();
		_playerMoveController = GetComponent<moveController>();
		_animationCurveMaxTime = _animationCurve[_animationCurve.length - 1].time;
		_cameraInitialPosition = _camera.transform.localPosition;
    }

    void LateUpdate()
    {
        if (!_playerMoveController.IsGrounded||_playerMoveController.IsCrouch)
        {
			return;
        }
		float _characterVelocityOnXAndZ =new Vector3(_characterController.velocity.x,0,_characterController.velocity.z).magnitude;
		_movementEffectTime.x += _characterVelocityOnXAndZ * Time.deltaTime/ _intervals.x ;
		_movementEffectTime.y += _characterVelocityOnXAndZ * Time.deltaTime/ _intervals.y ;
        if (_movementEffectTime.x > _animationCurveMaxTime)
        {
			_movementEffectTime.x -= _animationCurveMaxTime;
        }
		if (_movementEffectTime.y > _animationCurveMaxTime)
        {
			_movementEffectTime.y -= _animationCurveMaxTime;
        }
		float xPos = _animationCurve.Evaluate(_movementEffectTime.x)*_multiplier.x;
		float yPos = _animationCurve.Evaluate(_movementEffectTime.y)*_multiplier.y;
		Vector3 _finalMovement = new Vector3(xPos, yPos, 0);
		//ejecutar sonido de paso
		if (_lastFrameTime < 1.5f && _movementEffectTime.y > 1.5f)
		{
			_soundEffectsController.OnStep();
		}
		_lastFrameTime = _movementEffectTime.y;
        if (_playerMoveController.IsRunning)
        {
			_finalMovement *= _runningSpeedMultiplier;
        }
		if (_movementEffect)
		{
			if (_characterVelocityOnXAndZ > 0.1f)
			{
				_camera.transform.localPosition = _cameraInitialPosition + _finalMovement;
			}
			else
			{
				_camera.transform.localPosition = _cameraInitialPosition;
			}
		}
    }		
				
	#region Eventos
	
	#endregion
			
	#region Metodos
	
	#endregion
				
	#region Propiedades
	
	#endregion
	    
}
