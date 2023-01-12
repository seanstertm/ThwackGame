using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public abstract class MenuButton : MonoBehaviour
    {
        public abstract void OnPress();

        public MenuLayer layer;
    }
}