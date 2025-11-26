using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string LevelsFolder =
        Path.Combine(Application.persistentDataPath, "Levels");
    public static void SaveLevels(List<Level> levels)
    {
        //string json = JsonUtility.ToJson(new LevelList(levels), true);
        //File.WriteAllText(Path, json);

        if (!Directory.Exists(LevelsFolder))
            Directory.CreateDirectory(LevelsFolder);

        for (int i = 0; i < levels.Count; i++) 
        {
            string LevelJson = JsonUtility.ToJson(levels[i]);
            string pathName = Path.Combine(LevelsFolder, levels[i].id + ".json");
            File.WriteAllText (pathName, LevelJson);
        }
    }
    public static List<Level> LoadLevels()
    {
        List<Level> result = new List<Level>();
        if (!Directory.Exists(LevelsFolder))
        {
            Debug.LogWarning("Empty levels folder.");
            return result;
        }

        string[] files = Directory.GetFiles(LevelsFolder, "*.json");
        foreach (string path in files)
        {
            string json = File.ReadAllText(path);
            Level lvl = JsonUtility.FromJson<Level>(json);
            result.Add(lvl);
        }
        return result;
    }

    [System.Serializable]
    private class LevelList
    {
        public List<Level> levels;
        public LevelList(List<Level> levels) { this.levels = levels; }
    }
}
