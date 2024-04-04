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

        //����
        [SerializeField] private Worm worm;
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private TextMeshProUGUI cantMoveTextUI;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Image fadeImage;

        private string cantMoveText = "Can't Move Animore...";

        private bool isGameover = false;
        private bool isWin = false;
        public bool isPlay = false;

        //�̹� ����
        private int point;

        //����ü �ð��� �ӵ�
        private float waitTimer = 3f;
        //private float turnSpeed = 3f;

        //����Ÿ�̸�
        [SerializeField] private float timer = 60f; //1��

        public void ResetTimer()
        {
            timer = 60f;
        }

        protected override void Start()
        {
            //�ʱ�ȭ
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
            //1�� Ÿ�̸�
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
            //������
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
            //�̰�����
            if (isWin == true)
            {
                isPlay = false;
                isGameover = true;
                isWin = true;


                point = (int)timer * 3;
                ScoreSaveManager.Instance.SetNewScore(point);
                StartCoroutine(LoadWQScene(GameClear));
            }

            //ġƮŰ
            if (Input.GetKeyUp(KeyCode.G))
            {
                isWin = true;
            }
        }

        //���ӿ���
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

        //TEXT Ÿ���ο���
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

        //�̱���
        public void Reset()
        {
            Worm.isWormDead = false;
            isGameover = false;
        }
    }
}