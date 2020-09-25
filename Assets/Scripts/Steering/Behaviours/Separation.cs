using UnityEngine;

namespace Steering.Behaviours
{
    public class Separation : SteeringBehaviour
    {
        [SerializeField] private Group _group;
        [SerializeField] private float _separationForce = 1;
        [SerializeField] private float _separationRange = 1;

        public override Vector2 SteeringForce 
        {
            get
            {
                if (_group)
                {
                    var members = _group.Members;

                    if (members.Count > 0)
                    {
                        var position = transform.position;
                        var count = 1;

                        foreach (var member in members)
                        {
                            var memberTransform = member.transform;

                            if (memberTransform)
                            {
                                position += memberTransform.position;
                                count++;
                            }
                        }

                        position /= count;

                        return Steering.Flee(transform.position, position, _separationForce, _separationRange);
                    }
                }
                
                return Vector2.zero;
            }
        }
    }
}
