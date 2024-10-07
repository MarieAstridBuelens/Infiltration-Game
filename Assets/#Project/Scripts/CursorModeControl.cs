using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorModeControl : MonoBehaviour
{
    [SerializeField] private bool visible;
    [SerializeField] CursorLockMode lockMode;
    //permet de faire ça dans toutes les scènes du jeu et décider comment ça se comporte
    

    void Start()
    {
        Cursor.lockState = lockMode; //le curseur n'est pas lock, peut aller où il veut
        Cursor.visible = visible;
    }

}
