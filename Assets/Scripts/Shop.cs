using UnityEngine;
using UnityEngine.UI;

namespace Slither
{
    public class Shop : MonoBehaviour
    {
        //���� ��ư [ Shop ]
        public Button ShopButton;

        //������ ������?
        private bool isInShop = false;

        // Ÿ��Ʋ <-> ( �ʵ� <-> ���� )
        //�ʵ����� : ���ӽ���        �� Ÿ��Ʋ����
        //�ʵ����� : ���ӿ���        �� Ÿ��Ʋ��
        //�������� : ���� ��ư Ŭ��  �� �ʵ忡��
        //�������� : ESC            �� �ʵ��
        private void Update()
        {
            //���� �ȿ� ����?
            if (isInShop)
            {
                //���� ������ ������ �ʹ� - ESC
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    //�ʵ�� ���� �ż���
                    GotoPlayGround();
                }
            }
        }

        //�������� �ʵ��
        public void GotoPlayGround()
        {
            Time.timeScale = 1f; //�ٽ� ���
            ShopStatusReverse();
        }

        //��ư Ŭ����
        //�ʵ忡�� ��������
        public void GotoShop()
        {
            Time.timeScale = 0f; //�Ͻ�����
            ShopStatusReverse();
        }

        //���� ���� / ����
        private void ShopStatusReverse()
        {
            ShopButton.enabled = !(ShopButton.enabled);
            ShopButton.image.enabled = !(ShopButton.image.enabled);
            isInShop = !isInShop;
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
