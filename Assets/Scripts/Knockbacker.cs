using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Knockbacker : MonoBehaviour
{
    [SerializeField] private float knockBackForce = 5f;
    private void OnCollisionEnter2D(Collision2D other) { var playerMovement = other.gameObject.GetComponent<PlayerMovement>(); if (playerMovement) { Vector2 knockBackDir = (other.transform.position - transform.position).normalized; playerMovement.ApplyKnockback(knockBackDir, knockBackForce); } }
}
