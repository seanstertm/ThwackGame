using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public class ThemeManager : Singleton<ThemeManager>
    {
        public static Theme CurrentTheme { get; private set; } = new Theme(Color.black, Color.black, Color.black, new List<Color>());
        private readonly List<Theme> themes = new();
        public static bool colorChangeFlag = false;

        public void SetTheme(int theme)
        {
            CurrentTheme = themes[theme];
            PlayerPrefs.SetInt("Theme", theme);
            colorChangeFlag = !colorChangeFlag;
        }

        new public void Start()
        {
            base.Start();

            InitList();

            SetTheme(PlayerPrefs.GetInt("Theme"));
        }

        void InitList()
        {
            // Dark mode
            themes.Add(new Theme(
                new Color(0xCF / 255f, 0xCF / 255f, 0xCF / 255f), 
                new Color(0x37 / 255f, 0x37 / 255f, 0x37 / 255f), 
                new Color(0x37 / 255f, 0x37 / 255f, 0x37 / 255f), 
                new List<Color>() {
                    new Color(0x5E / 255f, 0xC2 / 255f, 0xFF / 255f),
                    new Color(0xFF / 255f, 0xC0 / 255f, 0x33 / 255f),
                    new Color(0x35 / 255f, 0xB9 / 255f, 0x5A / 255f),
                    new Color(0xC8 / 255f, 0xFD / 255f, 0xD1 / 255f),
                    new Color(0xE1 / 255f, 0x53 / 255f, 0x53 / 255f),
                    new Color(0x5D / 255f, 0xF3 / 255f, 0xAE / 255f),
                    new Color(0xC2 / 255f, 0x7B / 255f, 0xA0 / 255f),
                    new Color(0x4F / 255f, 0x5C / 255f, 0xDB / 255f),
                    new Color(0.9f, 0.9f, 0.9f)
                }));
            // Light mode
            themes.Add(new Theme(
                new Color(0x37 / 255f, 0x37 / 255f, 0x37 / 255f), 
                Color.white, 
                Color.white,
                new List<Color>() {
                    new Color(0x5E / 255f, 0xC2 / 255f, 0xFF / 255f),
                    new Color(0xFF / 255f, 0xC0 / 255f, 0x33 / 255f),
                    new Color(0x35 / 255f, 0xB9 / 255f, 0x5A / 255f),
                    new Color(0xC8 / 255f, 0xFD / 255f, 0xD1 / 255f),
                    new Color(0xE1 / 255f, 0x53 / 255f, 0x53 / 255f),
                    new Color(0x5D / 255f, 0xF3 / 255f, 0xAE / 255f),
                    new Color(0xC2 / 255f, 0x7B / 255f, 0xA0 / 255f),
                    new Color(0x4F / 255f, 0x5C / 255f, 0xDB / 255f),
                    new Color(0.9f, 0.9f, 0.9f)
                }));
        }
    }

    public class Theme
    {
        public Color Background { get; private set; }
        public Color Accent { get; private set; }
        public Color Ball { get; private set; }
        public List<Color> Blocks { get; private set; }

        public Theme(Color background, Color accent, Color ball, List<Color> blocks)
        {
            Background = background;
            Accent = accent;
            Ball = ball;
            Blocks = blocks;
        }
    }
}
