using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [Header("CameraShake")] 
    [SerializeField] private float intensity;
    [SerializeField] private float dashShakeTime;

    [Space]
    [SerializeField] private StartFlag defaultRespawnPoint;

    private PlayerMovement playerMovement;
    private StartFlag currentRespawnPoint;
    private CinemachineVirtualCamera currentVirtualCam;
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

        playerMovement.Dashed += OnDash;
    }

    private void OnDash()
    {
        StartCoroutine(CameraShake(dashShakeTime));
    }

    private IEnumerator CameraShake(float shakeTime)
    {
        if (currentVirtualCam)
        {
            var cameraPerlin = currentVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cameraPerlin.m_AmplitudeGain = intensity;
            
            yield return new WaitForSeconds(shakeTime);
            
            cameraPerlin.m_AmplitudeGain = 0f;
        }
        else
        {
            yield return null;
        }
    }

    public void OnRoomSwitch(StartFlag respawn, CinemachineVirtualCamera virtualCamera)
    {
        currentRespawnPoint = respawn;
        currentVirtualCam = virtualCamera;
    }

    private void ResetPlayer()
    {
        if (!currentRespawnPoint)
            currentRespawnPoint = defaultRespawnPoint;
        transform.position = currentRespawnPoint.transform.position;
        playerMovement.enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    public void KillPlayer()
    {
        cameraFade.ActivateFade();
        cameraFade.OnFadeCompleteCallback += ResetPlayer;
        playerMovement.enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    private void OnDisable()
    {
        playerMovement.enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        playerMovement.Dashed -= OnDash;
    }
}
