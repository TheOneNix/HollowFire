using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour {

    private float m_JumpForce = 1000f;                           // Amount of force added when the player jumps.
    private float m_MovementSmoothing = .01f;                   // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                          // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                           // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                            // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                           // A position marking where to check for ceilings
    [SerializeField] private float m_minShrinking = 0.6f;

    const float k_GroundedRadius = .2f;                         // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;                                    // Whether or not the player is grounded.
    //const float k_CeilingRadius = .2f;                        
    private Vector2 m_Size = new Vector2(1.611345f, 1.867575f);
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    private int sizeState = 0;                                // 0 = Normal Size, 1 = Small Size, 2 = is Shrinking, 3 = is growing

    [Header("Events")] [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable] public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            
        }
        if (m_Grounded && jump)
        {
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }
    public void Resize (bool resize, float time)
    {
        // While being small, check if the character can grow
        if (sizeState == 1)
        {
            if (Physics2D.OverlapBox(m_CeilingCheck.position, m_Size, m_WhatIsGround))
                resize = false;
        }
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