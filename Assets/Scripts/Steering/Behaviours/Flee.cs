using UnityEngine;

namespace Steering.Behaviours
{
    public class Flee : SteeringBehaviour
    {
        private Vector2 _steeringForce;
        
        [SerializeField] private float _fleeForce;
        [SerializeField] private float _range;

        [SerializeField] private Transform _target;

        private void Reset()
        {
            _fleeForce = 1f;
            _range = 5f;
        }

        public override Vector2 SteeringForce
        {
            get
            {
                var position = transform.position;
                var target = _target ? _target.position : Camera.main.ScreenToWorldPoint(Input.mousePosition);

                return Steering.Flee(position, target, _fleeForce, _range);
            }
        }
    }
}
