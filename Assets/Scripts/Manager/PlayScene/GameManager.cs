using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Drawing;

namespace WiggleQuest
{
    public class GameManager : MonoBehaviour
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

        private string sceneOVER = "GameOver";
        private string sceneWIN = "GameClear";
        private string cantMoveText = "Can't Move Animore...";
        [SerializeField] private float waitTimer = 3f;

        private bool isGameover = false;
        private bool isWin = false;
        public bool isPlay = false;

        //이번 점수
        private int point;

        //게임타이머
        [SerializeField] private float timer = 60f; //1분

        void Start ()
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
        }

        void Update()
        {
            //1분 타이머
            if (isGameover == false && isPlay == true)
            {
                timerText.text = ((int)timer).ToString("D2");
                timer -= Time.deltaTime;

                if (timer < 0)
                {
                    GameOver();
                }
            }

            //Debug.Log("");
            if(Worm.isWormDead && isGameover == false)
            {
                GameOver();
                return;
            }

            if (Input.GetKeyUp(KeyCode.G))
            {
                isWin = true;
            }

            if (isWin == true)
            {
                isPlay = false;
                point = (int)timer * 3;
                ScoreSaveManager.Instance.SetNewScore(point);
                StartCoroutine(GotoScene(sceneWIN));
            }
            
        }

        //게임오버
        void GameOver()
        {
            isGameover = true;

            worm.DeadWorm();
            GameOverUI.SetActive(true);
            waitTimer = Mathf.Clamp(waitTimer, 1f, 8f);

            StartCoroutine(typewriterText());
            StartCoroutine(GotoScene(sceneOVER,waitTimer));
        }

        //해당씬으로 이동
        IEnumerator GotoScene(string sceneName, float timer = 0f)
        {
            isWin = false;
            yield return new WaitForSeconds(timer);
            SceneManager.LoadScene(sceneName);
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

        public void Reset()
        {
            Worm.isWormDead = false;
            isGameover = false;
        }
    }
}