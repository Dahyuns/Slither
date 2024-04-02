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

        //�ش������ �̵�
        IEnumerator GotoScene(string sceneName, float timer = 0f)
        {
            yield return new WaitForSeconds(timer);
            SceneManager.LoadScene(sceneName);
        }
    }
}