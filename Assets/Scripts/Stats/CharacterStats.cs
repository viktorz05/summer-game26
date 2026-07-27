using System;
using System.Collections.Generic;
using UnityEngine;
// Credit:https://onewheelstudio.com/blog/2022/11/8/how-i-do-stats
[CreateAssetMenu(fileName = "CharacterStats", menuName = "Scriptable Objects/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    [Serializable]
    private struct StatEntry
    {
        public Stat stat;
        public float value;
    }

    [SerializeField] private List<StatEntry> baseStats;

    public Dictionary<Stat, float> GetBaseStats()
    {
        Dictionary<Stat, float> stats = new Dictionary<Stat, float>(); 
        foreach (var entry in baseStats)
        {
            stats.Add(entry.stat, entry.value);
        }
        return stats;
    }

}
