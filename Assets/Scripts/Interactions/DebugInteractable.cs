using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInteractable : InteractTrigger
{
    private Logger LoggerInstance;
    public bool InteractableState = false;
    private void Awake()
    {
        LoggerInstance = new Logger("Debug Interactable");
        LoggerInstance.Enable();
    }
    public override void DoTrigger()
    {
        LoggerInstance.Log("HELLO THIS IS DEBUG INTERACTABLE");
    }
}
