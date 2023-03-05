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

    int totalScore;

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
    }
}
