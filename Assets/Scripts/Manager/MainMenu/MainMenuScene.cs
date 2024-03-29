using UnityEngine;
using UnityEngine.SceneManagement;

namespace WiggleQuest
{
    public class MainMenuScene : MonoBehaviour
    {
        private string scenePlay = "PlayScene";
        //private string sceneTitle = "Title";

        public void BNewGame()
        {
            SceneManager.LoadScene(scenePlay);
            //GameManager.Reset();
        }

        public void BContinue()
        {

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