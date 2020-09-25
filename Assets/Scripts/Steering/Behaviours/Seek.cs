using UnityEngine;

namespace Steering.Behaviours
{
    public class Seek : SteeringBehaviour
    {
        private Vector2 _steeringForce;
        
        [SerializeField] private float _seekForce;

        [SerializeField] private Transform _target;
        
        private Vector2? _staticTarget;

        private void Reset()
        {
            _seekForce = 1f;
        }

        public override Vector2 SteeringForce
        {
            get
            {
                if (_target)
                {
                    return Steering.Seek(transform.position, _target.position, _seekForce);
                }

                if (_staticTarget.HasValue)
                {
                    return Steering.Seek(transform.position, _staticTarget.Value, _seekForce);
                }

                return Vector2.zero;
            }
        }

        public Transform Target
        {
            get => _target;
            set
            {
                _staticTarget = null;
                _target = value;
            }
        }

        public Vector2? StaticTarget
        {
            get => _staticTarget;
            set
            {
                _target = null;
                _staticTarget = value;
            }
        }
    }
}
