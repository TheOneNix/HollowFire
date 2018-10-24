using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour {

    private float m_JumpForce = 2000f;                           // Amount of force added when the player jumps.
    private float m_MovementSmoothing = .01f;                   // How much to smooth out the movement
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask whatIsGround;                           // A mask determining what is ground to the character
    [SerializeField] private Transform m_CeilingCheck;                           // A position marking where to check for ceilings
    [SerializeField] private float m_minShrinking = 0.6f;
    [SerializeField] private float gravity = 2.0f;
    private bool onGround;
    private float groundRadius = 0.2f;
    private Rigidbody2D rigbody2D;
    private Vector3 velocity = Vector3.zero;
    private int sizeState = 0;                                // 0 = Normal Size, 1 = Small Size, 2 = is Shrinking, 3 = is growing

    private void Awake()
    {
        rigbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigbody2D.AddForce(Physics.gravity * (gravity * rigbody2D.mass * rigbody2D.mass));
    }

    public void Move(float move, bool jumpKeyPressed)
    {   
        
        
        if (jumpKeyPressed && isOnGround())
        {
            rigbody2D.AddForce(new Vector2(rigbody2D.velocity.x, m_JumpForce));
        }
        
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, rigbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        rigbody2D.velocity = Vector3.SmoothDamp(rigbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);
            
    }

    public bool isOnGround()
    {
        if(rigbody2D.velocity.y <=0)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoint.position, groundRadius, whatIsGround);
            for(int i = 0; i < colliders.Length; i++)
            {
                if( colliders[i].gameObject != gameObject)          //check if colliders game Object is different from the player
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void Resize (bool resize, float time)
    {   /*      TODO
        if (sizeState == 1)                         // While being small, check if the character can grow
        {
            if (Physics2D.OverlapBox(m_CeilingCheck.position, m_Size, whatIsGround))
                resize = false;
        }
        */
        if (resize || sizeState == 2 || sizeState == 3)
        {
            Vector3 theScale = transform.localScale;

            if (sizeState == 0)                     //Normal Size t- start shrinking status
                sizeState = 2;
            else if (sizeState == 1)                //Small Size - start growing status
                sizeState = 3;


            if(sizeState == 2)
            {
                theScale.x = theScale.x - (m_minShrinking  * time);
                theScale.y = theScale.y - (m_minShrinking  * time);
                transform.localScale = theScale;
                if (theScale.x <= m_minShrinking)
                    sizeState = 1;

            } else if (sizeState == 3)
            {
                theScale.x = theScale.x + (m_minShrinking  * time);
                theScale.y = theScale.y + (m_minShrinking  * time);
                transform.localScale = theScale;
                if (theScale.x >= 1)
                    sizeState = 0;
            }
        }

    }
}