using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameLogic
{
    public static Dictionary<string, Level> Levels = new ();
    public static Level currentLevel { get; private set; }
    private static List<string> HiddenLevelNames = new List<string>() { "MainMenu"};

    public static void FetchLevels()
    {
        var loaded = SaveSystem.LoadLevels();
        if (loaded.Count == 0)
        {
            // Build from build settings if no JSON yet
            Levels = BuildLevelsFromScenes();
            SaveSystem.SaveLevels(Levels.Values.ToList());
        }
        else
        {
            Levels = loaded.ToDictionary(l => l.id, l => l);
        }

    }
#if UNITY_EDITOR
    private static Dictionary<string, Level> BuildLevelsFromScenes()
    {
        var result = new Dictionary<string, Level>();

        // get all scenes in build settings
        var scenes = UnityEditor.EditorBuildSettings.scenes;

        foreach (var scene in scenes)
        {
            string sceneName = Path.GetFileNameWithoutExtension(scene.path);
            if (HiddenLevelNames.Contains(sceneName)){
                continue;
            }
            Debug.Log("Loading..." + sceneName);
            var level = new Level
            {
                id = sceneName,
                sceneString = sceneName,
                unlocked = (result.Count == 0), // unlock the first scene by default
                finished = false,
                bestTimeCompletion = 0f
            };

            result.Add(sceneName, level);
        }

        Debug.Log($"[GameLogic] Built {result.Count} levels from Build Settings.");
        return result;
    }
#endif
    public static void SelectLevel(string levelId)
    {
        Debug.Log("Selected level: " + levelId);
        currentLevel = GetLevel(levelId);
        Debug.Log("Current Level: " + currentLevel.name);
    }
    public static void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        currentLevel = null;
    }
    public static void LoadLevel() 
    {
        if (currentLevel == null) return;
        
        SceneManager.LoadScene(currentLevel.sceneString, LoadSceneMode.Single);
        

    }
    public static Level GetLevel(string levelId)
    {
        Levels.TryGetValue(levelId, out Level level);
        Debug.Log("GetLevel, level value: " + level);
        return level;
    }
    public static void SaveAllLevels()
    {
        SaveSystem.SaveLevels(Levels.Values.ToList());
    }

    public static List<Level> GetLevelList()
    {
        return Levels.Values.ToList();
    }
    // public Setting GetSetting{} //get the settings object so that scripts can just modify the instance. Setting object will also apply the settings.

    public static void LevelWin()
    {
        currentLevel.bestTimeCompletion = 0f;
        currentLevel.finished = true;
        //unlock next level (how)
    }
    public static void LevelLose()
    {

    }
    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }


}
