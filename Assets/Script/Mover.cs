using UnityEngine;
using System;
using UnityEngine.EventSystems;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;
    protected Vector3 pos;
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    

    protected virtual void UpdateMotor(Vector3 input)
    {
        pos = transform.localScale;
        //Rest moveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        //Swap sprite direction 
        // for axis X
        if (moveDelta.x > 0)
            transform.localScale = new Vector3(Math.Abs(pos.x), Math.Abs(pos.y), pos.z);
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-(Math.Abs(pos.x)), pos.y, pos.z);
        // Add a push vector, if any
        moveDelta += pushDirection;

        // Reduce the push every frame

        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // Check if we can move 
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor"));
        if (hit.collider == null)
        {
            // Make the player move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }


        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor"));
        if (hit.collider == null)
        {
            // Make the player move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

    protected virtual void FixedUpdated()
    {

    }
}
