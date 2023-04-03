using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static int score = 0;

    public static void setScore(int value)
    {
        score += value;
    }
    public static int getScore()
    {
        return score;
    }
}
