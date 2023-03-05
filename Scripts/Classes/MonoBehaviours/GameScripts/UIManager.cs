using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour, IEventListener
{
    [SerializeField]
    string scorePrefix = "Score: ";

    [SerializeField]
    TMP_Text scoreText;

    [SerializeField]
    ScoreEvent onTotalScoreUpdate;

    public void OnEnable()
    {
        onTotalScoreUpdate.RegisterEventListener(this);
    }

    public void OnDisable()
    {
        onTotalScoreUpdate.UnRegisterEventListener(this);
    }
    public void OnEventRaised(int totalScore)
    {
        scoreText.text = scorePrefix + totalScore.ToString();
    }
}
