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
    [SerializeField] float LevelUIElementOffset;


    private List<GameObject> InstantiatedUILevelElements = new List<GameObject>();
    List<Level> levels = new List<Level>();
    private void OnEnable()
    {
        EventBus.Subscribe<LevelSelectRefresh>(BuildLevelsListUI);
    }
    private void Awake()
    {
        
    }
    
    public void BuildLevelsListUI(LevelSelectRefresh parameters)
    {
        ClearLevelUIElements();
        levels = GameLogic.GetLevelList();
        Vector3 UIPosition = Vector3.zero;
        foreach (Level level in levels) {
            GameObject levelElement = Instantiate(LevelUIElement, LevelListContainer.transform);
            levelElement.GetComponentInChildren<TMP_Text>().text = level.sceneString;

            Button button = levelElement.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => EventBus.Raise(new LevelSelect(level)));

            RectTransform elementRect = levelElement.GetComponent<RectTransform>();
            elementRect.anchoredPosition = UIPosition;
            UIPosition += new Vector3(0, LevelUIElementOffset - elementRect.rect.height, 0);

            InstantiatedUILevelElements.Add(levelElement);
        }
    }
    public void ClearLevelUIElements()
    {
        foreach(GameObject g in InstantiatedUILevelElements)
        {
            Destroy(g);
        }
    }
}
