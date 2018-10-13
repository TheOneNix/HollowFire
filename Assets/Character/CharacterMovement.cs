using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float speed;
    private Rigidbody2D rBody;
    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)][SerializeField]private float m_MovementSmoothing = .05f;




    // Use this for initialization
    void Start () {
        rBody = GetComponent<Rigidbody2D>();
	}
	
    void FixedUpdate()
    {
        // Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(speed* 10f, rBody.velocity.y);
			// And then smoothing it out and applying it to the character
			rBody.velocity = Vector3.SmoothDamp(rBody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        
    }
}
