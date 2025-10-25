using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string Path = Application.persistentDataPath + "/levels.json";
    public static void SaveLevels(List<Level> levels)
    {
        string json = JsonUtility.ToJson(new LevelList(levels), true);
        File.WriteAllText(Path, json);
    }
    public static List<Level> LoadLevels()
    {
        if (!File.Exists(Path))
            return new List<Level>();

        string json = File.ReadAllText(Path);
        LevelList wrapper = JsonUtility.FromJson<LevelList>(json);
        return wrapper.levels;
    }

    [System.Serializable]
    private class LevelList
    {
        public List<Level> levels;
        public LevelList(List<Level> levels) { this.levels = levels; }
    }
}
