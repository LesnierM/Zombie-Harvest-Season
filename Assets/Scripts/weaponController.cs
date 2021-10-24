using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponController : MonoBehaviour
{
	public delegate void EventHandler();
	public delegate void ShootEventHandler(int CartridgeRemainigBullets,int BulletsCount);
	public event EventHandler OnReload;
	public event ShootEventHandler OnAmmoStatusChange;

	//Variables Expuestas
	[SerializeField] Weapons _weaponType;
	[Header("Propiedades")]
	[SerializeField] float _cartridgeBulletDelay;
	[SerializeField] int _cartridgeCaseCapacity;
	[SerializeField] int _cartridgeCaseRemaningBullets;
	[SerializeField] int _bulletsCount;
	[SerializeField] int _bulletsMaxCount;
	[Header("Retraso de sonidos")]
	[SerializeField] float _shootSoundDelay;
    [SerializeField] float _reloadSoundDelay;
	[Header("Sonidos")]
	[SerializeField] AudioClip _shootSound;
	[SerializeField] AudioClip _reloadSound;
    [SerializeField] GameObject _cartridgeCase;
	[Header("Disparo")]
	[SerializeField] GameObject _bulletHole;
	[SerializeField] GameObject _muzzleFlash;
	[SerializeField] LayerMask _hitLayerMask;
	[SerializeField] float _fireRate;
	[SerializeField] float _maxRange;
	[SerializeField] float _effectiveMaxRange;
	[SerializeField] float _bulletHoleDuration;
	[SerializeField] float _aimingZoomMultiplier;
	[Tooltip("Velocidad del proyectil en u/s")]
	[SerializeField] float _bulletSpeed;
	[Header("Imagen Ui")]
	[SerializeField] Texture2D _UiImage;

	//Variables
	RaycastHit _rayHit;
	float _lastFiredTime;
	//Componentes
	Animator _animator;
	AudioSource _soundPlayer;
	//Clases
	moveController _playerController;
	//debug
	float _xAxyRandomDirection;
	float _yAxyRandomDirection;
	void Awake()
    {
		_playerController = transform.parent.GetComponentInParent<moveController>();
		_animator = GetComponent<Animator>();
		_soundPlayer = GetComponent<AudioSource>();
    }
    private void OnDrawGizmos()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.parent.parent.position,transform.parent.parent.position +transform.parent.parent.forward * _maxRange);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.parent.parent.position + transform.parent.parent.forward * _effectiveMaxRange, transform.parent.parent.position + transform.parent.parent.forward * _effectiveMaxRange+ (transform.parent.parent.forward + transform.parent.parent.up*_yAxyRandomDirection + transform.parent.parent.right * _xAxyRandomDirection) *(_maxRange-_effectiveMaxRange));
    }

    #region Eventos
    #endregion

    #region Metodos
    public void shoot()
    {
		//no disparar si esta disparando
        if (_playerController.IsRunning||_lastFiredTime+_fireRate>Time.time||_animator.GetCurrentAnimatorStateInfo(1).IsName("reloading"))
        {
			return;
        }
		_lastFiredTime = Time.time;
		if (_cartridgeCaseRemaningBullets > 0)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("aiming"))
            {
				Destroy(Instantiate(_muzzleFlash, transform.GetChild(2).transform.position, Quaternion.Euler(_muzzleFlash.transform.forward), transform), .5f); ;
            }
            else
            {
			Destroy(Instantiate(_muzzleFlash, transform.GetChild(1).transform.position, Quaternion.Euler(_muzzleFlash.transform.forward),transform),.5f);
            }
            --_cartridgeCaseRemaningBullets;
			OnAmmoStatusChange(_cartridgeCaseRemaningBullets, _bulletsCount);
            _animator.Play("shoot", 1);
            Invoke("showCartridge", _cartridgeBulletDelay);
            Invoke("shootSound", _shootSoundDelay);
			//calcular desvio de proyectil
			_xAxyRandomDirection = Random.Range(-.1f, .1f);
			_yAxyRandomDirection = Random.Range(-.1f, .1f);
			if (Physics.Raycast(transform.parent.parent.position, transform.parent.parent.forward, out _rayHit, _effectiveMaxRange, _hitLayerMask))
			{
				Invoke("performBulletCollision", _rayHit.distance / _bulletSpeed);
				Debug.Log("En rango");
			}
			else if (Physics.Raycast(transform.parent.parent.position + transform.parent.parent.forward * _effectiveMaxRange, transform.parent.parent.forward + transform.parent.parent.up * _yAxyRandomDirection + transform.parent.parent.right * _xAxyRandomDirection, out _rayHit, _maxRange - _rayHit.distance, _hitLayerMask))
			{
				Invoke("performBulletCollision", _rayHit.distance / _bulletSpeed);
				Debug.Log("Fuera de rango");
			}
        }
        else
        {
			Reload();
        }
    }
    private void performBulletCollision()
    {
		//if (_rayHit.collider.tag == "Target")
		//{
		//	_rayHit.collider.gameObject.GetComponent<TargetScript>().isHit = true;
		//	Destroy(Instantiate(_bulletHole, _rayHit.point + _rayHit.normal * 0.001f, Quaternion.LookRotation(_rayHit.normal),_rayHit.collider.transform), _bulletHoleDuration);
		//	return;
		//}
		//else if (_rayHit.collider.tag == "Target")
		//{
		//	_rayHit.collider.gameObject.GetComponent<ExplosiveBarrelScript>().explode = true;
		//}
			Destroy(Instantiate(_bulletHole, _rayHit.point + _rayHit.normal * 0.001f, Quaternion.LookRotation(_rayHit.normal)), _bulletHoleDuration);
	}
    public void Reload()
	{
        if (_animator.GetCurrentAnimatorStateInfo(1).IsName("reloading")||_bulletsCount==0||_cartridgeCaseCapacity==_cartridgeCaseRemaningBullets)
        {
			return;
        }
		//si esta apuntando a la hora de recargar entonces cuando termine de racargar volver apuntar
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("aiming"))
        {
			Invoke("playStartAmingAnimationAfterReloading", 2.1f);
        }
		_bulletsCount = _bulletsCount - (_cartridgeCaseCapacity-_cartridgeCaseRemaningBullets);
		if (_bulletsCount < 0)
		{
			_cartridgeCaseRemaningBullets =_bulletsCount+ _cartridgeCaseCapacity;
			_bulletsCount = 0;
		}
		else
		{
			_cartridgeCaseRemaningBullets = _cartridgeCaseCapacity;
		}
		_animator.Play("reloading", 1);
		OnReload();
		OnAmmoStatusChange(_cartridgeCaseRemaningBullets, _bulletsCount);
		Invoke("reloadSound", _reloadSoundDelay);
	}
	void playStartAmingAnimationAfterReloading()
    {
		_animator.Play("startAiming",0);
	}
	void showCartridge()
	{
		Instantiate(_cartridgeCase, transform.GetChild(0).position, transform.GetChild(0).rotation);
	}
	void reloadSound()
    {
		_soundPlayer.PlayOneShot(_reloadSound);
	}
	void shootSound()
	{
		_soundPlayer.PlayOneShot(_shootSound);
	}
	#endregion
				
	#region Propiedades
    public GameObject CartridgeCase { get => _cartridgeCase;}
    public float AimingZoomMultiplier { get => _aimingZoomMultiplier;}
    public int CartridgeCaseRemaningBullets { get => _cartridgeCaseRemaningBullets;}
    public int BulletsCount { get => _bulletsCount; set => _bulletsCount = value; }
	public WeaponsStruct status
    {
        get
        {
			return new WeaponsStruct(_bulletsCount, _cartridgeCaseRemaningBullets, _weaponType);
        }
        set
        {
			_bulletsCount = value.BulletsTotalCount;
			_cartridgeCaseRemaningBullets = value.BulletsInCartridge;
        }
    }
    public Texture2D UiImage { get => _UiImage; }

    #endregion

}
