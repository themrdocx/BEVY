using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PlayerMovement),typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private List<AudioClip> landSounds;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip jumpSound;
    
    private PlayerMovement player;
    private AudioSource audioSource;
    

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        player.Landed += OnPlayerLand;
        player.Dashed += OnPlayerDash;
        player.Jumped += OnPlayerJump;
    }


    private void OnPlayerLand()
    {
        audioSource.PlayOneShot(landSounds[Random.Range(0,landSounds.Count)]);
    }

    private void OnPlayerDash()
    {
       audioSource.PlayOneShot(dashSound);
    }

    private void OnPlayerJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }
    private void OnDisable()
    {
        player.Landed -= OnPlayerLand;
        player.Dashed -= OnPlayerDash;
        player.Jumped -= OnPlayerJump;
    }
}
