using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode {
    public class ColorCycle : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.HSVToRGB(Random.Range(0, 360) / 360f, 0.47f, 0.74f);
        }
    }
}