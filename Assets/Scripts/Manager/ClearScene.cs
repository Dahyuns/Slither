using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WiggleQuest
{
    public class ClearScene : MonoBehaviour
    {
        string SceneMenu = "MainMenu";

        private void Awake()
        {
            StartCoroutine(GotoScene(SceneMenu, 5f));
        }

        //해당씬으로 이동
        IEnumerator GotoScene(string sceneName, float timer = 0f)
        {
            yield return new WaitForSeconds(timer);
            SceneManager.LoadScene(sceneName);
        }
    }
}