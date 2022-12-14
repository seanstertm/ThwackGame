using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public class ColorAttribute : MonoBehaviour
    {
        private ThemeManager theme;
        [SerializeField] private ColorType colorType;
        [SerializeField] private Color offset;
        [SerializeField] private bool Camera;
        private Camera cam;
        private SpriteRenderer spriteRenderer;

        private bool lastCheckFlag;
        void Start()
        {
            theme = GameObject.FindGameObjectWithTag("Theme").GetComponent<ThemeManager>();
            if(Camera)
            {
                cam = gameObject.GetComponent<Camera>();
                cam.backgroundColor = ColorGet();
            } 
            else
            {
                spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.color = ColorGet();
            }

            lastCheckFlag = theme.colorChangeFlag;
        }

        void Update()
        {
            if(lastCheckFlag != theme.colorChangeFlag)
            {
                if (Camera)
                {
                    cam.backgroundColor = ColorGet();
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
                ColorType.Background => 
                theme.
                CurrentTheme.
                Background,
                ColorType.Accent => theme.CurrentTheme.Accent,
                ColorType.Ball => theme.CurrentTheme.Ball,
                ColorType.Blocks => theme.CurrentTheme.Blocks[0],
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