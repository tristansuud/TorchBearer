using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string id;
    public string name;
    public string sceneString;
    public bool unlocked;
    public bool finished;
    public float bestTimeCompletion;
    public List<string> unlockedLevels;

    public void UpdateState(string? sceneString = null,string? name = null, bool? unlocked = null, bool? finished = null, float? bestTimeCompletion = null, List<string>? unlockedLevels = null)
    {
        if (sceneString != null) this.sceneString = sceneString;
        if (name != null) this.name = name;
        if (unlocked.HasValue)
            this.unlocked = unlocked.Value;

        if (finished.HasValue)
            this.finished = finished.Value;

        if (bestTimeCompletion.HasValue)
            this.bestTimeCompletion = bestTimeCompletion.Value;

        
    }
}
