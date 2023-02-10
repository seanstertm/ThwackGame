using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UserCode
{
    public class ColorAttribute : MonoBehaviour
    {
        [SerializeField] private ColorType colorType;
        [SerializeField] private Color offset;
        [SerializeField] private bool Camera;
        [SerializeField] private bool Text;
        private Camera cam;
        private TextMeshProUGUI text;
        private SpriteRenderer spriteRenderer;

        private bool lastCheckFlag;
        void Start()
        {
            if(Camera)
            {
                cam = gameObject.GetComponent<Camera>();
                cam.backgroundColor = ColorGet();
            } 
            else if(Text)
            {
                text = gameObject.GetComponent<TextMeshProUGUI>();
                text.color = ColorGet();
            }
            else
            {
                spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.color = ColorGet();
            }

            lastCheckFlag = ThemeManager.colorChangeFlag;
        }

        void Update()
        {
            if(lastCheckFlag != ThemeManager.colorChangeFlag)
            {
                if (Camera)
                {
                    cam.backgroundColor = ColorGet();
                }
                else if (Text)
                {
                    text.color = ColorGet();
                }
                else
                {
                    spriteRenderer.color = ColorGet();
                }
                lastCheckFlag = !lastCheckFlag;
            }
        }

        private Color ColorGet()
        {
            return colorType switch
            {
                ColorType.Background => ThemeManager.CurrentTheme.Background,
                ColorType.Accent => ThemeManager.CurrentTheme.Accent,
                ColorType.Ball => ThemeManager.CurrentTheme.Ball,
                ColorType.Blocks => ThemeManager.CurrentTheme.Blocks[0],
                _ => Color.black,
            } + offset;
        }
    }

    public enum ColorType
    {
        Background,
        Accent,
        Ball,
        Blocks
    }
}