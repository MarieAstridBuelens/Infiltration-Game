using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            SceneManager.LoadScene("Win Screen");
            //Debug.Log("The player is here!");
        }
    }
}
