using UnityEngine;

namespace WiggleQuest
{
    //�ؾ��ϴ°�
    //���ִ��� Ȯ��

    public class ShopUI : MonoBehaviour
    {
        //����
        public Worm worm;
        public GameObject ShopButton; //���� ��ư [ Shop ]
        public GameObject ShopMenu; //���� UI

        //����
        [SerializeField] private int[] priceHeart = {100,200,300};
        [SerializeField] private int[] priceGold  = { 100, 200, 300 };
        [SerializeField] private int[] priceSpeed = { 100, 200, 300 };
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

        //ESC input�� //�������� �ʵ��
        public void GotoPlayGround()
        {
            Time.timeScale = 1f; //�ٽ� ���
            ShopStatusReverse();
        }

        //��ư Ŭ���� //�ʵ忡�� ��������
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

        //���ǵ� ����
        public void PurchaseSpeed()
        {
            //�������?..
            worm.SubtractGold(priceSpeed[0]);
            worm.AddLv(AddPercent.Speed);
            Debug.Log("���ǵ� ����");

        }

        //���� ����
        public void PurchaseDef()
        {
            worm.SubtractGold(priceDef[0]);
            worm.AddLv(AddPercent.Def);
            Debug.Log("���� ����");
        }

        //�����ۼ�Ʈ ����
        public void PurchaseHeart()
        {
            worm.SubtractGold(priceHeart[0]);
            worm.AddLv(AddPercent.Heart);
            Debug.Log("HP ����");

        }

        //����ۼ�Ʈ ����
        public void PurchaseGold()
        {
            worm.SubtractGold(priceGold[0]);
            worm.AddLv(AddPercent.Gold);
            Debug.Log("Gold ����");
        }
    }
}
