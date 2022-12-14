using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public class GameManager : MonoBehaviour
    {
        public bool game { get; private set; } = false;
        public int paddleArmWidth = 1;
        public int foresight = 1;
        public int numBalls = 1;
        public int bounces = 1;

        [SerializeField] private GameObject borders;
        [SerializeField] private GameObject bar;
        [SerializeField] private GameObject launcher;

        public void BeginGame()
        {
            game = true;
            borders.SetActive(true);
            borders.GetComponent<Animator>().Play("Border Enter");
        }
        // Callback BorderEntered() when animation ends

        public void BorderEntered()
        {
            bar.SetActive(true);
            launcher.SetActive(true);
        }
    }
}
