using UnityEngine;

namespace UserCode
{
    public class Ball : MonoBehaviour
    {
        private bool active = false;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (active && collision.name == "Bar")
            {
                gameObject.transform.parent.transform.Find("Launcher").GetComponent<Launcher>().DestroyBall();
                Destroy(gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            active = true;
        }
    }
}
