using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType
{
    game,
    paint
}

public class MenuInput : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttons;

    private void Start()
    {
        foreach(GameObject button in buttons)
        {
            // get the id from button id script
            // get boudning box store to tuple list
        }
    }

    private void Update()
    {
        if(Input.touches.Length != 0)
        {
            ButtonType pressedButton = GetButton(Camera.main.ScreenToWorldPoint(Input.touches[0].position));
            Debug.Log(pressedButton);
        }
    }

    private ButtonType GetButton(Vector3 worldPoint)
    {
        return ButtonType.game;
    }
}
