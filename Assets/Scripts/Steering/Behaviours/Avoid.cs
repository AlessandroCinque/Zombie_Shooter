using UnityEngine;

namespace Steering.Behaviours
{
    public class Avoid : SteeringBehaviour
    {
        [SerializeField] private float _avoidForce = 5f;
        [SerializeField, Range(1f, 20f)] private float _range = 10f;
    
        [SerializeField] private Collider2D _bodyCollider;
        [SerializeField] private ContactFilter2D _contactFilter;
        [SerializeField] private Transform _facingTransform;
    
        private readonly RaycastHit2D[] _hits = new RaycastHit2D[10]; 
    
        public override Vector2 SteeringForce
        {
            get
            {
                // find out if we hit anything
                var numHits = _bodyCollider.Cast(_facingTransform.up, _contactFilter, _hits, _range);
                if (numHits <= 0) return Vector2.zero;
            
                // get the closest obstacle
                var target = _hits[0].point;
                var distance = Vector2.Distance(transform.position, target);
                for (var i = 1; i < numHits; i++)
                {
                    var newDistance = Vector2.Distance(transform.position, _hits[i].point);
                    if (!(newDistance < distance)) continue;
                    
                    Debug.LogWarning("The first element in the hit array was not the closest");
                    distance = newDistance;
                    target = _hits[i].point;
                }

                return Steering.Avoid(transform.position, transform.right, target, _avoidForce, _range);
            }
        }
    }
}
