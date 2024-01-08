using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
public class Item
{
    public List<ItemStat> Stats = new();

    public string ItemName { get; set; }
    public string Description { get; set; }
    public void RollRandomStats(int count)
    {
        System.Random r = new();
        for (int i = 0; i < count; i++) 
        {
            ItemStat stat = ItemStat.CreateStat(r);
            Stats.Add(stat);
        }
        BuildDescription();
    }

    private void BuildDescription()
    {
        Description = "";
        foreach(ItemStat stat in Stats)
        {
            Description += stat.Name + ": " + stat.StatValue.ToString() + "\n";
        }
    }
}
