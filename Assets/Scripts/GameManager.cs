using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WiggleQuest
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private string GameOver = "GameOver";
        [SerializeField] private string cantMoveText = "Can't Move Animore...";
        [SerializeField] private float waitTimer = 3f;

        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private TextMeshProUGUI cantMoveTextUI;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.O))
            {
                StartCoroutine(ToGameOver());
            }
        }

        IEnumerator ToGameOver()
        {
            GameOverUI.SetActive(true);


            yield return new WaitForSeconds(waitTimer);
            SceneManager.LoadScene(GameOver);
        }

        void typewriterText()
        {
            cantMoveTextUI.text = "";

            char[] chars = cantMoveText.ToCharArray();

            float timePerPiece = chars.Length / waitTimer;

            foreach (char c in chars)
            {
                
            }




        }
    }
}