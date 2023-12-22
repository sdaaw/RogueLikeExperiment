using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
public class Item
{
    public List<Stat> Stats = new();

    public string ItemName { get; set; }

    public string Description { get; set; }
    public Item(bool rollRandomStats = false, int statCount = 3) 
    {
        Random r = new();
        ItemName = Constants.ADJECTIVES[r.Next(0, Constants.ADJECTIVES.Length)] + " " + Constants.ITEMNAMES[r.Next(0, Constants.ITEMNAMES.Length)];
        if(rollRandomStats)
        {
            RollRandomStats(statCount);
        }
    }
    private void RollRandomStats(int count)
    {
        Random r = new();
        for (int i = 0; i < count; i++) 
        {
            Stat.StatType[] types = (Stat.StatType[])Enum.GetValues(typeof(Stat.StatType));
            Stat stat = new();
            int roll = r.Next(1, types.Length);
            stat.statType = types[roll];
            stat.StatValue = r.Next(1, 100);
            Tuple<string, string> namedesc = stat.GetStatAndDescription(stat);
            stat.Name = namedesc.Item1;
            stat.Description = namedesc.Item2;
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
