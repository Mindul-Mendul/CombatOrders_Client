using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Warrior : Job
{
    public Warrior()
    {
        Att = 1;
        Def = 1;
        MaxHP = 1;
        MovSpd = 1;
        AttSpd = 1;
        Q = this.gameObject.AddComponent<Skill_WarriorQ>();
        W = this.gameObject.AddComponent<Skill_WarriorW>();
        E = this.gameObject.AddComponent<Skill_WarriorE>();
        R = this.gameObject.AddComponent<Skill_WarriorR>();
    }


    public class Skill_WarriorQ : Skill
    {
        GameObject WarriorQ;
        public Skill_WarriorQ()
        {
            //spriteRenderer = ;
            //spriteMask = 1;
            //img_Skill = 1;
            level = 1;
            maxStack = 1;
            stackCount = maxStack;
            damage = 1.0f;
            cooltime = 3.0f;
            isCooling = false;
        }
    }
    public class Skill_WarriorW : Skill
    {
        GameObject WarriorQ;
        public Skill_WarriorW()
        {
            //spriteRenderer = ;
            //spriteMask = 1;
            //img_Skill = 1;
            level = 1;
            maxStack = 1;
            stackCount = maxStack;
            damage = 2.0f;
            cooltime = 5.0f;
            isCooling = false;
        }
    }
    public class Skill_WarriorE : Skill
    {
        GameObject WarriorQ;
        public Skill_WarriorE()
        {
            //spriteRenderer = ;
            //spriteMask = 1;
            //img_Skill = 1;
            level = 1;
            maxStack = 1;
            stackCount = maxStack;
            damage = 3.0f;
            cooltime = 7.0f;
            isCooling = false;
        }
    }
    public class Skill_WarriorR : Skill
    {
        GameObject WarriorQ;
        public Skill_WarriorR()
        {
            //spriteRenderer = ;
            //spriteMask = 1;
            //img_Skill = 1;
            level = 1;
            maxStack = 1;
            stackCount = maxStack;
            damage = 4.0f;
            cooltime = 9.0f;
            isCooling = false;
        }
    }
}