using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public MovementController controller;
    private float time = 0f;
    private float runSpeed = 40f;
    private bool moveDir = true;       // true = left, false = right

    void FixedUpdate()
    {

        moveDirection();
    }
    private void moveDirection()
    {
        if (controller.isOnGround() || true)
        {
            if (time <= 3)
            {
                time += Time.fixedDeltaTime;
                Debug.Log(time);
            }
            else if (time > 3)
            {
                Debug.Log(moveDir);
                moveDir = !moveDir;
                time = 0;
            }
            if (moveDir)
                controller.Move(runSpeed * Time.fixedDeltaTime, false);
            else if (!moveDir)
                controller.Move(-runSpeed * Time.fixedDeltaTime, false);
        }
    }
}