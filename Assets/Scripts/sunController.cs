using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunController : MonoBehaviour
{
	//Variables Expuestas
	[SerializeField] float _dayLightSpeed;
	[SerializeField] float _nightLightSpeed;
    //Variables
    //Componentes
    //Clases
    void Start()
    {
        
    }

    void FixedUpdate()
    {
		float _rotacion = transform.rotation.eulerAngles.x;
		float _velocity= _rotacion > 190 && _rotacion < 360 ? _nightLightSpeed : _dayLightSpeed;
		transform.Rotate(Vector3.right, _velocity * Time.deltaTime);
       
    }		
				
	#region Eventos
	
	#endregion
			
	#region Metodos
	
	#endregion
				
	#region Propiedades
	
	#endregion
	    
}
