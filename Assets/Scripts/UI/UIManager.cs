using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("UI elements that should be active when this UIManager is enabled.")]
    [SerializeField] private List<GameObject> activeUIElements = new List<GameObject>();

    [Tooltip("UI elements that should be disabled when this UIManager is enabled.")]
    [SerializeField] private List<GameObject> inactiveUIElements = new List<GameObject>();

    protected virtual void Awake()
    {
        // optional: automatically deactivate everything on load
        SetUIState(false);
    }

    protected virtual void OnEnable()
    {
        SetUIState(true);
    }

    protected virtual void OnDisable()
    {
        SetUIState(false);
    }

    protected void SetUIState(bool enable)
    {
        if (enable)
        {
            foreach (var ui in activeUIElements)
            {
                if (ui != null)
                    ui.SetActive(true);
            }

            foreach (var ui in inactiveUIElements)
            {
                if (ui != null)
                    ui.SetActive(false);
            }
        }
        else
        {
            // optional: define what happens when disabling the whole UI manager
        }
    }
}