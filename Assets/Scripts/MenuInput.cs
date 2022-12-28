using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType
{
    game,
    paint
}

namespace UserCode {
    public class MenuInput : MonoBehaviour
    {
        [SerializeField] private List<GameObject> buttons;
        private readonly List<(Vector3 topRight, Vector3 bottomLeft, ButtonType)> buttonInfo = new();
        private bool pressed = false;

        private void Start()
        {
            foreach (GameObject button in buttons)
            {
                (Vector3 topRight, Vector3 bottomLeft) = GetBounds(button);
                ButtonType buttonType = button.GetComponent<ButtonID>().buttonType;
                buttonInfo.Add((topRight, bottomLeft, buttonType));
            }

        }

        private (Vector3, Vector3) GetBounds(GameObject button)
        {
            return (new Vector3(button.transform.position.x + 0.5f * button.transform.localScale.x, button.transform.position.y + 0.5f * button.transform.localScale.y, 0),
                    new Vector3(button.transform.position.x - 0.5f * button.transform.localScale.x, button.transform.position.y - 0.5f * button.transform.localScale.y, 0));
        }

        private void Update()
        {
            if (GameManager.Main.menuState != MenuState.home) { return; }

            if (Input.touches.Length != 0)
            {
                if (!pressed)
                {
                    SwitchCallback(GetButton(Camera.main.ScreenToWorldPoint(Input.touches[0].position)));
                    pressed = true;
                }
            }
            else
            {
                pressed = false;
            }
        }

        private ButtonType GetButton(Vector3 worldPoint)
        {
            foreach ((Vector3 topRight, Vector3 bottomLeft, ButtonType buttonType) in buttonInfo)
            {
                if (worldPoint.x > bottomLeft.x && worldPoint.x < topRight.x && worldPoint.y > bottomLeft.y && worldPoint.y < topRight.y)
                {
                    return buttonType;
                }
            }
            return ButtonType.game;
        }

        private void SwitchCallback(ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.game:
                    GameManager.Main.BeginGame();
                    break;
                default:
                    break;
            }
        }
    }
}
