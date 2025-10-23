using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{
    public class LevelStart : EventData //When level starts
    {

    }
    public class LevelClear : EventData //When player reaches exit
    {

    }
    public class  LevelFail : EventData // When torch Runs out
    {
        
    }
    public class TriggerSelected : EventData
    {

    }
    public class  TriggerInteracted : EventData //When some interactable is interacted
    {
        
    }
}


