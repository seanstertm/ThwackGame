using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public class GuideBallManager : MonoBehaviour
    {
        [SerializeField] private GameObject ball;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private ThemeManager theme;
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


                if (launcher.DeltaTouch.magnitude < 1)
                {
                    foreach (GameObject guideBall in guideBalls)
                    {
                        guideBall.GetComponent<SpriteRenderer>().color = new Color(theme.CurrentTheme.Ball.r, theme.CurrentTheme.Ball.g, theme.CurrentTheme.Ball.b, 0.2f);
                    }
                }
                else
                {
                    foreach (GameObject guideBall in guideBalls)
                    {
                        guideBall.GetComponent<SpriteRenderer>().color = new Color(theme.CurrentTheme.Ball.r, theme.CurrentTheme.Ball.g, theme.CurrentTheme.Ball.b, 0.5f);
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
            StartCoroutine(BallTrack(guideBall, InterpretForesight(gameManager.foresight)));
        }

        IEnumerator BallTrack(GameObject ball, float distance)
        {
            yield return new WaitForSeconds(distance / 3);
            guideBalls.Remove(ball);
            Destroy(ball);
        }
    }
}
