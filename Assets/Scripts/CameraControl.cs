using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Logger LoggerInstance;
    public enum CameraMode
    {
        SmoothFollow,
        StaticLookat
    }

    [SerializeField] GameObject target;
    [SerializeField] bool UseInitialOffset = true;
    [SerializeField] Vector3 CustomOffset;
    [SerializeField] CameraMode mode;
    [Header("Smooth Follow settings")]
    [SerializeField] float LerpSpeed = 0.3f;

    // Start is called before the first frame update
    Vector3 currentOffset;
    private void Awake()
    {
        LoggerInstance = new Logger("CameraControl");
        if (target == null)
        {
            LoggerInstance.Error("target not set.");
            this.enabled = false;
        }
        if (UseInitialOffset)
        {
            currentOffset = this.gameObject.transform.position - target.transform.position;
        }
        else
        {
            currentOffset = CustomOffset;
        }
        gameObject.transform.LookAt(target.transform.position, Vector3.up);
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (mode == CameraMode.SmoothFollow)
        {
            SmoothFollow();
        }
        else if (mode == CameraMode.StaticLookat)
        {

        }
    }
    void SmoothFollow()
    {
        Vector3 targetPos = new Vector3(target.transform.position.x + currentOffset.x, target.transform.position.y + currentOffset.y, target.transform.position.z + currentOffset.z);
        Vector3 delta = targetPos - transform.position;
        transform.position += delta.normalized * LerpSpeed * delta.magnitude * Time.deltaTime; 
    }
    public void ChangeTarget(GameObject newTarget)
    {
        target = newTarget;
        transform.LookAt(target.transform, Vector3.up);
    }
    public void ChangeMode(CameraMode newMode)
    {
        mode = newMode;
    }
}
