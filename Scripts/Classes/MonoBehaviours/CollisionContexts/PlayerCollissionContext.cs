using UnityEngine;

public class PlayerCollissionContext : MonoBehaviour, ICollisionContext
{
    [SerializeField]
    PlayerController pController;

    public PlayerController controller
    {
        get 
        {
            return pController;
        }
    }

    //If a player needs to do something after a collision, like a collect animation, it can be done here
    public void ProcessCollision(ICollisionContext other)
    {
        if (other is ObstacleCollisionContext)
        {
            pController.PlayHitAnimation();
        }
        
       
    }
}
