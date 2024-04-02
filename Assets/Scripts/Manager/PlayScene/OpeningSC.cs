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
/*스토리?
나는 개똥지렁이.. 친구가 없네~ //내 친구들은 모두 나비가 되어 출가했다.
나홀로 남아 백수인생 어언 24일째...
내 동생들이 나보다 먼저 나비가 된다면 가족 모두가 내게 실망할거다..(왼쪽위 타이머가 있습니다)
더 이상 지체할 시간이 없다. 먹이를 100개 얻어서 멋진 나비로 변신하게 도와줘!
*/

//안내
//처음구역은 안전지대이다.
//우선 안전지대를 벗어나보자.