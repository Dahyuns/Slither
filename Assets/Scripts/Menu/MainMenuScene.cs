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

        //종료
        public void BExit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중일 때
#else
        Application.Quit(); // 빌드된 런타임에서 실행 중일 때
#endif
        }
    }
}