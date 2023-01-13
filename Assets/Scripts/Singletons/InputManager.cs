using UnityEngine;

namespace UserCode
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private void Update()
        {
            if (Input.touches.Length == 0)
            {
                spriteRenderer.enabled = false;
                return;
            }

            spriteRenderer.enabled = true;
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        }
    }
}