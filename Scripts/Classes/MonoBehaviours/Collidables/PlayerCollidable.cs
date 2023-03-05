using UnityEngine;

public class PlayerCollidable : MonoBehaviour, ICollidable
{
    ICollisionContext collisionContext;
    public ICollisionContext context
    {
        get
        {
            return collisionContext;
        }
        set
        {
            collisionContext = value;
        }
    }

    void Start()
    {
        collisionContext = GetComponent<PlayerCollissionContext>();
        Debug.Assert(collisionContext != null, "Object should have collision context");
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ICollisionContext otherContext = hit.gameObject.GetComponent<ICollisionContext>();
        Debug.Assert(otherContext != null, "Collliding object does not have a collision context");
        CollisionProcessor.instance.ProcessCollision(collisionContext, otherContext);
    }
}
