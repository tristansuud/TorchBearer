using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class Door : MonoBehaviour
{
    private Logger LoggerInstance;
    [SerializeField] bool InitialLockedState = false;
    private bool Locked = false;
    Rigidbody rb;
    private void Awake()
    {
        LoggerInstance = new Logger("Door");
        rb = GetComponent<Rigidbody>();
        Locked = InitialLockedState;
        if (rb == null)
        {
            LoggerInstance.Error("Door doesn't have a hinge.");
        }
    }
    private void Update()
    {
        if (Locked)
        {
            rb.freezeRotation = true;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            rb.freezeRotation = false;
        }
    }
    public void ModifyDoorLock(bool locked)
    {
        Locked = locked;
    }
    public bool IsLocked() { return Locked; }
}
