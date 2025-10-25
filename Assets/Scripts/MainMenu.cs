using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    private void Start()
    {
        //Debug.Log(Application.persistentDataPath);
        GameLogic.FetchLevels();
        playerAnimator.SetBool("mainMenu", true); //TO IMPLEMENT: use events
    }
    private void Update()
    {
        
    }
}
