using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserCode
{
    public enum MenuState
    {
        home,
        game,
        paint
    }
    public class GameManager : MonoBehaviour
    {
        public bool game { get; private set; } = false;
        public MenuState menuState = MenuState.home;
        public int paddleArmWidth = 1;
        public int foresight = 1;
        public int numBalls = 1;
        public int bounces = 1;

        [SerializeField] private GameObject borders;
        [SerializeField] private GameObject bar;
        [SerializeField] private GameObject launcher;
        [SerializeField] private GameObject menu;

        private static GameManager mainReference;

        public void Start()
        {
            mainReference = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        }

        public static GameManager Main 
        {
            get
            {
                return mainReference;
            }
        }

        public void BeginGame()
        {
            menuState = MenuState.game;
            game = true;
            borders.SetActive(true);
            menu.SetActive(false);
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
