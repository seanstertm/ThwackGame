using System.Collections;
using UnityEngine;

namespace UserCode
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] private GameObject bar;
        [SerializeField] private GameObject ball;
        [SerializeField] private GameObject paddle;
        [SerializeField] private Paddle paddleRef;
        private bool touchBuffer = false;
        private Vector2 initialTouch;
        public Vector2 InitialLaunch { get; private set; }
        public Vector2 DeltaTouch { get; private set; }
        public bool Held { get; private set; } = false;
        public uint BallCount { get; private set; } = 0;

        private void Update()
        {
            if (paddle.activeInHierarchy)
            {
                touchBuffer = true;
                return;
            }

            if (Input.touches.Length > 0)
            {
                if (touchBuffer) return;
                if (Held)
                {
                    Vector2 touch = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    DeltaTouch = touch - initialTouch;
                    if (DeltaTouch.y < 0)
                    {
                        DeltaTouch *= -1;
                    }
                }
                else
                {
                    initialTouch = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    InitialLaunch = gameObject.transform.position = new Vector3(Clamp(initialTouch.x, -3.5f, 3.5f), bar.transform.position.y, 0);
                    Held = true;
                    DeltaTouch = new Vector2(0, 0);
                }
            }
            else
            {
                touchBuffer = false;
                if (Held && DeltaTouch.y != 0 && DeltaTouch.magnitude > 1)
                {
                    StartCoroutine(LaunchBalls(DeltaTouch * 3, GameManager.Main.numBalls));
                }
                DeltaTouch = new Vector2(0, 0);
                Held = false;
            }
        }

        private float Clamp(float x, float lowerBound, float upperBound)
        {
            if (x > upperBound) return upperBound;
            if (x < lowerBound) return lowerBound;
            return x;
        }

        private void LaunchBall(Vector2 direction)
        {
            paddle.SetActive(true);
            paddleRef.Activate();
            direction.Normalize();
            BallCount++;
            GameObject newBall = Instantiate(ball, InitialLaunch, Quaternion.identity);
            newBall.transform.parent = gameObject.transform.parent;
            newBall.name = "Ball";
            newBall.GetComponent<Rigidbody2D>().velocity = direction * 9;
        }

        IEnumerator LaunchBalls(Vector2 direction, int count)
        {
            while(count > 0)
            {
                LaunchBall(direction);
                yield return new WaitForSeconds(0.25f);
                count--;
            }
        }

        public void DestroyBall()
        {
            BallCount--;
            if(BallCount == 0)
            {
                paddleRef.Deactivate();
            }
        }
    }
}