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

        //����
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

        //�̹� ����
        private int point;

        //����Ÿ�̸�
        [SerializeField] private float timer = 60f; //1��

        void Start ()
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
        }

        void Update()
        {
            //1�� Ÿ�̸�
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

        //���ӿ���
        void GameOver()
        {
            isGameover = true;

            worm.DeadWorm();
            GameOverUI.SetActive(true);
            waitTimer = Mathf.Clamp(waitTimer, 1f, 8f);

            StartCoroutine(typewriterText());
            StartCoroutine(GotoScene(sceneOVER,waitTimer));
        }

        //�ش������ �̵�
        IEnumerator GotoScene(string sceneName, float timer = 0f)
        {
            isWin = false;
            yield return new WaitForSeconds(timer);
            SceneManager.LoadScene(sceneName);
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

        public void Reset()
        {
            Worm.isWormDead = false;
            isGameover = false;
        }
    }
}