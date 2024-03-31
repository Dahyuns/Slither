using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSaveManager : MonoBehaviour
{
    private static ScoreSaveManager instance;
    public static ScoreSaveManager Instance
    {  get 
        { 
            return instance; 
        } 
    }

    //����
    public Button resetButton;
    public TextMeshProUGUI[] scorePlace;

    //�ӽ� ����
    private int[] scores;

    //PlayerPrefs Key��
    private string[] strKeys;


    void Start()
    {
        //�ʱ�ȭ
        if (Instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate ScoreSaveManager instance found!");
            Destroy(gameObject);
        }
        
        scores = new int[] { 0, 0, 0, 0 };
        strKeys = new string[] { "strFirst", "strSecond", "strThird", "strFourth"};

        //����Ǿ� �ִ� �� ��������
        DrawScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            DrawScore();
        }
        ButtonSwitch();
    }

    //���ο� ���� ȹ���
    public void SetNewScore(int newValue)
    {
        //��
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

    //Score�ǿ� ���
    private void DrawScore()
    {
        for (int i = 0; i < strKeys.Length; i++)
        {
            //DrawScore
            scorePlace[i].text = PlayerPrefs.GetInt(strKeys[i], 0).ToString();
        }

    }

    //[�ʱ�ȭ ��ư] �Ѱ���� 
    private void ButtonSwitch()
    {
        //���������� �ϳ��� �߰� �ϱ⶧����
        //ù��°�� ������ �ȵǾ������� ��� ���� ��

        //���� ����� ������ �ִٸ�?
        if (PlayerPrefs.HasKey(strKeys[0]))
        {
            //�ʱ�ȭ ��ư ON
            resetButton.interactable = true;
        }
        else //���ٸ� off
        {
            resetButton.interactable = false;
        }
    }

    //[�ʱ�ȭ ��ư] ���
    public void ResetScore()
    {
        //���ﶩ ���� ���� (�ϳ���X)
        PlayerPrefs.DeleteAll();
        DrawScore();
    }
}
