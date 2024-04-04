using System.Collections;
using TMPro;
using UnityEngine;

namespace WiggleQuest
{
    public class OpeningSC : MonoBehaviour
    {
        public TextMeshProUGUI sqText;
        public TextMeshProUGUI clickText;
        [SerializeField] private string ObjectiveMs = "100���� �޼��ϼ���!";

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
            sqText.text = ObjectiveMs;

            isSquance = true;
            yield return null;
        }
    }
}