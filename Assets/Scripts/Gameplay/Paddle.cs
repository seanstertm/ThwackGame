using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UserCode
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private GameObject leftBar;
        [SerializeField] private GameObject bar;
        [SerializeField] private GameObject rightBar;
        [SerializeField] private BoxCollider2D bc;
        [SerializeField] private TextMeshProUGUI text;
        private int lifetime;
        public void Activate()
        {
            bc.enabled = false;
            lifetime = GameManager.Main.bounces;
            text.text = lifetime.ToString();
            gameObject.transform.position = new Vector3(Clamp(gameObject.transform.position.x, -3.5f + GameManager.Main.paddleArmWidth, 3.5f - GameManager.Main.paddleArmWidth), bar.transform.position.y, 0);
            StartCoroutine(FadeIn());
            StartCoroutine(PaddleGrow());
            SetPaddleSize(GameManager.Main.paddleArmWidth);
        }
        public void Deactivate()
        {
            StartCoroutine(FadeOut());
        }

        public void Update()
        {
            if (Input.touches.Length == 0) return;
            gameObject.transform.position = new Vector3(Clamp(Camera.main.ScreenToWorldPoint(Input.touches[0].position).x, -3.5f + GameManager.Main.paddleArmWidth, 3.5f - GameManager.Main.paddleArmWidth), bar.transform.position.y, 0);
        }

        public void SetPaddleSize(float scale)
        {
            bc.size = new Vector2(2 * scale + 1, 0.37f);
            leftBar.transform.localPosition = new Vector3(-scale / 2 - 0.45f, 0, -1);
            leftBar.transform.localScale = new Vector3(scale, 2, 1);
            rightBar.transform.localPosition = new Vector3(scale / 2 + 0.45f, 0, -1);
            rightBar.transform.localScale = new Vector3(scale, 2, 1);
        }

        private float Clamp(float x, float lowerBound, float upperBound)
        {
            if (x > upperBound) return upperBound;
            if (x < lowerBound) return lowerBound;
            return x;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            lifetime--;
            text.text = lifetime.ToString();
            if(lifetime == 0)
            {
                text.text = "";
                Deactivate();
            }
        }

        IEnumerator FadeIn()
        {
            SpriteRenderer[] sps = { gameObject.GetComponent<SpriteRenderer>(), leftBar.GetComponent<SpriteRenderer>(), rightBar.GetComponent<SpriteRenderer>(), };
            for (float opacity = 0; opacity < 1; opacity += Time.deltaTime * 10)
            {
                foreach(SpriteRenderer sp in sps)
                {
                    sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, opacity);
                }
                yield return null;
            }
            foreach(SpriteRenderer sp in sps)
            {
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1.0f);
            }
        }

        IEnumerator FadeOut()
        {
            SpriteRenderer[] sps = { gameObject.GetComponent<SpriteRenderer>(), leftBar.GetComponent<SpriteRenderer>(), rightBar.GetComponent<SpriteRenderer>() };
            for (float opacity = 1; opacity > 0; opacity -= Time.deltaTime * 10)
            {
                foreach (SpriteRenderer sp in sps)
                {
                    sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, opacity);
                }
                yield return null;
            }
            foreach (SpriteRenderer sp in sps)
            {
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0.0f);
            }
            gameObject.transform.localPosition = new Vector3(0, 0, 0);
            gameObject.SetActive(false);
        }

        IEnumerator PaddleGrow()
        {
            for (float size = 0; size < GameManager.Main.paddleArmWidth; size += Time.deltaTime * 5)
            {
                SetPaddleSize(size);
                yield return null;
            }
            bc.enabled = true;
            SetPaddleSize(GameManager.Main.paddleArmWidth);
        }
    }
}