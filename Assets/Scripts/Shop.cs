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

        //���� ����
        public void PurchaseDef()
        {

        }

        //�����ۼ�Ʈ ����
        public void PurchaseHeartP()
        {

        }

        //����ۼ�Ʈ ����
        public void PurchaseGoldP()
        {

        }

    }
}
