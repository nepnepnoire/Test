using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<PlayerController>().Interact(this);
    }
}
