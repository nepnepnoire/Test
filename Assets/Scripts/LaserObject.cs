using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("You Take 1 Damagew!");
        }
    }
}
