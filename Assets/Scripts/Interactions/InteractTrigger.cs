using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    PlayerControl playerController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnEnter();
            playerController = other.gameObject.GetComponent<PlayerControl>();
            if (playerController == null)
            {
                Debug.LogError("Object has player tag but no PlayerControl", this);
            }
            playerController.SetInteractTrigger(this);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnStay();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnExit();
            playerController.SetInteractTrigger(null);
        }
    }
    public virtual void DoTrigger() { }
    public virtual void OnEnter() { }
    public virtual void OnStay() { }
    public virtual void OnExit() { }
}
