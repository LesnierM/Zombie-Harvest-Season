using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    //Variables Expuestas
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
	public void ChangeScene(string ScenenName)
    {
        SceneManager.LoadScene(ScenenName);
    }
	#endregion
				
	#region Propiedades
	
	#endregion
	    
}
