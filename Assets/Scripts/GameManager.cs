using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace WiggleQuest
{
    public class GameManager : MonoBehaviour
    {
        //����
        public Worm worm;
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private TextMeshProUGUI cantMoveTextUI;

        private string sceneGO = "GameOver";
        private string cantMoveText = "Can't Move Animore...";
        [SerializeField] private float waitTimer = 3f;

        private bool isGameover = false;


        void Update()
        {
            if(Worm.isWormDead && isGameover == false)
            {
                GameOver();
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
            StartCoroutine(GotoScene(sceneGO,waitTimer));
        }

        //�ش������ �̵�
        IEnumerator GotoScene(string sceneName, float timer = 0f)
        {
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