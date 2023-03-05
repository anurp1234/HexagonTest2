using UnityEngine;

public class PlayerCollissionContext : MonoBehaviour, ICollisionContext
{
    public void ProcessCollision(ICollisionContext other)
    {
       //If a player needs to do something after a collision, like a collect animation, it can be done here
    }
}
