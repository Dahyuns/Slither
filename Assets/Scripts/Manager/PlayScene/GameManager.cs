using System.Collections;
using UnityEngine;
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

        private string cantMoveText = "Can't Move Animore...";

        private bool isGameover = false;
        private bool isWin = false;

        public bool isPlay = false; //OpeningSC에서 쓰임

        //이번 점수
        private int point;

        //씬교체 시간및 속도
        private float waitTimer = 3f;
        //private float turnSpeed = 3f;

        //게임타이머
        private float timer; //1분
        [SerializeField] private float timeSet = 200f;

        public void ResetTimer()
        {
            timer = timeSet;
        }

        private void Awake()
        {
            timer = timeSet;
            StartCoroutine(FadeIN());

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

            //이벤트 추가
            ResetGame.Reset += Reset;
        }


        private void Reset()
        {
            Worm.isWormDead = false; 
            isGameover = false;
            ResetTimer();
        }

        void Update()
        {
            if (Worm.Level >= 100)
            {
                isWin = true;
            }
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


                worm.DeadWorm();
                GameOverUI.SetActive(true);
                waitTimer = Mathf.Clamp(waitTimer, 1f, 8f);

                StartCoroutine(typewriterText());
                StartCoroutine(LoadWQScene(GameOVER, waitTimer)); //?
                return;
            }
            //이겼을때
            if (isWin == true && isCorutine == false)
            {
                isPlay = false;
                isWin = false;

                //점수 : 남은시간 * 4 + 레벨 * 2
                point = (int)((timer) * 4) + (Worm.Level * 2);
                Debug.Log(point);
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
        //public void Reset()
    }
}