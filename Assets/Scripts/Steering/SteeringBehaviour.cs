using UnityEngine;

namespace Steering
{
    public abstract class SteeringBehaviour : MonoBehaviour
    {
        public abstract Vector2 SteeringForce { get; }
    }
}