using UnityEngine;

namespace Steering
{
    public static class Steering
    {
        private static readonly AnimationCurve Linear = AnimationCurve.Linear(0, 0, 1, 1);
        
        public static Vector2 Seek(Vector2 position, Vector2 target, float seekForce)
        {
            return (target - position).normalized * seekForce;
        }
        
        public static Vector2 Flee(Vector2 position, Vector2 target, float seekForce)
        {
            return (position - target).normalized * seekForce;
        }
        
        public static Vector2 Flee(Vector2 position, Vector2 target, float seekForce, float range)
        {
            var distance = (target - position).magnitude;
            var factor = 1 - Mathf.Min(distance / range, 1);
            
            return (position - target).normalized * seekForce * factor;
        }

        public static Vector2 Arrive(Vector2 position, Vector2 target, float seekForce, float range)
        {
            return Arrive(position, target, seekForce, range, Linear);
        }
        
        public static Vector2 Arrive(Vector2 position, Vector2 target, float seekForce, float range, AnimationCurve curve)
        {
            if (range <= 0f) return Vector2.zero;
            
            var desiredVelocity = target - position;
            var sqrDistance = desiredVelocity.sqrMagnitude;
            var factor = curve.Evaluate(Mathf.Min(sqrDistance / (range * range), 1));
            
            return desiredVelocity.normalized * seekForce * factor;
        }

        public static Vector2 Avoid(Vector2 position, Vector2 right, Vector2 target, float avoidForce, float range)
        {
            // get the vector from the current position to the obstacle
            var distance = (target - position).magnitude;
            var direction = (target - position).normalized;

            // get the perpendicular vector of the oncoming obstacle
            var left = new Vector2(-direction.y, direction.x);

            // get the sign based on whether the object is to the left (-1) or the right (1)?
            var leftRightSign = Mathf.Sign(Vector2.Dot(right, direction));

            // get the amount we should avoid based on the distance
            var factor = 1f - Mathf.Min(distance, range) / range;

            // use the sign to apply the force in the given direction
            return left * leftRightSign * factor * avoidForce;
        }
    }
}