using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public class GuideBallMovement : MonoBehaviour
    {
        private Launcher launcher;
        private float timeAlive = 0;

        private void Start()
        {
            launcher = transform.parent.parent.GetComponent<Launcher>();
        }
        private void Update()
        {
            timeAlive += Time.deltaTime;
        }
        private void LateUpdate()
        {
            transform.position = SimulateBounces(3 * timeAlive);
        }

        private Vector2 SimulateBounces(float distance)
        {
            Vector2 resultPosition = launcher.InitialLaunch;

            Vector2 rayDirection = launcher.DeltaTouch.normalized;

            // Rework to Box Cast

            RaycastHit2D hit = Physics2D.CircleCast(resultPosition, 0.3f, rayDirection, distance, LayerMask.GetMask("Walls"));

            if(hit.distance == 0 && hit.collider!= null)
            {
                return resultPosition;
            }

            int preventPermaLoop = 0;

            while (hit.collider != null && preventPermaLoop < 10)
            {
                resultPosition = hit.centroid;
                distance -= hit.distance;

                rayDirection = Quaternion.FromToRotation(-rayDirection, hit.normal) * hit.normal;
                hit = Physics2D.CircleCast(resultPosition + rayDirection * 0.1f, 0.3f, rayDirection, distance, LayerMask.GetMask("Walls"));
                preventPermaLoop++;
            }

            return resultPosition + rayDirection * distance;
        }
    }
}