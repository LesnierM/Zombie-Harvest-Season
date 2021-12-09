using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
	enum States
    {
		Idle,
		Walk
    }
	//Variables Expuestas
	[SerializeField] EnemyTypes _type;
	[Header("Vida")]
	[SerializeField] int _maxHp;
	[Header("Daño")]
	[SerializeField] float _nomalAttack;
	[Header("Acciones")]
	[SerializeField] float _minWalkTime;
	[SerializeField] float _maxWalkTime;
	[SerializeField] float _minIdleTime;
	[SerializeField] float _maxIdleTime;
	//Variables
	States _currentState;
	float _walkTime;
	float _nextWalkTime;
	float _idleDuration;
	int _curentHp;
	//Componentes
	Animator _animator;
    //Clases
    void Awake()
    {
        createIdleTime();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        #region Acciones

        #endregion
    }

    #region Eventos

    #endregion

    #region Metodos
    private void createIdleTime()
	{
		_idleDuration = Random.Range(_minIdleTime, _maxIdleTime);
	}
	public void damage(int DamageTaken,Vector3 impactPoint)
    {
		Destroy(Instantiate(Resources.Load("Particles\\bloodSplash"), impactPoint, Quaternion.LookRotation(impactPoint.normalized), transform),1);
    }
	#endregion

	#region Propiedades

	#endregion

}
