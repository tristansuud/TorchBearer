using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAbility : MonoBehaviour
{
    Rigidbody currentTarget = null;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Pushable")
        {
            
            currentTarget = other.gameObject.GetComponent<Rigidbody>();
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        currentTarget = null;
    }
    public Rigidbody getPushTarget()
    {
        
        return currentTarget;
    }
}
