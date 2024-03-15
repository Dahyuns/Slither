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

        //�б�����
        public static float Heart   { get { return heart; } }
        public static int   Gold    { get { return gold; } }
        public static float Speed   { get { return speed; } }
        public static float Def     { get { return def; } }

        public static int Level     { get { return (int)heart;} } //�Ӹ����� ǥ�ÿ� //���� = ��� �ݳ�����

        //*--Lv��ŭ �߰� ȹ��
        private static float heartAddLv = 0;                   //����   �߰� ȹ��Lv
        private static float goldAddLv = 0;                    //���   �߰� ȹ��Lv
        private static float defAddLv = 0;                     //���� �߰� ȹ��Lv
        private static float speedAddLv = 0;                   //�ӵ�   �߰� ȹ��Lv

        //--%��ŭ �߰� ȹ�� <����> //�뷱�������κ�
        private float heartAddP = 0;                   //����   --%��ŭ �߰� ȹ��
        private float goldAddP = 0;                    //���   --%��ŭ �߰� ȹ��
        private float defAddP = 0;                     //���� --%��ŭ �߰� ȹ��
        private float speedAddP = 0;                   //�ӵ�   --%��ŭ �߰� ȹ��

        //Gold, Heart, Def�� ����, %�߰�     
        public void AddLv(AddPercent what)
        {
            switch (what)
            {
                //��巹����
                case AddPercent.Gold:
                    goldAddLv += 1;
                    break;

                //����
                case AddPercent.Heart:
                    heartAddLv += 1;
                    break;

                //����
                case AddPercent.Def:
                    defAddLv += 1;
                    break;

                //�ӵ�
                case AddPercent.Speed:
                    speedAddLv += 1;
                    break;
            }
        }

        //���� ����
        private int startHeart = 3;
        private int startGold = 200;
        private float startSpeed = 5f;
        private float startDef = 0;

        //������ ����
        Vector3 moveDir;
        private Vector3 beforeLocate;

        //�ٸ� �ڵ忡 ��������
        public static bool isWormMoving = false;
        public static bool isWormDead = false;

        private void Start()
        { 
            //�ʱ�ȭ
            gold = startGold;
            heart = startHeart;
            speed = startSpeed;
            def = startDef;
        }

        private void Update()
        {
            //����ó��
            if (heart < 1)
            {
                isWormDead = true;
                return;
            }


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


            //[ġƮŰ] : ��� up
            if (Input.GetKey(KeyCode.M))
            {
                gold += 10000;
            }
            //[ġƮŰ] : ���� ���� up
            if (Input.GetKeyDown(KeyCode.LeftShift))
                heart++;
            //[ġƮŰ] : ���� ���� down
            if (Input.GetKeyDown(KeyCode.LeftControl))
                heart--;

        }

        #region + , -
        //[ float ���� int �� ��ȯ�Ҷ� �⺻������ �Ҽ��� ���ڸ��� ������ �Ѵ�. ]

        // ��� �߰� - ���ȹ�� => ���� ����ִ� ��� += ���� ��� + ���� ��� * (goldAddLv * 00%) 
        public void AddGold(int value)
        {
            gold += value + (int)(value * goldAddLv * goldAddP);
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

        // ��� �߰� - ����ȹ�� => ���� �����ִ� ��� += ���� ���� + ���� ���� * (goldAddLv * 00%)
        public void AddHeart(float value)
        {
            heart += value + (int)(value * heartAddLv * heartAddP);
        }

        //      ���� - ��ֹ� �ε��� => ����!!!
        public void SubtractHeart(float value)
        {
            float newvalue = heart - value - (int)(value * ((heartAddLv * heartAddP) / 100));
            if (newvalue < 0)
            {
                heart = 0;
                DeadWorm();
                return;
            }
            heart = newvalue ;
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