using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private bool isDead;

    public bool IsDead => isDead;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void ResetPlayer()
    {
        playerMovement.enabled = true;
    }

    public void KillPlayer()
    {
        //Death effects
        playerMovement.enabled = false;
    }

    private void OnDisable()
    {
        playerMovement.enabled = false;
    }
}
