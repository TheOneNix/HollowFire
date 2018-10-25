using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public MovementController controller;
    private float time = 0f;
    private float runSpeed = 40f;
    private float moveDir = -1;       // true = left, false = right

    void FixedUpdate()
    {
        moveDirection();
    }
    private void moveDirection()
    {
        if (controller.isOnGround() || true)
        {
            time += Time.fixedDeltaTime;
            if (time > 3)
            {
                moveDir *= -1;
                time = 0;
            }
            controller.Move(moveDir * runSpeed * Time.fixedDeltaTime, false);
        }
    }
}