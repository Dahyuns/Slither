using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace WiggleQuest
{
    public class ShopUI : MonoBehaviour
    {
        //����
        [SerializeField] private Worm worm;
        [SerializeField] private GameObject ShopButton; //���� ��ư [ Shop ]
        [SerializeField] private GameObject ShopMenu; //���� UI
        [SerializeField] private GameObject LockImage;

        //���� - �뷱�������κ�
        #region Price
        [SerializeField] 
        private int priceShop = 50;
        public int PriceShop { get { return priceShop; } }

        [SerializeField] private int[] priceHeart = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100 };
        [SerializeField] private int[] priceGold  = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100 };
        [SerializeField] private int[] priceSpeed = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100 };
        [SerializeField] private int[] priceDef   = { 100, 200, 300 };
        public int[] PriceHeart { get { return priceHeart; } }
        public int[] PriceGold  { get { return priceGold; } }
        public int[] PriceSpeed { get { return priceSpeed; } }
        public int[] PriceDef   { get { return priceDef; } }
        #endregion

        private bool shopButtonOn = true;

        private void Update()
        {
            //������ ��ư ���ְ� RETURN
            if (Worm.isWormDead)
            {
                ShopButton.GetComponent<Button>().interactable = false;
                return;
            }

            #region Shop inout

            //���� �� ���� ������
            if (Worm.Gold < priceShop && shopButtonOn == true)
            {
                //��ư ����, LOCK�̹��� �ѱ�
                ShopButton.GetComponent<Button>().interactable = false;
                LockImage.SetActive(true);
                shopButtonOn = false;
            } 
            
            // ������
            else if (Worm.Gold >= priceShop && shopButtonOn == false)
            {
                //��ư �ѱ�, LOCK�̹��� ����
                ShopButton.GetComponent<Button>().interactable = true;
                LockImage.SetActive(false);
                shopButtonOn = true;
            }


            //���� �ȿ� ������, ESCŰ�� ������ ����
            if (isInShop)
            {
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    //�ʵ�� ���� �ż���
                    GotoPlayGround();
                }
            }
            #endregion
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
            if (worm.SubtractGold(priceShop))
            {
                Time.timeScale = 0f; //�Ͻ�����
                ShopStatusReverse();
            }
            else
            {
                Debug.Log("�������ԺҰ���");
            }
        }

        //���� ���� / ����
        private void ShopStatusReverse()
        {
            /*1 ��ư���� ����������
            ShopButton.enabled = !(ShopButton.enabled);
            ShopButton.image.enabled = !(ShopButton.image.enabled);
            foreach (GameObject each in EveryInShop)
            {
                each.SetActive(isInShop);
            }*/
            //2 ���� ������Ʈ�� ����������
            ShopButton.SetActive(isInShop);

            isInShop = !isInShop; //�ݴ�� �۾�
            ShopMenu.SetActive(isInShop);
        }
        #endregion

        #region Purchase in Shop
        //�����߰�Ȯ�� ����
        public void PurchaseHeart()
        {
            //������ �����ɷ������� ũ�ų� ������ return
            if (Worm.HeartLv >= priceHeart.Length)
            {
                Debug.Log("!! HP ���� �Ұ�");
                return;
            }
            //                         ������ ���� 

            //�� �� �ִ� ���� ����
            if (worm.SubtractGold(priceHeart[Worm.HeartLv]))
            {
                worm.AddLv(AddPercent.Heart);
                return;
            }
        }

        //����߰�Ȯ�� ����
        public void PurchaseGold()
        {
            //������ �����ɷ������� ũ�ų� ������ return
            if (Worm.GoldLv >= priceGold.Length)
            {
                Debug.Log("!! Gold ���� �Ұ�");
                return;
            }

            //                         ������ ���� 
            if (worm.SubtractGold(priceGold[Worm.GoldLv]))
            {
                worm.AddLv(AddPercent.Gold);
                return;
            }
        }

        //���ǵ��߰�Ȯ�� ����
        public void PurchaseSpeed()
        {
            //������ �����ɷ������� ũ�ų� ������ return
            if (Worm.SpeedLv >= priceSpeed.Length)
            {
                Debug.Log("speed ���źҰ�");
                return;
            }
            //                         ������ ���� 
            if (worm.SubtractGold(priceSpeed[Worm.SpeedLv]))
            {
                worm.AddLv(AddPercent.Speed);
                return;
            }
        }

        //���� ����
        public void PurchaseDef()
        {
            //������ �����ɷ������� ũ�ų� ������ return
            if (Worm.DefLv >= priceDef.Length)
            {
                Debug.Log("���� ���źҰ�");
                return; 
            }
            //                         ������ ���� 
            //1,2,3���� ���� �����ʿ�
            if (worm.SubtractGold(priceDef[Worm.DefLv]))
            {
                worm.AddLv(AddPercent.Def);
                return;
            }
        }
        #endregion

        void OnPoint(InputValue value)
        {
            PriceTextControl.mousePoint = value.Get<Vector2>();
        }
    }
}
