using UnityEngine;
using UnityEngine.UI;

namespace Slither
{
    public class Shop : MonoBehaviour
    {
        public Button Button;

        private bool isInShop = false;

        private void Update()
        {
            if (isInShop)
            {
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    GotoPlayGround();
                }
            }
        }

        public void GotoPlayGround()
        {
        }
            public void GotoShop()
        {
            Time.timeScale = 0f;
            Button.enabled = false;
            isInShop = true;
        }

        //방어력 구매
        public void PurchaseDef()
        {

        }

        //먹이퍼센트 구매
        public void PurchaseHeartP()
        {

        }

        //골드퍼센트 구매
        public void PurchaseGoldP()
        {

        }

    }
}
