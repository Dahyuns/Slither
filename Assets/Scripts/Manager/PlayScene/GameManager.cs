using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WiggleQuest
{
    public class GameManager : FadeINOUT
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                return instance;
            }
        }

        //참조
        [SerializeField] private Worm worm;
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private TextMeshProUGUI cantMoveTextUI;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Image fadeImage;

        private string cantMoveText = "Can't Move Animore...";

        private bool isGameover = false;
        private bool isWin = false;
        public bool isPlay = false;

        //이번 점수
        private int point;

        //씬교체 시간및 속도
        private float waitTimer = 3f;
        //private float turnSpeed = 3f;

        //게임타이머
        [SerializeField] private float timer = 60f; //1분

        public void ResetTimer()
        {
            timer = 60f;
        }

        protected override void Start()
        {
            //초기화
            if (Instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogWarning("Duplicate GameManager instance found!");
                Destroy(gameObject);
            }

            base.Start();
            StartCoroutine(FadeIN());
        }

        void Update()
        {
            //1분 타이머
            if (isGameover == false && isPlay == true)
            {
                timerText.text = ((int)timer).ToString("D2");
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    timer = 0;
                    GameOver();
                }
            }

            //Debug.Log("");
            //졌을때
            if (Worm.isWormDead && isGameover == false)
            {
                isPlay = false;
                isGameover = true;
                isWin = false;


                worm.DeadWorm();
                GameOverUI.SetActive(true);
                waitTimer = Mathf.Clamp(waitTimer, 1f, 8f);

                StartCoroutine(typewriterText());
                StartCoroutine(LoadWQScene(GameOVER, waitTimer)); //?
                return;
            }
            //이겼을때
            if (isWin == true)
            {
                isPlay = false;
                isGameover = true;
                isWin = true;


                point = (int)timer * 3;
                ScoreSaveManager.Instance.SetNewScore(point);
                StartCoroutine(LoadWQScene(GameClear));
            }

            //치트키
            if (Input.GetKeyUp(KeyCode.G))
            {
                isWin = true;
            }
        }

        //게임오버
        void GameOver()
        {
            isPlay = false;
            isGameover = true;
            isWin = false;


            worm.DeadWorm();
            GameOverUI.SetActive(true);
            waitTimer = Mathf.Clamp(waitTimer, 1f, 8f);

            StartCoroutine(typewriterText());
            StartCoroutine(LoadWQScene(GameOVER, waitTimer));
        }

        //TEXT 타이핑연출
        IEnumerator typewriterText()
        {
            cantMoveTextUI.text = "";

            char[] chars = cantMoveText.ToCharArray();

            float timePerPiece = (waitTimer - 1) / chars.Length;

            for (int c = 0; c < chars.Length; c++)
            {
                yield return new WaitForSeconds(timePerPiece);

                cantMoveTextUI.text += chars[c];
            }
        }

        //미구현
        public void Reset()
        {
            Worm.isWormDead = false;
            isGameover = false;
        }
    }
}