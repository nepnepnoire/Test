
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Transform leftBounds;
    public Transform rightBounds;
    public Transform aboveBounds;  // New variable for upper bounds
    public Transform belowBounds;   // New variable for lower bounds

    // Smooth damp settings
    public float smoothDampTime = 0.15f;
    private Vector3 smoothDampVelocity = Vector3.zero;

    private float camWidth, camHeight, levelMinX, levelMaxX, levelMinY, levelMaxY;

    // Use this for initialization
    void Start()
    {
        camHeight = Camera.main.orthographicSize * 2;
        camWidth = camHeight * Camera.main.aspect;

        // Calculate left and right bounds
        float leftBoundsWidth = leftBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        float rightBoundsWidth = rightBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        levelMinX = leftBounds.position.x + leftBoundsWidth + (camWidth / 2);
        levelMaxX = rightBounds.position.x - rightBoundsWidth - (camWidth / 2);

        // Calculate upper and lower bounds
        float aboveBoundsHeight = aboveBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;
        float belowBoundsHeight = belowBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;
        levelMaxY = aboveBounds.position.y - aboveBoundsHeight - (camHeight / 2);
        levelMinY = belowBounds.position.y + belowBoundsHeight + (camHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            // Clamp the target's position within the horizontal bounds
            float targetX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, target.position.x));

            // Clamp the target's position within the vertical bounds
            float targetY = Mathf.Max(levelMinY, Mathf.Min(levelMaxY, target.position.y));

            // Smoothly move the camera's x and y positions
            float x = Mathf.SmoothDamp(transform.position.x, targetX, ref smoothDampVelocity.x, smoothDampTime);
            float y = Mathf.SmoothDamp(transform.position.y, targetY, ref smoothDampVelocity.y, smoothDampTime);

            // Update the camera's position
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
