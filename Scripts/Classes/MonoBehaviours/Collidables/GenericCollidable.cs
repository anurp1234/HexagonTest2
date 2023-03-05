using UnityEngine;

public class GenericCollidable : MonoBehaviour, ICollidable
{
    ICollisionContext collisionContext;
    public ICollisionContext Context
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
        collisionContext  = GetComponent<ICollisionContext>();
        Debug.Assert(collisionContext != null, "Object should have collision context");
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        ICollisionContext otherContext = collisionInfo.gameObject.GetComponent<ICollisionContext>();
        Debug.Assert(otherContext != null, "Collliding object does not have a collision context");
        CollisionProcessor.instance.ProcessCollision(collisionContext, otherContext);
    }
}
