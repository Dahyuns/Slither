using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WiggleQuest
{
    public class FadeINOUT : MonoBehaviour
    {
        protected string MainMenu = "MainMenu";
        protected string GameOVER = "GameOver";
        protected string GameClear = "GameClear";
        protected string PlayScene = "PlayScene";

        private Image fadeImage;

        private float turnSpeed = 0.6f;

        public bool isCorutine = false;

        protected virtual void Start()
        {
            fadeImage = GameObject.Find("FadeIm").GetComponent<Image>();
        }

        protected IEnumerator FadeIN(float timer = 0f)
        {
            isCorutine =true;
            float alpha = 1.0f;
            yield return new WaitForSeconds(timer);

            while (alpha > 0)
            {
                alpha -= Time.deltaTime * turnSpeed;
                fadeImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
            isCorutine =false;
        }

        protected IEnumerator LoadWQScene(string sceneName, float timer = 0f)
        {
            StartCoroutine(FadeOUT(timer));
            while (isCorutine)
            {
                yield return null;
            }
            SceneManager.LoadScene(sceneName);
        }

        IEnumerator FadeOUT(float timer)
        {
            isCorutine = true;
            yield return new WaitForSeconds(timer);

            float alpha = 0f;
            while (alpha < 1)
            {
                alpha += Time.deltaTime * turnSpeed;
                fadeImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
            isCorutine = false;
        }
    }
}