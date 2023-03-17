using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public class PaintMenuButton : MenuButton
    {
        [SerializeField] private GameObject paintMenu;
        public override void OnPress()
        {
            MenuManager.Main.currentLayer = MenuLayer.paint;
            paintMenu.SetActive(true);
            paintMenu.GetComponent<Animator>().Play("PaintMenuEnter");
        }
    }
}