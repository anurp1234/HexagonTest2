using System.Collections.Generic;
using UnityEngine;

/*
 * The colllision processor class is responsible for handling collisions by letting specific collision processors
 * handle collisions, it attempts to handle collisions by letting a list of processors handle the collisions.
 * Each processor handles collisions of a certain type of object with others. In this case it is only the player that is 
 * primarily colliding with objects so there is one collision processor, however this can be extended further, following the 
 * open close principle
 */

public class CollisionProcessor : Singleton<CollisionProcessor>
{
    [SerializeField]
    int pframesToSkipOnplayerCollision = 15;

    public int framesToSkipOnplayerCollision
    {
        get
        {
            return pframesToSkipOnplayerCollision;
        }
    }

    public ScoreEvent scoreEvent;

    public SoundEvents soundEvents;

    List<ICollissionProcessor> collisionProcessors;

    int pPlayerCollisionSkipFrames = 0;

    public int playerCollisionSkipFrames
    {
        get
        {
            return pPlayerCollisionSkipFrames;
        }
        set
        {
            pPlayerCollisionSkipFrames = value;
        }
    }




    void Start()
    {
        collisionProcessors = new List<ICollissionProcessor>();
        collisionProcessors.Add(new PlayerCollisionProcessor());
    }
    /*
     * This method attempts to handle collisions by running them through a set of processors, if a collision is handled
     * by any of the processors the method returns
     */

    void Update()
    {
        if (playerCollisionSkipFrames > 0)
        {
            playerCollisionSkipFrames--;
        }
    }
    public void ProcessCollision(ICollisionContext a, ICollisionContext b)
    {
        int processorCount = collisionProcessors.Count;
        for (int i = 0; i < processorCount; i++)
        {
            bool isHandled = collisionProcessors[i].TryProcess(this, a, b);
            if (isHandled)
                return;
        }
    }
}
public interface ICollissionProcessor
{
    bool TryProcess(CollisionProcessor processor, ICollisionContext a, ICollisionContext b);
}

public class PlayerCollisionProcessor : ICollissionProcessor
{
    public bool TryProcess(CollisionProcessor processor,ICollisionContext a, ICollisionContext b)
    {
        if (a is PlayerCollissionContext || b is PlayerCollissionContext)
        {
            ICollisionContext other = a is PlayerCollissionContext ? b : a;
            ICollisionContext playerContext = a is PlayerCollissionContext ? a : b;
            if (other is GemCollissionContext)
            {
                GemCollissionContext gemContext = null;
                gemContext = (GemCollissionContext)other;
                GameObject particleGO = GameObject.Instantiate(gemContext.particleEffect);
                particleGO.transform.position = gemContext.gameObject.transform.position;
                GameObject.Destroy(gemContext.gameObject);
                processor.scoreEvent.Raise(gemContext.scoreIncrement);
                processor.soundEvents.RaiseSFXEvent(SFXType.COLLECT);
            }
            else if (other is ObstacleCollisionContext && processor.playerCollisionSkipFrames == 0)
            {
                processor.playerCollisionSkipFrames = processor.framesToSkipOnplayerCollision;
                PlayerController playerController = ((PlayerCollissionContext)playerContext).controller;
                processor.soundEvents.RaiseSFXEvent(SFXType.PLAYERHIT);
                playerController.PlayHitAnimation();
            }
            else if (other is IgnoreCollisionContext)
            {
                //Nothing to be done here
            }
            else //Here we can handle collision of the player with different other objects
            {
                Debug.Assert(false, "NYI"); 
            }
            return true;
        }
        return false;
    }
}
