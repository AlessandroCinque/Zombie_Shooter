using System.Collections.Generic;
using UnityEngine;

namespace Steering.Vehicles
{
    //[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class Humanoid : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
    
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private CircleCollider2D _collider;
    
        [SerializeField] private Transform _view;
    
        private Vector2 _steeringForce;

        [SerializeField] private List<SteeringBehaviour> steeringBehaviours;

        private void Reset()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CircleCollider2D>();
            
            _view = transform.Find("View");

            if (_rigidbody)
            {
                _rigidbody.gravityScale = 0f;
                _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            }
        }
    
        private void Update()
        {
            _steeringForce = Vector2.zero;
        
            foreach (var steeringBehaviour in steeringBehaviours)
            {
                _steeringForce += steeringBehaviour.SteeringForce;
            }
            // this is for make the character turn around to face its destination. Not needed
            //if (_rigidbody.velocity.sqrMagnitude > 0f) _view.transform.right = _rigidbody.velocity.normalized;
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce(_steeringForce);

            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
        }
    }
}
