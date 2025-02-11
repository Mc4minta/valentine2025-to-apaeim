using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector2 targetPosition = new Vector2(player.position.x,player.position.y);
            transform.position = Vector2.Lerp(transform.position,targetPosition, 
                                                smoothSpeed * Time.deltaTime);
        }
    }
}
