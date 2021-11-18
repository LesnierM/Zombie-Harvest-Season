using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageableObjects : MonoBehaviour
{
	//Variables Expuestas
	[SerializeField] int _maxHp;
	[SerializeField] int _currentHp;
	[SerializeField] GameObject _fireParticles;
    //Variables
    //Componentes
    //Clases
    void Start()
    {
        
    }

    void Update()
    {
        
    }

	#region Eventos

	#endregion

	#region Metodos
	public void damage()
	{
		_currentHp--;
		if (_currentHp == 0)
		{
			_fireParticles.SetActive(true);
		}
	}  
	#endregion
				
	#region Propiedades
	
	#endregion
	    
}
