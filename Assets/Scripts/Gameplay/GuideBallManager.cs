using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public class GuideBallManager : Singleton<GuideBallManager>
    {
        [SerializeField] private GameObject ball;
        private readonly List<GameObject> guideBalls = new();
        private Launcher launcher;
        private bool firstTimePress = true;

        private new void Start()
        {
            base.Start();
            launcher = gameObject.transform.parent.gameObject.GetComponent<Launcher>();
        }
        private void Update()
        {
            if (!GameManager.Main.Game) return;
            if (launcher.Held)
            {
                if(firstTimePress)
                {
                    CreateGuideBalls();
                    firstTimePress = false;
                }

                if (!launcher.validTouch)
                {
                    SetGuideBallOpacity(0.2f);
                }
                else
                {
                    SetGuideBallOpacity(0.5f);
                }
            }
            else
            {
                DestroyAllGuideBalls();
                firstTimePress = true;
            }
        }

        private void DestroyAllGuideBalls()
        {
            if (guideBalls.Count > 0)
            {
                foreach (GameObject guideBall in guideBalls)
                {
                    Destroy(guideBall);
                }
                guideBalls.Clear();
            }
        }

        private void SetGuideBallOpacity(float opacity)
        {
            foreach (GameObject guideBall in guideBalls)
            {
                SpriteRenderer spriteRenderer = guideBall.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, opacity);
            }
        }

        private void CreateGuideBalls()
        {
            for(int i = 0; i < GameManager.Main.foresight / GameManager.Main.guideBallGap; i++)
            {
                CreateGuideBall(i);
            }
        }

        private void CreateGuideBall(int number)
        {
            GameObject guideBall = Instantiate(ball);
            guideBalls.Add(guideBall);
            guideBall.name = "Guide Ball";
            guideBall.transform.parent = gameObject.transform;
            guideBall.transform.position = gameObject.transform.position;
            guideBall.GetComponent<GuideBallMovement>().distanceTravelled = number * GameManager.Main.guideBallGap;
        }
    }
}
