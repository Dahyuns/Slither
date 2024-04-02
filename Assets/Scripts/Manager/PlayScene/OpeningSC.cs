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
            Time.timeScale = 0f;
            StartCoroutine(ShowObjective());
        }

        private void Update()
        {
            if (isSquance && Input.anyKeyDown)
            {
                Time.timeScale = 1f;
                sqText.text = "";
                clickText.text = "";
                isSquance = false;
                GameManager.Instance.isPlay = true;
            }
        }

        IEnumerator ShowObjective()
        {
            isSquance = true;
            sqText.text = ObjectiveMs;
            yield return null;
        }
    }
}
/*���丮?
���� ����������.. ģ���� ����~ //�� ģ������ ��� ���� �Ǿ� �Ⱑ�ߴ�.
��Ȧ�� ���� ����λ� ��� 24��°...
�� �������� ������ ���� ���� �ȴٸ� ���� ��ΰ� ���� �Ǹ��ҰŴ�..(������ Ÿ�̸Ӱ� �ֽ��ϴ�)
�� �̻� ��ü�� �ð��� ����. ���̸� 100�� �� ���� ����� �����ϰ� ������!
*/

//�ȳ�
//ó�������� ���������̴�.
//�켱 �������븦 �������.