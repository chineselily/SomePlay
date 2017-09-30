using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo
{
    public string userName;
    public int score { get; private set; }

    public void ChangeScore(int deltaScore)
    {
        score += deltaScore;
        score = Mathf.Max(0, score);
    }

    ///PlayerPrefs.SetString
}
