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
        Random r = new();
        for (int i = 0; i < count; i++) 
        {
            //get random type from all the stat types and assign it a random value and getting the corresponding name and description for said stat type
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
