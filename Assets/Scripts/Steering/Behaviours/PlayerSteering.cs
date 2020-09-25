using UnityEngine;

namespace Steering.Behaviours
{
    public class PlayerSteering : SteeringBehaviour
    {
        [SerializeField] private float _inputForce = 5.0f;

        public override Vector2 SteeringForce 
        {
            get
            {
                var inputX = Input.GetAxis("Horizontal");
                var inputY = Input.GetAxis("Vertical");
                var input = new Vector2(inputX, inputY);
                
                if (input.sqrMagnitude < 0.1f * 0.1f) return Vector2.zero;
                
                return input.normalized * _inputForce;
            }
        }
    }
}
