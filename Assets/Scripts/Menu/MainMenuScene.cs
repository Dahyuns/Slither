using UnityEngine;

namespace WiggleQuest
{
    public class MainMenuScene : MonoBehaviour
    {
        public void BNewGame()
        {
            Debug.Log("NewGame");
        }

        public void BContinue()
        {
            Debug.Log("Continue");
        }

        public void BExit()
        {
            Debug.Log("Exit");
        }
    }
}