using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayloadManager : MonoBehaviour
{
    public float velocity;
    [Tooltip("1 = Full Speed, 0 = None")]
    public float velocityScale;

    void FixedUpdate()
    {
        transform.Translate(new Vector3(0, (velocity * velocityScale) / (1.0f/Time.deltaTime), 0));
    }
}
