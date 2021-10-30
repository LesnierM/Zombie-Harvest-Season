using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunController : MonoBehaviour
{
	//Variables Expuestas
	[SerializeField] float _speed;
    //Variables
    //Componentes
    //Clases
    void Start()
    {
        
    }

    void FixedUpdate()
    {
		transform.Rotate(Vector3.right, _speed * Time.deltaTime);
    }		
				
	#region Eventos
	
	#endregion
			
	#region Metodos
	
	#endregion
				
	#region Propiedades
	
	#endregion
	    
}
