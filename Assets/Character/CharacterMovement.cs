using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public MovementController controller;

    private float moveX = 0f;
    private float runSpeed = 40f;
    private bool jump = false;
    private bool crouch = false;            


    void Update ()
    {
        moveX = Input.GetAxis("Horizontal") * runSpeed;        //Press A-Key = -1, Press D-Key = 1
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }
    }
    void FixedUpdate ()
    {
        controller.Move(moveX * Time.fixedDeltaTime, false, jump);
        jump = false;
        crouch = false;
    }
}
