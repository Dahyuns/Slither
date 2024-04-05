using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WiggleQuest
{
    public class ScoreSaveManager : MonoBehaviour
    {
        private static ScoreSaveManager instance;
        public static ScoreSaveManager Instance
        {
            get
            {
                return instance;
            }
        }

        //참조
        private Button resetButton;
        private TextMeshProUGUI[] scorePlace;

        //임시 저장
        private int[] scores;
        
        private static int countInstance = 0;

        //PlayerPrefs Key값
        private string[] strKeys;

        void Awake()
        {
            //초기화
            if (Instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogWarning("Duplicate ScoreSaveManager instance found!");
                Destroy(gameObject);
            }

            // 씬 이동시 파괴금지
            DontDestroyOnLoad(gameObject);


            //초기화
            resetButton = GameObject.Find("ResetB").GetComponent<Button>();
            scorePlace = GameObject.Find("Board").GetComponentsInChildren<TextMeshProUGUI>();

            strKeys = new string[] { "strFirst", "strSecond", "strThird", "strFourth" };
            scores = new int[] { 0, 0, 0, 0 };
            for(int i = 0; i < scores.Length; i++)
            {
                scores[i] = PlayerPrefs.GetInt(strKeys[i], 0);
            }

            countInstance++;



            //저장되어 있는 값 가져오기
            DrawScore();
        }

        // Update is called once per frame
        void Update()
        {
            if (resetButton != null)
            {
                if (Input.GetKeyDown(KeyCode.H))
                {
                    DrawScore();
                }

                ButtonSwitch();
            }
        }

        //새로운 점수 획득시
        public void SetNewScore(int newValue)
        {
            //비교
            for (int i = scores.Length - 1; i >= 0; i--)
            {
                if (scores[i] < newValue)
                {
                    int tmp = scores[i];
                    scores[i] = newValue;
                    if (i < scores.Length - 1)
                    {
                        scores[i + 1] = tmp;
                    }
                }
                else
                {
                    break;
                }
            }

            //SaveScore
            for (int i = 0; i < strKeys.Length; i++)
            {
                PlayerPrefs.SetInt(strKeys[i], scores[i]);
            }

            //DrawScore
            DrawScore();
        }

        //Score판에 등록
        private void DrawScore()
        {
            for (int i = 0; i < strKeys.Length; i++)
            {
                //DrawScore
                scorePlace[i].text = PlayerPrefs.GetInt(strKeys[i], 0).ToString();
            }

        }

        //[초기화 버튼] 켜고끄기 
        private void ButtonSwitch()
        {
            //위에서부터 하나씩 추가 하기때문에
            //첫번째에 저장이 안되어있으면 모두 없는 것

            //만약 저장된 점수가 있다면?
            if (PlayerPrefs.HasKey(strKeys[0]))
            {
                //초기화 버튼 ON
                resetButton.interactable = true;
            }
            else //없다면 off
            {
                resetButton.interactable = false;
            }
        }

        //[초기화 버튼] 기능
        public void ResetScore()
        {
            //지울땐 전부 지움 (하나씩X)
            PlayerPrefs.DeleteAll();
            DrawScore();
        }
    }
}