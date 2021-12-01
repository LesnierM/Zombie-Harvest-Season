using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
	//Variables Expuestas
	//Variables
	AsyncOperation _farmScene;
	//Componentes
	//Clases
	void Awake()
	{
		StartCoroutine(loadScene());

	}

    void Update()
    {
        
    }

	#region Eventos

	#endregion

	#region Metodos
	IEnumerator loadScene()
    {
		yield return null;
		_farmScene = SceneManager.LoadSceneAsync("Farm");
		_farmScene.allowSceneActivation = false;
	}
	public void startGame()
	{
		if (_farmScene.progress>=0.9f)
		{
			_farmScene.allowSceneActivation = true;
		}
	}
	#endregion
				
	#region Propiedades
	
	#endregion
	    
}