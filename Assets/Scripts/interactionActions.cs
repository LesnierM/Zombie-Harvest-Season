using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionActions : MonoBehaviour
{
	//Variables Expuestas
	[SerializeField] string _actionText;
    [SerializeField] InteractionActions _action;
    //Variables
    //Componentes
    //Clases
    gameManager _gameManager;
    void Start()
    {
        _gameManager = FindObjectOfType<gameManager>();
    }

    void Update()
    {
        
    }

	#region Eventos

	#endregion

	#region Metodos
    public void performAction()
    {
        switch (_action)
        {
            case InteractionActions.None:
                break;
            case InteractionActions.SaveGame:
                _gameManager.saveGame();
                break;
        }
    }
    #endregion

    #region Propiedades
    public string ActionText { get => _actionText; }
    #endregion

}
