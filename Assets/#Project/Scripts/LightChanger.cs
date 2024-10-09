using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanger : MonoBehaviour
{
    public Light light;

    void Start(){
        light = GetComponent<Light>();
    }
    void Update(){
        // if(AICharacterController.visiblePlayer){
        //     light.color = Color.red;
        // }
        // else{
        //     light.color = Color.green;
        // }
    }
    
    
}
