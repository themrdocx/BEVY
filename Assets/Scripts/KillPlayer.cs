using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player && !player.IsDead)
            player.KillPlayer();
    }
}
