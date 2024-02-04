using UnityEngine;

public class WinCollectible : Collectible
{
    protected override void Collect(GameObject collidedObject)
    {
        GameManager.Instance.GameOver();
    }
}
