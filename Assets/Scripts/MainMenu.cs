using GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    Logger LoggingInstance;

    [Header("Player")]
    [SerializeField] Animator playerAnimator;

    private void OnEnable()
    {
        EventBus.Subscribe<LevelSelect>(SelectLevel);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<LevelSelect>(SelectLevel);
    }
    private void Start()
    {
        //Debug.Log(Application.persistentDataPath);
        GameLogic.FetchLevels();
        playerAnimator.SetBool("mainMenu", true); //TO IMPLEMENT: use events
    }
    private void Awake()
    {
        LoggingInstance = new Logger("Main Menu");
    }
    
    public void QuitGame()
    {
        GameLogic.QuitGame();
    }
    public void SelectLevel(LevelSelect parameters)
    {
        GameLogic.SelectLevel(parameters.selectedLevel.id);
        
    }
    public void LoadLevel()
    {
        GameLogic.LoadLevel();
    }
    public void GetLevels()
    {
        //MainMenuUI.BuildLevelChoices(List<Level> levels) -> GameEvents.LevelSelectRefresh.Raise(new LevelSelectEvent())
        EventBus.Raise(new LevelSelectRefresh());
        LoggingInstance.Log("Getting levels");
    }
    private void Update()
    {
        
    }
}
