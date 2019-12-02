﻿using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static int score;

    Text text;

    // Use this for initialization
    void Awake()
    {
        text = GetComponent<Text>();
        score = 00;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "SCORE: " + score;
    }
}
