using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface ISkill
{
    public void Cast();
}

class Skill : ISkill
{
    public string SkillName { get; set; }
    public string SkillDescription { get; set; }

    public void Cast()
    {

    }
}
