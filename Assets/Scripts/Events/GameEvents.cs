using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{
    public class GameStart : EventData //when app starts
    {

    }
    public class LevelSelectRefresh : EventData
    {
        
    }
    public class  LevelSelect: EventData
    {
        public Level selectedLevel;
        public LevelSelect(Level selectedLevel)
        {
            this.selectedLevel = selectedLevel;
        }
    }
    public class LevelEnter : EventData
    {

    }
    public class LevelStart : EventData //When level starts
    {

    }
    public class LevelClear : EventData //When player reaches exit
    {

    }
    public class  LevelFail : EventData // When torch Runs out
    {
        
    }
    public class LevelExit : EventData // When going back from level to main menu
    {
    
    }
    public class LevelTimerUpdate : EventData
    {

    }
    public class TriggerSelected : EventData
    {

    }
    public class  TriggerInteracted : EventData //When some interactable is interacted
    {
        
    }
    
}


