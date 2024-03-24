using UnityEngine;
using UnityEngine.InputSystem;

namespace WiggleQuest
{
    //�߰�����
    //�̵� ���� (���������� ������)


    // �߰�Ȯ�� ����
    public enum AddPercent
    {
        Gold,   //���  
        Heart,  //����
        Speed,  //�ӵ�  
        Def     //����
    }

    public class Worm : MonoBehaviour
    {
        //������ ����
        [SerializeField] private static float heart;                       //���
        [SerializeField] private static int gold;                          //���
        [SerializeField] private static float speed;                       //�ӵ�
        [SerializeField] private static float def;                         //����
        //  ���б�����
        public static float Heart { get { return heart; } }
        public static int Gold { get { return gold; } }
        public static float Speed { get { return speed; } }
        public static float Def { get { return def; } }

        //�Ӹ����� ǥ�ÿ� //���� = ��� �ݳ�����
        public static int Level { get { return (int)heart; } }

        //*--Lv��ŭ �߰� ȹ��
        private static int heartAddLv = 0;                   //����   �߰� ȹ��Lv
        private static int goldAddLv = 0;                    //���   �߰� ȹ��Lv
        private static int speedAddLv = 0;                   //�ӵ�   �߰� ȹ��Lv
        private static int defAddLv = 0;                     //���� �߰� ȹ��Lv
        //  ���б����� - ����UI�� �������� ���� �� �̹��� �����ؾ���
        public static int HeartLv { get { return heartAddLv; } }
        public static int GoldLv { get { return goldAddLv; } }
        public static int SpeedLv { get { return speedAddLv; } }
        public static int DefLv { get { return defAddLv; } }

        //--%��ŭ �߰� ȹ�� <����> //�뷱�������κ�
        private const float heartAddP = 100;                    //����   --%��ŭ �߰� ȹ��
        private const float goldAddP = 100;                    //���   --%��ŭ �߰� ȹ��
        private const float speedAddP = 100;                    //�ӵ�   --%��ŭ �߰� ȹ��
        private const float defAddP = 100;                    //���� --%��ŭ �߰� ȹ��

        //���� ���� > �ʱ�ȭ��
        private int startHeart = 3;
        private int startGold = 10;
        private float startSpeed = 5f;
        private float startDef = 0;

        //�̵�
        Vector3 moveDir;
        private Vector3 beforeLocate;

        //����
        private ShopUI shopUI;
        //������
        public static bool isWormMoving = false;
        public static bool isWormDead = false;

        private void Start()
        {
            //�ʱ�ȭ
            gold = startGold;
            heart = startHeart;
            speed = startSpeed;
            def = startDef;

            //����
            shopUI = GameObject.Find("ShopUI").GetComponent<ShopUI>(); ;
        }

        private void Update()
        {
            //����ó��
            if (heart < 1)
            {
                isWormDead = true;
                return;
            }

            //�̵�
            if (moveDir != Vector3.zero)
            {
                transform.Translate(moveDir.normalized * Time.deltaTime * Speed, Space.World);
                isWormMoving = true;
            }
            else
            {
                isWormMoving = false;
                beforeLocate = this.transform.position;
            }

            //[ġƮŰ]
            //��� up
            if (Input.GetKey(KeyCode.M)) gold += 10000;
            //���� ���� up
            if (Input.GetKeyDown(KeyCode.LeftShift)) heart++;
            //���� ���� down
            if (Input.GetKeyDown(KeyCode.LeftControl)) heart--;

        }

        //Gold, Heart, Def�� ����, % ���� �߰�     
        public void AddLv(AddPercent what)
        {
            switch (what)
            {
                //����
                case AddPercent.Heart:
                    if (heartAddLv < shopUI.lengthPriceHeart)
                    {
                        heartAddLv++;
                    }
                    break;

                //���
                case AddPercent.Gold:
                    if (goldAddLv < shopUI.lengthPriceGold)
                    {
                        goldAddLv++;
                    }
                    break;

                //�ӵ�
                case AddPercent.Speed:
                    if (speedAddLv < shopUI.lengthPriceSpeed)
                    {
                        speedAddLv++;
                    }
                    break;

                //����
                case AddPercent.Def:
                    if (defAddLv < shopUI.lengthPriceDef)
                    {
                        defAddLv++;
                    }
                    break;
            }
        }

        #region + , - (ȹ�� ��� ����)
        //[ float ���� int �� ��ȯ�Ҷ� �⺻������ �Ҽ��� ���ڸ��� ������ �Ѵ�. ]

        // ��� �߰� - ����ȹ�� => ���� �����ִ� ��� += ���� ���� + ���� ���� * (goldAddLv * 00%)
        public void AddHeart(float value)
        {
            heart += value + (int)(value * heartAddLv * heartAddP);
        }

        //      ���� - ��ֹ� �ε��� => ����!
        public void SubtractHeart(float value)
        {
            heart -= value;
            if (heart <= 0)
            {
                heart = 0;
                //����ó��
                DeadWorm();
                return;
            }
        }

        // ��� �߰� - ���ȹ�� => ���� ����ִ� ��� += ���� ��� + ���� ��� * (goldAddLv * 00%) 
        public void AddGold(int value)
        {
            gold += value + (int)(value * goldAddLv * goldAddP);
            Debug.Log(gold);
        }

        //      ���� - ��������
        public bool SubtractGold(int value)
        {
            if (gold < value)
            {
                Debug.Log("������! ���źҰ���");
                return false;
            }
            else // gold >= value 
            {
                gold -= value;
                return true;
            }
        }

        // �ӵ� ����
        public void AddSpeed(float value)
        {
            speed += value + (int)(value * speedAddLv * speedAddP);
        }

        // ���� �߰� (�ۼ�Ʈ)
        public void AddDef(float value)
        {
            def += value + (int)(value * defAddLv * defAddP);
        }
        #endregion

        public float MoveDis()
        {
            return (beforeLocate - transform.position).magnitude;
        }

        //Death ó�� : �ӵ� 0�����
        public void DeadWorm()
        {
            speed = 0;
            isWormMoving = false;
        }

        void OnMove(InputValue value)
        {
            Vector2 input = value.Get<Vector2>();
            if (input != null)
            {
                moveDir = new Vector3(input.x, 0, input.y);
            }
        }
    }
}