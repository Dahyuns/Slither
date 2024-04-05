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
            StartCoroutine(LoadWQScene(PlayScene));
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

        public void BMenu()
        {
            StartCoroutine(LoadWQScene(MainMenu));
        }
    }
}