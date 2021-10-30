using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudsController : MonoBehaviour
{
	//Variables Expuestas
    [SerializeField] float _speed;
    //Variables
    //Componentes
    //Clases
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, _speed * Time.deltaTime);
    }
}
