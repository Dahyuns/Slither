using System.Collections;
using TMPro;
using UnityEngine;

namespace WiggleQuest
{
    public class OpeningSC : MonoBehaviour
    {
        public TextMeshProUGUI sqText;
        public TextMeshProUGUI clickText;
        [SerializeField] private string ObjectiveMs = "100♡를 달성하세요!";

        private bool isSquance = false;

        void Awake()
        {
            StartCoroutine(ShowObjective());
        }

        private void Update()
        {
            if (isSquance && Input.anyKeyDown)
            {
                isSquance = false;
                //일시정지 해제
                Time.timeScale = 1f;
                //텍스트 제거
                sqText.text = "";
                clickText.text = "";


                //게임 타이머 리셋
                GameManager.Instance.ResetTimer();
                //플레이중으로 변경
                GameManager.Instance.isPlay = true;
            }
        }

        IEnumerator ShowObjective()
        {
            //FadeIN중에는 재생X
            while (GameManager.Instance.isCorutine)
            {
                yield return null;
            }

            //일시정지
            Time.timeScale = 0f;
            //텍스트 생성 (목표메세지 적용)
            sqText.text = ObjectiveMs;

            isSquance = true;
            yield return null;
        }
    }
}