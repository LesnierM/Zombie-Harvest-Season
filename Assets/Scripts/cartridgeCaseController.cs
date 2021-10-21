using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class cartridgeCaseController : MonoBehaviour
{
	
	//Variables Expuestas
	[Header("Fuerza en X")]
	[SerializeField] float _minXForce;
	[SerializeField] float _maxXForce;
	[Header("Fuerza en Y")]
	[SerializeField] float _minYForce;
	[SerializeField] float _maxYForce;
	[SerializeField] AudioClip[] sounds;
    //Variables
    //Componentes
    //Clases
    void Start()
    {
		GetComponent<Rigidbody>().AddRelativeForce(-Random.Range(_minXForce,_maxXForce),0, -Random.Range(_minYForce, _maxYForce));
		Invoke("playSound", .5f);
		Destroy(gameObject,1);
    }
	void playSound()
    {
		GetComponent<AudioSource>().PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
    }
				
	#region Eventos
	
	#endregion
			
	#region Metodos
	
	#endregion
				
	#region Propiedades
	
	#endregion
	    
}
