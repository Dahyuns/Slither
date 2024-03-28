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
            Debug.Log("NewGame");
        }

        public void BContinue()
        {
            Debug.Log("Continue");
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