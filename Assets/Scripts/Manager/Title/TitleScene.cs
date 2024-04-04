using System.Collections;
using UnityEngine;
using TMPro;

namespace WiggleQuest
{
    public class TitleScene : FadeINOUT
    {
        [SerializeField] private TextMeshProUGUI PressTextUI;

        private string PressText = "Press Anikey";
        private string waitText = "...";

        private float readyTime = 2f;

        private bool isReady;
        private bool isTimerStart;

        protected override void Start()
        {
            base.Start();
            isReady = false;
            isTimerStart = false;
            StartCoroutine(typewriterText());
        }

        void Update()
        {
            if (isReady == false && isTimerStart == false)
            {
                isTimerStart = true;
                StartCoroutine(StartTimer(readyTime));
            }
            if (isReady && Input.anyKeyDown)
            {
                StartCoroutine(LoadWQScene(MainMenu));
            }
        }

        //입력받기 시작 타이머
        IEnumerator StartTimer(float sec)
        {
            yield return new WaitForSeconds(sec);
            isReady = true;
        }
        
        //타이핑연출
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

        //"..." 계속 출력
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