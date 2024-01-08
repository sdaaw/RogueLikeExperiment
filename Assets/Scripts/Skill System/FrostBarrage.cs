using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBarrage
{
    public string SkillName { get; set; }
    public string SkillDescription { get; set; }

    public int MulticastCount;

    public GameObject objectPrefab;

    public GameObject[] Cast()
    {
        GameObject[] objectList = new GameObject[MulticastCount];
        for(int i = 0; i < MulticastCount; i++) 
        {
            objectList[i] = objectPrefab;
        }

        return objectList;
    }
}
