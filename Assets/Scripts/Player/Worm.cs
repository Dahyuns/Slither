using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.Rendering.DebugUI;

namespace WiggleQuest
{
    //�߰�����
    //�̵� ���� (���������� ������)
    //���ǵ�� ���� ����


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
        private const float heartAddP = 3;                    //����   --%��ŭ �߰� ȹ��
        private const float goldAddP = 10;                    //���   --%��ŭ �߰� ȹ��
        private const float speedAddP = 100;                    //�ӵ�   --%��ŭ �߰� ȹ��
        private const float defAddP = 10;                    //���� --%��ŭ �߰� ȹ��

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
        private StatusUI statusUI;
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
            shopUI = GameObject.Find("ShopUI")?.GetComponent<ShopUI>();
            statusUI = GameObject.Find("StatusUI")?.GetComponent<StatusUI>();
        }

        private void Update()
        {
            //����ó��
            if (heart < 1)
            {
                isWormDead = true;
                return;
            }

            //���ǵ�� ���� ����
            //if (speed < )
            {

            }
            //if (def < )
            {

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
                    if (heartAddLv < shopUI.PriceHeart.Length)
                    {
                        heartAddLv++;
                    }
                    break;

                //���
                case AddPercent.Gold:
                    if (goldAddLv < shopUI.PriceGold.Length)
                    {
                        goldAddLv++;
                    }
                    break;

                //�ӵ�
                case AddPercent.Speed:
                    if (speedAddLv < shopUI.PriceSpeed.Length)
                    {
                        speedAddLv++;
                    }
                    break;

                //����
                case AddPercent.Def:
                    if (defAddLv < shopUI.PriceDef.Length)
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
            float tmp = value + (value * heartAddLv * heartAddP);
            heart += tmp;

            string str = "+" + tmp.ToString("F1");
            statusUI.StartCoroutine(statusUI.DrawAddText(AddType.Heart, str));
        }

        //      ���� - ��ֹ� �ε��� => ����!
        public void SubtractHeart(float value)
        {
            float tmp = value - (defAddLv * defAddP);
            if (heart - tmp <= 0)
            {
                heart = 0;
                //����ó��
                DeadWorm();
                return;
            }
            heart -= tmp;

            string str = "-" + tmp.ToString("F1");
            statusUI.StartCoroutine(statusUI.DrawAddText(AddType.Heart, str));
        }

        // ��� �߰� - ���ȹ�� => ���� ����ִ� ��� += ���� ��� + ���� ��� * (goldAddLv * 00%) 
        public void AddGold(int value)
        {
            int tmp = value + (int)(value * goldAddLv * goldAddP);
            gold += tmp;

            string str = "+" + tmp.ToString();
            statusUI.StartCoroutine(statusUI.DrawAddText(AddType.Gold, str));
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
            Debug.Log(speed);
        }

        // ���� �߰� (�ۼ�Ʈ)
        public void AddDef(float value)
        {
            def += value + (int)(value * defAddLv * defAddP);
            Debug.Log(def);
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