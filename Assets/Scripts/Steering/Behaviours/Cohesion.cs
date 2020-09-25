using UnityEngine;

namespace Steering.Behaviours
{
    public class Cohesion : SteeringBehaviour
    {
        [SerializeField] private Group _group;
        [SerializeField] private float _cohesionForce = 1;

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

                        return Steering.Seek(transform.position, position, _cohesionForce);
                    }
                }
                
                return Vector2.zero;
            }
        }
    }
}
