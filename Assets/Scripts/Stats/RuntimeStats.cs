using System.Collections.Generic;
using UnityEngine;

public class RuntimeStats
{
    private Dictionary<Stat, float> stats;

    public RuntimeStats(CharacterStats SO)
    {
        stats = SO.GetBaseStats();
    }

    public void SetStat(Stat stat, float amount)
    {
        if (stats == null)
            Debug.Log("Base stats not set!");

        if (stats.TryGetValue(stat, out var value))
        {
            stats[stat] += amount;
        }
        else
        {
            Debug.Log($"No stat {stat} found for character!");
        }
    }

    public float GetStat(Stat stat)
    {

        if (stats == null)
            Debug.Log("Base stats not set!");

        if (stats.TryGetValue(stat, out var value))
        {
            return value;
        }
        else
        {
            Debug.Log($"No stat {stat} found for character!");
            return 0f;
        }
    }

}
