using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode {
    public enum MenuLayer
    {
        home,
        paint
    }
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> buttons;
        private readonly List<(Vector3 topRight, Vector3 bottomLeft, MenuButton menuButton)> buttonInfo = new();
        [HideInInspector] public MenuLayer currentLayer = MenuLayer.home;
        private bool pressed = false;

        private MenuManager mainReference;

        public MenuManager Main
        {
            get
            {
                return mainReference;
            }
            private set { }
        }

        private void Start()
        {
            mainReference = gameObject.GetComponent<MenuManager>();

            foreach (GameObject button in buttons)
            {
                (Vector3 topRight, Vector3 bottomLeft) = GetBounds(button);
                MenuButton menuButton = button.GetComponent<MenuButton>();
                buttonInfo.Add((topRight, bottomLeft, menuButton));
            }

        }

        private (Vector3, Vector3) GetBounds(GameObject button)
        {
            return (new Vector3(button.transform.position.x + 0.5f * button.transform.localScale.x, button.transform.position.y + 0.5f * button.transform.localScale.y, 0),
                    new Vector3(button.transform.position.x - 0.5f * button.transform.localScale.x, button.transform.position.y - 0.5f * button.transform.localScale.y, 0));
        }

        private void Update()
        {
            if (GameManager.Main.Game) { return; }

            if (Input.touches.Length != 0)
            {
                if (!pressed)
                {
                    GetButton(Camera.main.ScreenToWorldPoint(Input.touches[0].position));
                    pressed = true;
                }
            }
            else
            {
                pressed = false;
            }
        }

        private void GetButton(Vector3 worldPoint)
        {
            foreach ((Vector3 topRight, Vector3 bottomLeft, MenuButton menuButton) in buttonInfo)
            {
                if (worldPoint.x > bottomLeft.x && worldPoint.x < topRight.x && worldPoint.y > bottomLeft.y && worldPoint.y < topRight.y && menuButton.layer == currentLayer)
                {
                    menuButton.OnPress();
                    return;
                }
            }
            GameManager.Main.BeginGame();
        }
    }
}
