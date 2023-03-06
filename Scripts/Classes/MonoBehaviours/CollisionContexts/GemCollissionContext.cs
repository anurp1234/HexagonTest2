using UnityEngine;
/* 
 * A gem collision context will have information specific to the collision of the gem, here
 * we only have a score value to be updated, however there can be particle effects, audioclips etc
 * tagged here which can be used when the collision is being processed
 */

public class GemCollissionContext : MonoBehaviour, ICollisionContext
{
    [SerializeField]
    int pScoreIncrement;

    public GameObject particleEffect;
    public int scoreIncrement
    {
        get
        {
            return pScoreIncrement;
        }
    }
}
