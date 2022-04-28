using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stats : Attributes
{
    [Serializable]
    public struct BaseStat
    {
        public string name;
        public int value;
        public int modifier;
        public int tempValue;
    }
    public BaseStat[] baseStats = new BaseStat[6];
    public override void Start()
    {
        base.Start();
        baseStats[0].name = "Strength";
        baseStats[1].name = "Dexterity";
        baseStats[2].name = "Constitution";
        baseStats[3].name = "Wisdom";
        baseStats[4].name = "Intelligence";
        baseStats[5].name = "Charisma";
    }
}
