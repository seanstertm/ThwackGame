using UnityEngine;

namespace UserCode
{
    public class ThemeMenuButton : MenuButton
    {
        [SerializeField] private int theme;
        public override void OnPress() => ThemeManager.Main.SetTheme(theme);
    }
}