using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public class GameManager : Singleton<GameManager>
    {
        public bool Game = false;

        [Header("Stats")]
        public float paddleArmWidth = 1;
        public float foresight = 1;
        public float numBalls = 1;
        public float bounces = 1;

        [Header("Guide Ball Options")]
        public float guideBallSpeed = 3;
        public float guideBallGap = 1;

        [Header("")]
        [SerializeField] private GameObject borders;
        [SerializeField] private GameObject bar;
        [SerializeField] private GameObject launcher;
        [SerializeField] private GameObject menu;

        new private void Start()
        {
            base.Start();
        }

        public void BeginGame()
        {
            Game = true;
            borders.SetActive(true);
            menu.SetActive(false);
            borders.GetComponent<Animator>().Play("Border Enter");
        }
        // Callback BorderEntered() when animation ends

        public void BorderEntered()
        {
            bar.SetActive(true);
            launcher.SetActive(true);
            BlockManager.Main.GenerateRow(4);
        }
    }
}
