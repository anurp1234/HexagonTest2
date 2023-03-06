using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
    [SerializeField]
    SoundEvents soundEvent;
    [SerializeField]
    float speed = 3;
    [SerializeField]
    float rotationSpeed = 30;

    CharacterController controller;
    Animator animator;
    float movementX;
    float movementY;

    bool isMoving = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    //This needs to be decoupled into a separate input event pub sub model
    //This is not following single responsibility yet due to time constraints
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
        animator.SetBool("isRunning", movementY > 0);
        
        if (movementY > 0 && !isMoving)
        {
            isMoving = true;
            soundEvent.RaiseFootStepsEvent();
        }
        else if(movementY == 0)
        {
            isMoving = false;
            soundEvent.RaiseFootStepsStopEvent();
        }
    }

    void Update()
    {
        transform.Rotate(0, movementX * Time.deltaTime * rotationSpeed, 0);
        if (movementY > 0)
            controller.Move(transform.forward * speed * Time.deltaTime * movementY);
    }

    public void PlayHitAnimation()
    {
        animator.SetBool("IsHit", true);
    }
}
