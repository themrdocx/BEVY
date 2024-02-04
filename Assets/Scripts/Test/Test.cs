using System;
using UnityEngine;

namespace Test
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private Vector2 bounds;
        [SerializeField] private Vector2 ball;
        [SerializeField] private Vector2 startVel;
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position,bounds);    
            
            
            Gizmos.DrawWireSphere(ball,radius);
            
        }
    }
}