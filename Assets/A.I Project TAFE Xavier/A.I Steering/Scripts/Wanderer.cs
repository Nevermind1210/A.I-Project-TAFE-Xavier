using UnityEngine;

namespace Wandering
{
    public class Wanderer : MonoBehaviour
    {
        [SerializeField, Range(.01f, .1f)] private float jitter = .05f;
        [SerializeField, Min(1f)] private float speed = 1;
        [SerializeField] private float smoothing = .1f;

        //The force driving the agent. Updates every frame
        private Vector3 currentForce = Vector3.zero;
        //The speed the smoothe dposition is traveling
        private Vector3 velocity = Vector3.zero;

        // Update is called once per frame
        void Update()
        {
            // Calculate the actual movement that needs to occur and how fast
            Vector3 movement = (transform.forward + (CalculateForce() * speed)) * Time.deltaTime;
            Vector3 position = Vector3.SmoothDamp(
                transform.position,
                transform.position + movement,
                ref velocity,
                smoothing);

            // Calculate the rotation from where we are looking to the new one
            Quaternion rotation = Quaternion.Slerp(
                transform.localRotation,
                Quaternion.LookRotation(currentForce),
                Time.deltaTime);

            transform.position = position;
            transform.rotation = rotation;
        }


        /// <summary>
        /// Calculate the force the agent should be moving by. Uses jitter to 
        /// create the wandering effect.
        /// </summary>
        /// <returns></returns>
        private Vector3 CalculateForce()
        {
            //First copy the current force and calculate the random offset using jitter
            Vector3 FORCE = currentForce;
            Vector2 OFFSET = new Vector2(Random.Range(-jitter, jitter), Random.Range(-jitter, jitter));

            //Ad the offset to the horizontal and vertical axis of the transform
            FORCE += transform.right * OFFSET.x;
            FORCE += transform.up * OFFSET.y;

            // Make sure the force is normalised because it is a direction
            FORCE.Normalize();

            // Update the current force with the caluclated one and return it
            currentForce = FORCE;
            return FORCE;

        }
    }
}

