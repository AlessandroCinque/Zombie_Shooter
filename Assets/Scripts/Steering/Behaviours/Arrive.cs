using UnityEngine;

namespace Steering.Behaviours
{
    public class Arrive : SteeringBehaviour
    {
        private Vector2 _steeringForce;
        
        [SerializeField] private float _seekForce;
        [SerializeField] private float _range;

        [SerializeField] private Transform _target;

        private void Reset()
        {
            _seekForce = 1f;
            _range = 5f;
        }

        public override Vector2 SteeringForce
        {
            get
            {
                var position = transform.position;
                var target = _target ? _target.position : Camera.main.ScreenToWorldPoint(Input.mousePosition);

                return Steering.Arrive(position, target, _seekForce, _range);
            }
        }
    }
}
