using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace WiggleQuest
{
    public class GameManager : MonoBehaviour
    {
        //참조
        public Worm worm;
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private TextMeshProUGUI cantMoveTextUI;

        private string sceneOVER = "GameOver";
        private string sceneWIN = "GameClear";
        private string cantMoveText = "Can't Move Animore...";
        [SerializeField] private float waitTimer = 3f;

        private bool isGameover = false;

        private bool isWin = false;

        int point = 800;

        void Update()
        {
            Debug.Log("");
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
                point += 50;
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