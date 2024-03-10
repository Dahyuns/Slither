using UnityEngine;
using UnityEngine.InputSystem;

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

        //���� - �뷱�������κ�
        [SerializeField] private int[] priceHeart = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
        [SerializeField] private int[] priceGold  = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
        [SerializeField] private int[] priceSpeed = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
        [SerializeField] private int[] priceDef   = { 100, 200, 300 };

        //������ ������?
        private bool isInShop = false;

        // Ÿ��Ʋ <-> ( �ʵ� <-> ���� )
        //�ʵ����� : ���ӽ���        �� Ÿ��Ʋ����
        //�ʵ����� : ���ӿ���        �� Ÿ��Ʋ��
        //�������� : ���� ��ư Ŭ��  �� �ʵ忡��
        //�������� : ESC            �� �ʵ��
        private void Start()
        {
            //���޴��� ��� ������Ʈ ��������?
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

        //�����߰�Ȯ�� ����
        public void PurchaseHeart()
        {
            if (worm.SubtractGold(priceHeart[0]))
            {
                worm.AddLv(AddPercent.Heart);
                Debug.Log("HP ����");
            }
        }

        //����߰�Ȯ�� ����
        public void PurchaseGold()
        {
            if (worm.SubtractGold(priceGold[0]))
            {
                worm.AddLv(AddPercent.Gold);
                Debug.Log("Gold ����");
            }
        }

        //���ǵ��߰�Ȯ�� ����
        public void PurchaseSpeed()
        {
            if (worm.SubtractGold(priceSpeed[0]))
            {
                worm.AddLv(AddPercent.Speed);
                Debug.Log("���ǵ� ����");
            }
        }

        //���� ����
        public void PurchaseDef()
        {
            //1,2,3���� ���� �����ʿ�
            if (worm.SubtractGold(priceDef[0]))
            {
                worm.AddLv(AddPercent.Def);
                Debug.Log("���� ����");
            }
        }

        void OnPoint(InputValue value)
        {
            PriceTextControl.mousePoint = value.Get<Vector2>();
        }
    }
}
