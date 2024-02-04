
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform playerHand;
    public float minShootingPower = 5f;
    public float multShootingPower = 4f;
    public float maxShootingPower = 30f;

    private bool isDragging = false;
    private Vector2 startPoint;
    private Vector2 endPoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }

        if (isDragging)
        {
            ContinueDragging();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }
    }

    void StartDragging()
    {
        isDragging = true;
        startPoint = playerHand.position;
    }

    void ContinueDragging()
    {
        endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Shooting Power"+CalculateShootingPower());
    }

    void StopDragging()
    {
        isDragging = false;

        float shootingPower = CalculateShootingPower();
        
        GameObject projectileObject = Instantiate(projectilePrefab, playerHand.position, Quaternion.identity);
        Rigidbody2D projectileRigidbody = projectileObject.GetComponent<Rigidbody2D>();
        
        Debug.Assert(projectileRigidbody!=null,"No Rigidbody Component added to Prfeab!");
        
        Vector2 shootDirection = playerHand.right;
        projectileRigidbody.AddForce(shootDirection * shootingPower, ForceMode2D.Impulse);
    }

    float CalculateShootingPower()
    {
        float distance = Vector2.Distance(startPoint, endPoint);
        float clampedDistance = Mathf.Clamp(Mathf.Abs(distance)*multShootingPower, minShootingPower, maxShootingPower);
        float shootingPower =  clampedDistance;
        
        return shootingPower;
    }
}

