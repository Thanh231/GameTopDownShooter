using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper 
{
    public static float GetQualityLevelUp(int currentLevel)
    {
        return 0.5f * currentLevel / (2 - 0.5f);
    }
}
