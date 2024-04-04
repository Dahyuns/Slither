using UnityEngine;
using UnityEngine.SceneManagement;

namespace WiggleQuest
{
    public class MainMenuScene : FadeINOUT
    {
        protected override void Start()
        {
            base.Start();
            StartCoroutine(FadeIN());
        }

        public void BNewGame()
        {
            SceneManager.LoadScene(PlayScene);
        }

        //����
        public void BExit()
        {
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ��� ���� ���� ��
            #else
                    Application.Quit(); // ����� ��Ÿ�ӿ��� ���� ���� ��
            #endif
        }
    }
}