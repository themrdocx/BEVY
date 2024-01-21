using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private StartFlag defaultRespawnPoint;
    
    private PlayerMovement playerMovement;
    private StartFlag currentRespawnPoint;
    private CameraFade cameraFade;
    private bool isDead;

    public bool IsDead => isDead;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        if (Camera.main != null) cameraFade = Camera.main.gameObject.GetComponent<CameraFade>();
        
        Debug.Assert(cameraFade!=null,"No Camera Fade Found");
        Debug.Assert(defaultRespawnPoint,"DefaultRespawnPointNotSet");

        currentRespawnPoint = defaultRespawnPoint;
    }

    public void SetCurrentRespawnPoint(StartFlag respawn)
    {
        currentRespawnPoint = respawn;
    }

    private void ResetPlayer()
    {
        if (!currentRespawnPoint)
            currentRespawnPoint = defaultRespawnPoint;
        transform.position = currentRespawnPoint.transform.position;
        playerMovement.enabled = true;
    }

    public void KillPlayer()
    {
        cameraFade.ActivateFade();
        cameraFade.OnFadeCompleteCallback += ResetPlayer;
        playerMovement.enabled = false;
    }

    private void OnDisable()
    {
        playerMovement.enabled = false;
    }
}
