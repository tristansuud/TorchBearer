using GameEvents;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : UIManager
{
    [SerializeField] GameObject LevelListContainer;
    [Header("Level List Builder")]
    [SerializeField] GameObject LevelUIElement;
    [SerializeField] GameObject LevelChoiceElementDone;
    [SerializeField] GameObject LevelChoiceElementUnlocked;
    [SerializeField] GameObject LevelChoiceElementLocked;
    [SerializeField] GameObject BestTimeTextDisplay;
    [SerializeField] float LevelUIElementOffset;


    private List<GameObject> InstantiatedUILevelElements = new List<GameObject>();
    List<Level> levels = new List<Level>();
    private void OnEnable()
    {
        base.OnEnable();
        EventBus.Subscribe<LevelSelectRefresh>(BuildLevelsListUI);
        EventBus.Subscribe<LevelSelect>(UpdateBestTimeDisplay);
    }
    private void Awake()
    {
        
    }
    private void OnDisable()
    {
        base.OnDisable();
        EventBus.Unsubscribe<LevelSelectRefresh>(BuildLevelsListUI);
        EventBus.Unsubscribe<LevelSelect>(UpdateBestTimeDisplay);
    }

    public void UpdateBestTimeDisplay(LevelSelect param)
    {
        TMP_Text text = BestTimeTextDisplay.GetComponent<TMP_Text>();
        text.text = FloatIntoAnalogTime(param.selectedLevel.bestTimeCompletion);
    }
    public void BuildLevelsListUI(LevelSelectRefresh parameters)
    {
        ClearLevelUIElements();
        levels = GameLogic.GetLevelList();
        Vector3 UIPosition = Vector3.zero;
        foreach (Level level in levels) {
            GameObject levelElement;
            if (level.finished == true) 
            {
               levelElement = Instantiate(LevelChoiceElementDone, LevelListContainer.transform);
            } 
            else if (level.unlocked == true)
            {
                levelElement = Instantiate(LevelChoiceElementUnlocked, LevelListContainer.transform);
            } 
            else
            {
                levelElement = Instantiate(LevelChoiceElementLocked, LevelListContainer.transform);
            }
            //GameObject levelElement = Instantiate(LevelUIElement, LevelListContainer.transform);
            levelElement.GetComponentInChildren<TMP_Text>().text = level.sceneString;

            Button button = levelElement.GetComponentInChildren<Button>();
            if (level.unlocked) button.onClick.AddListener(() => EventBus.Raise(new LevelSelect(level)));

            //RectTransform elementRect = levelElement.GetComponent<RectTransform>();
            //elementRect.anchoredPosition = UIPosition;
            //UIPosition += new Vector3(0, LevelUIElementOffset - elementRect.rect.height, 0);

            InstantiatedUILevelElements.Add(levelElement);
        }
    }
    public void ClearLevelUIElements()
    {
        foreach(GameObject g in InstantiatedUILevelElements)
        {
            if (g) Destroy(g);
        }
        InstantiatedUILevelElements.Clear();
    }
    public string FloatIntoAnalogTime(float value)
    {
        int minutes = Mathf.FloorToInt(value / 60f);
        int seconds = Mathf.FloorToInt(value % 60f);
        return $"{minutes:00}:{seconds:00}";
    }
}
