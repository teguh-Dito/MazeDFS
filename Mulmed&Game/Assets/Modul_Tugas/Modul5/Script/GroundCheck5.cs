using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck5 : MonoBehaviour
{
    Movement5 logicMovement;

    private void Start(){
        logicMovement = this.GetComponentInParent<Movement5>();
    }
    private void OnTriggerEnter(Collider other){
        logicMovement.groundedChanger();
        Debug.Log("Touch The Ground");
    }
}
