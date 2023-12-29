using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
public class Item
{
    public List<Stat> Stats = new();

    public string ItemName { get; set; }
    public string Description { get; set; }
    public void RollRandomStats(int count)
    {
        System.Random r = new();
        for (int i = 0; i < count; i++) 
        {
            Stat stat = Stat.CreateStat(r);
            Stats.Add(stat);
        }
        BuildDescription();
    }

    private void BuildDescription()
    {
        Description = "";
        foreach(Stat stat in Stats)
        {
            Description += stat.Name + ": " + stat.StatValue.ToString() + "\n";
        }
    }
}
