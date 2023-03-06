using System;
using UnityEngine;

/*
 The game session class will hold global state variables of the game
In this case there is only one state variable of the score
The score update event is fired on collisions which the session listens to
in turn it Raises another event with the total score. This can be listened on by UI elements which
need to display the total score
*/
public class GameSession : MonoBehaviour, IEventListener
{
    [SerializeField]
    ScoreEvent onScoreUpdate;

    [SerializeField]
    ScoreEvent onTotalScoreUpdate;

    [SerializeField]
    SoundEvents soundEvent;

    [SerializeField]
    int celebrationScore = 800;

    [SerializeField]
    GameObject confettiParticle;

    [SerializeField]
    float confettiOffset = 2;

    int totalScore;

    bool isCelebrated;

    public PlayerController controller;
    

    public void OnEnable()
    {
        onScoreUpdate.RegisterEventListener(this);
    }

    public void OnDisable()
    {
        onScoreUpdate.UnRegisterEventListener(this);
    }

    public void OnEventRaised(int score)
    {
        totalScore += score;
        onTotalScoreUpdate.Raise(totalScore);
        if(totalScore >= celebrationScore && !isCelebrated)
        {
            isCelebrated = true;
            soundEvent.RaiseSFXEvent(SFXType.CELEBRATION);
            controller.PlayCelebrationAnim();
            CreateConfetti();
        }
    }

     void CreateConfetti()
    {
        GameObject confetti = GameObject.Instantiate(confettiParticle);
        confetti.transform.position = controller.transform.position + new Vector3(0, confettiOffset, 0);
    }
}
