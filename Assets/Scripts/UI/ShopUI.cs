using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace WiggleQuest
{
    //�߰�����
    //���ִ��� Ȯ��

    public class ShopUI : MonoBehaviour
    {
        //����
        public Worm worm;
        public GameObject ShopButton; //���� ��ư [ Shop ]
        public GameObject ShopMenu; //���� UI
        public GameObject LockImage;

        //���� - �뷱�������κ�
        [SerializeField] private int[] priceHeart = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
        [SerializeField] private int[] priceGold  = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
        [SerializeField] private int[] priceSpeed = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
        [SerializeField] private int[] priceDef   = { 100, 200, 300 };
        [SerializeField] private int PriceShop = 50;

        private bool shopButtonOn = true;

        private void Update()
        {
            if (Worm.isWormDead)
            {
                ShopButton.GetComponent<Button>().interactable = false;
                return;
            }

            //���� �� ���� ������
            if (Worm.Gold < PriceShop && shopButtonOn == true)
            {
                ShopButton.GetComponent<Button>().interactable = false;
                LockImage.SetActive(true);
                shopButtonOn = false;
            } // ������
            else if (Worm.Gold >= PriceShop && shopButtonOn == false)
            {
                ShopButton.GetComponent<Button>().interactable = true;
                LockImage.SetActive(false);
                shopButtonOn = true;
            }

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

        #region Shop inout
        // Ÿ��Ʋ <-> ( �ʵ� <-> ���� )
        //�ʵ����� : ���ӽ���        �� Ÿ��Ʋ����
        //�ʵ����� : ���ӿ���        �� Ÿ��Ʋ��
        //�������� : ���� ��ư Ŭ��  �� �ʵ忡��
        //�������� : ESC            �� �ʵ��

        //������ ������?
        private bool isInShop = false;

        //Back��ư Ŭ���� �� ESC input�� //�������� �ʵ��
        public void GotoPlayGround()
        {
            Time.timeScale = 1f; //�ٽ� ���
            ShopStatusReverse();
        }

        //Shop��ư Ŭ���� //�ʵ忡�� ��������
        public void GotoShop()
        {
            Time.timeScale = 0f; //�Ͻ�����
            ShopStatusReverse();
        }

        //���� ���� / ����
        private void ShopStatusReverse()
        {
            /*1 ��ư���� ����������
            ShopButton.enabled = !(ShopButton.enabled);
            ShopButton.image.enabled = !(ShopButton.image.enabled);*/
            //2 ���� ������Ʈ�� ����������
            ShopButton.SetActive(isInShop);

            isInShop = !isInShop; //�ݴ�� �۾�
            ShopMenu.SetActive(isInShop);
            /*foreach (GameObject each in EveryInShop)
            {
                each.SetActive(isInShop);
            }*/
        }
        #endregion

        //�����߰�Ȯ�� ����
        public void PurchaseHeart()
        {
            if (worm.SubtractGold(priceHeart[Worm.HeartLv]))
            {
                worm.AddLv(AddPercent.Heart);
                Debug.Log("HP ����");
                return;
            }

            Debug.Log("!! HP ���� �Ұ�");
        }

        //����߰�Ȯ�� ����
        public void PurchaseGold()
        {
            if (worm.SubtractGold(priceGold[Worm.GoldLv]))
            {
                worm.AddLv(AddPercent.Gold);
                Debug.Log("Gold ����");
                return;
            }

            Debug.Log("!! Gold ���� �Ұ�");
        }

        //���ǵ��߰�Ȯ�� ����
        public void PurchaseSpeed()
        {
            if (worm.SubtractGold(priceSpeed[Worm.SpeedLv]))
            {
                worm.AddLv(AddPercent.Speed);
                Debug.Log("���ǵ� ����");
                return;
            }

            Debug.Log("!! ���ǵ� ���� �Ұ�");
        }

        //���� ����
        public void PurchaseDef()
        {
            //1,2,3���� ���� �����ʿ�
            if (worm.SubtractGold(priceDef[Worm.DefLv]))
            {
                worm.AddLv(AddPercent.Def);
                Debug.Log("���� ����");
                return;
            }

            Debug.Log("!! ���� ���� �Ұ�");
        }

        void OnPoint(InputValue value)
        {
            PriceTextControl.mousePoint = value.Get<Vector2>();
        }
    }
}
