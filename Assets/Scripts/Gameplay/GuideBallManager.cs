using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public class GuideBallManager : MonoBehaviour
    {
        [SerializeField] private GameObject ball;
        private readonly List<GameObject> guideBalls = new();
        private Launcher launcher;
        private float time = 0.2f;

        private void Start()
        {
            launcher = gameObject.transform.parent.gameObject.GetComponent<Launcher>();
        }
        private void Update()
        {
            if (launcher.Held)
            {
                if (time > 0.2f)
                {
                    time -= 0.2f;
                    LaunchGuideBall();
                }
                time += Time.deltaTime;

                if (!launcher.validTouch)
                {
                    foreach (GameObject guideBall in guideBalls)
                    {
                        SpriteRenderer spriteRenderer = guideBall.GetComponent<SpriteRenderer>();
                        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
                    }
                }
                else
                {
                    foreach (GameObject guideBall in guideBalls)
                    {
                        SpriteRenderer spriteRenderer = guideBall.GetComponent<SpriteRenderer>();
                        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
                    }
                }
            }
            else
            {
                if (guideBalls.Count > 0)
                {
                    foreach (GameObject guideBall in guideBalls)
                    {
                        Destroy(guideBall);
                    }
                    guideBalls.Clear();
                }
                time = 0;
            }
        }

        private float InterpretForesight(int stat)
        {
            return stat * 0.75f + 1;
        }

        private void LaunchGuideBall()
        {
            GameObject guideBall = Instantiate(ball);
            guideBalls.Add(guideBall);
            guideBall.name = "Guide Ball";
            guideBall.transform.parent = gameObject.transform;
            guideBall.transform.position = gameObject.transform.position;
            StartCoroutine(BallTrack(guideBall, InterpretForesight(GameManager.Main.foresight)));
        }

        IEnumerator BallTrack(GameObject ball, float distance)
        {
            yield return new WaitForSeconds(distance / 3);
            guideBalls.Remove(ball);
            Destroy(ball);
        }
    }
}
