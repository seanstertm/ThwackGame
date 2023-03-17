using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public class GuideBallMovement : MonoBehaviour
    {
        private Launcher launcher;
        public float distanceTravelled = 0;

        private void Start()
        {
            launcher = transform.parent.parent.GetComponent<Launcher>();
        }
        private void Update()
        {
            if (distanceTravelled > GameManager.Main.foresight)
            {
                distanceTravelled = 0;
            }

            distanceTravelled += Time.deltaTime * GameManager.Main.guideBallSpeed;
        }
        private void LateUpdate()
        {
            transform.position = SimulateBounces(distanceTravelled);
        }

        private Vector2 SimulateBounces(float distance)
        {
            Vector2 resultPosition = launcher.InitialLaunch;

            Vector2 rayDirection = launcher.DeltaTouch.normalized;

            RaycastHit2D hit = Physics2D.BoxCast(resultPosition, new Vector2(0.6f, 0.6f), 0, rayDirection, distance, LayerMask.GetMask("Walls"));

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
                hit = Physics2D.BoxCast(resultPosition + rayDirection * 0.1f, new Vector2(0.6f, 0.6f), 0, rayDirection, distance, LayerMask.GetMask("Walls"));
                preventPermaLoop++;
            }

            return resultPosition + rayDirection * distance;
        }
    }
}