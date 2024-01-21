using UnityEngine;

public class DashItem : Collectible
{
    protected override void Collect(GameObject collidedObject)
    {
        var playerMovement = collidedObject.GetComponent<PlayerMovement>();
        
        if(playerMovement.IncrementDash(1))
            base.Collect(collidedObject);

    }
}
