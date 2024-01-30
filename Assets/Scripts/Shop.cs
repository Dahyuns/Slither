using UnityEngine;
using UnityEngine.UI;

namespace Slither
{
    public class Shop : MonoBehaviour
    {
        //���� ��ư [ Shop ]
        public  GameObject ShopButton;
        //���� UI
        public  GameObject ShopMenu;
        //private GameObject[] EveryInShop;

        //������ ������?
        private bool isInShop = false;

        // Ÿ��Ʋ <-> ( �ʵ� <-> ���� )
        //�ʵ����� : ���ӽ���        �� Ÿ��Ʋ����
        //�ʵ����� : ���ӿ���        �� Ÿ��Ʋ��
        //�������� : ���� ��ư Ŭ��  �� �ʵ忡��
        //�������� : ESC            �� �ʵ��
        private void Start()
        {
            //���޴��� ��� ������Ʈ ��������
            //EveryInShop = ShopMenu.GetComponentsInChildren<GameObject>(true);
        }
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
            //1 ��ư���� ����������
            //ShopButton.enabled = !(ShopButton.enabled);
            //ShopButton.image.enabled = !(ShopButton.image.enabled);
            //2 ���� ������Ʈ�� ����������
            ShopButton.SetActive(isInShop);

            isInShop = !isInShop; //��ư�� �ݴ�� �۾�
            ShopMenu.SetActive(isInShop);
            /*foreach (GameObject each in EveryInShop)
            {
                each.SetActive(isInShop);
            }*/

        }

        //���ǵ� ����
        public void PurchaseSpeed()
        {
            Debug.Log("���ǵ� ����");

        }

        //���� ����
        public void PurchaseDef()
        {
            Debug.Log("���� ����");
        }

        //�����ۼ�Ʈ ����
        public void PurchaseHeartP()
        {
            Debug.Log("HP ����");

        }
        
        //����ۼ�Ʈ ����
        public void PurchaseGoldP()
        {

            Debug.Log("Go ����");
        }

    }
}
