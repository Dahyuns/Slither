using System.Collections;
using TMPro;
using UnityEngine;

namespace WiggleQuest
{
    public class OpeningSC : MonoBehaviour
    {
        public TextMeshProUGUI sqText;
        public TextMeshProUGUI clickText;
        [SerializeField] private string objectiveMs = "100���� �޼��ϼ���!"; 
        [SerializeField] private string clickMs = "click to start!";
        private bool isSquance = false;

        void Start()
        {
            StartCoroutine(ShowObjective());
        }

        private void Update()
        {
            //Debug.Log(GameManager.Instance.isCorutine);
            if (isSquance && Input.anyKeyDown)
            {
                isSquance = false;

                //�Ͻ����� ����
                Time.timeScale = 1f;

                //�ؽ�Ʈ ����
                sqText.text = "";
                clickText.text = "";


                //���� Ÿ�̸� ����
                GameManager.Instance.ResetTimer();

                //�÷��������� ����
                GameManager.Instance.isPlay = true;
            }
        }

        IEnumerator ShowObjective()
        {
            //FadeIN�߿��� ���X
            while (GameManager.Instance.isCorutine)
            {
                yield return null;
            }

            //�Ͻ�����
            Time.timeScale = 0f;
            //�ؽ�Ʈ ���� (��ǥ�޼��� ����)
            sqText.text = objectiveMs;
            clickText.text = clickMs;

            isSquance = true;
            yield return null;
        }
    }
}