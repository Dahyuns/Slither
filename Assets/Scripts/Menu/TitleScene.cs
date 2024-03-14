using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WiggleQuest
{
    public class TitleScene : MonoBehaviour
    {
        [SerializeField] private string MainMenu = "MainMenu";
        [SerializeField] private float readyTime = 2f;
        private bool isReady = false;
        private bool isTimerStart = false;


        [SerializeField] private TextMeshProUGUI PressTextUI;
        private string PressText = "Press Anikey";
        private string waitText = "...";

        private void Start()
        {
            StartCoroutine(typewriterText());
        }

        void Update()
        {
            if (isReady == false && isTimerStart == false)
            {
                StartCoroutine(StartTimer(readyTime));
            }
            else if (isReady && Input.anyKeyDown)
            {
                SceneManager.LoadScene(MainMenu);
            }
        }

        IEnumerator StartTimer(float sec)
        {
            yield return new WaitForSeconds(sec);
            isReady = true;
        }
        
        //TEXT 타이핑연출 + ...계속되게 가능?
        IEnumerator typewriterText()
        {
            PressTextUI.text = "";

            char[] chars = PressText.ToCharArray();

            float timePerPiece = (readyTime - 1) / chars.Length;

            for (int c = 0; c < chars.Length; c++)
            {
                yield return new WaitForSeconds(timePerPiece);

                PressTextUI.text += chars[c];
            }
            StartCoroutine(WaitingText(PressTextUI));
        }

        IEnumerator WaitingText(TextMeshProUGUI textUI)
        {
            string tempText = textUI.text;
            char[] chars = waitText.ToCharArray();

            while (true)
            {
                for (int c = 0; c < chars.Length; c++)
                {
                    PressTextUI.text += chars[c];
                    yield return new WaitForSeconds(0.5f);
                }
                PressTextUI.text = tempText;
            }
        }
    }
}