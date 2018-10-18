using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    private Vector3 velocity;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    void FixedUpdate()
    {
        Vector3 playerPos = player.position;
        playerPos.z -= 1;
        playerPos = Vector3.SmoothDamp(transform.position, playerPos, ref velocity, 0.1f);

        transform.position = playerPos;

        //Border for Camera
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
            Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
            Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
    }
}
