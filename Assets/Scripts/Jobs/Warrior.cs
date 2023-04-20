using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Warrior: Job
{
    public Warrior()
    {
        Att = 1;
        Def = 1;
        MaxHP = 1;
        MovSpd = 1;
        AttSpd = 1;

        SkillQ_Warrior skillQ_Warrior = new();
        SkillW_Warrior skillW_Warrior = new();
        SkillE_Warrior skillE_Warrior = new();
        SkillR_Warrior skillR_Warrior = new();
        Q = skillQ_Warrior;
        W = skillW_Warrior;
        E = skillE_Warrior;
        R = skillR_Warrior;
    }

    class SkillQ_Warrior : Skill
    {
        public SkillQ_Warrior()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteMask = GetComponent<SpriteMask>();
            img_Skill = GetComponent<Image>();
            level = 1;
            maxStack = 1;
            stackCount = maxStack;
            damage = 1.0f;
            cooltime = 3.0f;
            isCooling = false;
        }
    }
    class SkillW_Warrior : Skill
    {
        public SkillW_Warrior()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteMask = GetComponent<SpriteMask>();
            img_Skill = GetComponent<Image>();
            level = 1;
            maxStack = 1;
            stackCount = maxStack;
            damage = 2.0f;
            cooltime = 5.0f;
            isCooling = false;
        }
    }
    class SkillE_Warrior : Skill
    {
        public SkillE_Warrior()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteMask = GetComponent<SpriteMask>();
            img_Skill = GetComponent<Image>();
            level = 1;
            maxStack = 1;
            stackCount = maxStack;
            damage = 3.0f;
            cooltime = 7.0f;
            isCooling = false;
        }
    }
    class SkillR_Warrior : Skill
    {
        public SkillR_Warrior()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteMask = GetComponent<SpriteMask>();
            img_Skill = GetComponent<Image>();
            level = 1;
            maxStack = 1;
            stackCount = maxStack;
            damage = 4.0f;
            cooltime = 9.0f;
            isCooling = false;
        }
    }
}