using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public class PaintMenuButton : MenuButton
    {
        public override void OnPress()
        {
            Debug.Log("Paint button pressed");

            ThemeManager.Main.SetTheme(0);
        }
    }
}