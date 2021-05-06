using UnityEngine;

using System;
using System.Collections.Generic;

namespace Steering
{
    [CreateAssetMenu(menuName = "Steering/Compsite", fileName = "Composite", order = -100)]
    public class CompositeBehaviour : SteeringBehaviours
    {   
        [Serializable] public struct WeightedBehaviour
        {
            [Min(.1f)] public float weighting;
            public SteeringBehaviours behaviours;
        }

        [SerializeField] private List<WeightedBehaviour> behaviours = new List<WeightedBehaviour>();
        public override Vector3 Calculate(SteeringAgent _agent)
        {
            Vector3 force = _agent.CurrentForce;

            behaviours.ForEach(weighted =>
            {
                force += weighted.behaviours.Calculate(_agent) * weighted.weighting;
            });

            return force;
        }    
    }
}