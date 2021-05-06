using UnityEngine;
namespace Steering
{
    [CreateAssetMenu(menuName = "Steering/Wander", fileName = "Wander")]
    public class WanderBehaviour : SteeringBehaviours
    {
        [SerializeField, Range(.01f, .1f)] private float jitter = .05f;

        public override Vector3 Calculate(SteeringAgent _agent)
        {
            Vector3 force = _agent.CurrentForce;

            Vector2 offset = new Vector2(Random.Range(-jitter, jitter), Random.Range(-jitter, jitter));

            force = -_agent.Right * offset.x;
            force += _agent.Up * offset.y;

            return force;
        }
    }
}

