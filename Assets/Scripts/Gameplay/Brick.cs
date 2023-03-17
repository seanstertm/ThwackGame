using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UserCode
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private Animator x;
        private int count;

        public void Blink()
        {
            x.Play("BrickXBlink");
        }

        public void SetText(int number)
        {
            count = number;
            text.text = number.ToString();
            sr.color = ThemeManager.CurrentTheme.Blocks[number - 1];
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.name != "Ball") { return; }
            count--;
            if(count == 0)
            {
                BlockManager.Main.RemoveGameObject(gameObject);
                return;
            }
            SetText(count);
        }
    }
}