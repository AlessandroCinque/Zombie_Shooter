using UnityEngine;

namespace Utilities
{
    public static class Vector2Extensions
    {
        public static float angleRadians(this Vector2 vector)
        {
            return Mathf.Atan2(vector.y, vector.x);
        }
        
        public static float angleDegrees(this Vector2 vector)
        {
            return Mathf.Rad2Deg * vector.angleRadians();
        }

        public static Vector2 randomVector(this Vector2 vector, float angleRangeDegrees, float magnitudeRange)
        {
            var angle = vector.angleDegrees();
            angle += Random.Range(angle - angleRangeDegrees, angle + angleRangeDegrees);
            var magnitude = Random.Range(0, magnitudeRange);
            
            return new Vector2(Mathf.Sin(angle) * magnitude, Mathf.Cos(angle) * magnitude);
        }
    }
}
