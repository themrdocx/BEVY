using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50.0f;
    [SerializeField] private float floatingSpeed = 1.0f;
    [SerializeField] private float resetTime = 2f;
    [SerializeField] private Color inactiveColor = Color.gray;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    protected bool isCollected = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
            defaultColor = spriteRenderer.color;
    }

    private void Update()
    {
        /*float newY = Mathf.Sin(Time.time * floatingSpeed) * 0.05f;
        transform.position = new Vector2(transform.position.x, transform.position.y +newY);*/
            
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && !isCollected)
        {
            Collect(other.gameObject);
        }
    }

    protected virtual void Collect(GameObject collidedObject)
    {
        StartCoroutine(ResetCollectible());
    }
    
    private IEnumerator ResetCollectible()
    {
        if (spriteRenderer)
        {
            spriteRenderer.color = inactiveColor;
        }
        
        isCollected = true;
        yield return new WaitForSeconds(resetTime);
        isCollected = false;
        
        if (spriteRenderer)
        {
            spriteRenderer.color = defaultColor;
        }
    }
}
