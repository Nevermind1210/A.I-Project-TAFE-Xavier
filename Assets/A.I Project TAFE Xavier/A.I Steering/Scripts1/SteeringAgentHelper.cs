using System.Collections.Generic;
using UnityEngine;


namespace Steering
{
    public static class SteeringAgentHelper
    {
        const int viewDirections = 100;

        public static readonly Vector3[] directions;
        private static Vector3[] coneDirections = null;

        // Default parameters are paramters that don't need to speicifally be passed in, 
        // if they aren't, the set value will be used, otherwise the one passed in will be.
        // Default parameters also MUST be at the end of the parameter list.
        public static Vector3[] DirectionsInCone(SteeringAgent _agent, bool _forceRecalculate = false)
        {
            //Determined if this function has't been run before
            if(coneDirections == null || _forceRecalculate) 
            {
                List<Vector3> newDirections = new List<Vector3>();
                // Loop through every direction that has already been caluclated in the sphere
                foreach (Vector3  direction in directions)
                {
                    // Calculate the angle between the forward of the agent
                    // and this direction... if it is less than the view angle, we can add
                    // it to the list
                    if(Vector3.Angle(direction, _agent.Forward) < _agent.ViewAngle)
                    {
                        newDirections.Add(direction);
                    }
                }

                coneDirections = newDirections.ToArray();
            }

            return coneDirections;
        }

        static SteeringAgentHelper()
        {
            directions = new Vector3[viewDirections];

            float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
            float angleIncrement = Mathf.PI * 2 * goldenRatio;

            for (int i = 0; i < viewDirections; i++)
            {
                float t = (float)i / viewDirections;
                float inclination = Mathf.Acos(1 - 2 * t);
                float azimuth = angleIncrement * i;

                float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
                float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
                float z = Mathf.Cos(inclination);

                directions[i] = new Vector3(x, y, z);
            }
        }
    }
}
