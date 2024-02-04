using UnityEngine;

public class WinCollectible : Collectible
{
    protected override void Collect(GameObject collidedObject)
    {
        isCollected = true;
        CameraFade fade = Camera.main.GetComponent<CameraFade>();
        fade.ActivateFade();
        fade.OnFadeCompleteCallback += GameOver;
    }

    private void GameOver()
    {
        CameraFade fade = Camera.main.GetComponent<CameraFade>();
        fade.OnFadeCompleteCallback -= GameOver;
        GameManager.Instance.GameOver();
    }
}
