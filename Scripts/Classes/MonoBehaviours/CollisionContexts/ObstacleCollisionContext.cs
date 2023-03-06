using UnityEngine;
public class ObstacleCollisionContext : MonoBehaviour, ICollisionContext
{
    public void ProcessCollision(ICollisionContext other)
    {
       //If the obstacle needs to do something after a collision, like get displaced it can be done here
    }
}
