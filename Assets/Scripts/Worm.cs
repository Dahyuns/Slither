using UnityEngine;
using UnityEngine.InputSystem;

namespace WiggleQuest
{
    // --% ��ŭ �߰� ȹ��
    public enum AddPercent
    {
        Gold,   //���  
        Heart,  //����
        Def,    //����
        Speed   //�ӵ�  
    }

    public class Worm : MonoBehaviour
    {
        //������ ����
        private static int gold;                          //���
        private static float heart;                       //���
        private static float speed;                       //�ӵ�
        private static float def;                         //����

        //�б�����
        public static int Gold      { get { return gold; } }
        public static float Heart   { get { return heart; } }
        public static float Speed   { get { return speed; } }
        public static float Def     { get { return def; } }
        public static int Level     { get { return (int)heart;} } //�Ӹ����� ǥ�� //���� = ��� �ݳ�����

        //*--Lv��ŭ �߰� ȹ��
        private static float goldAddLv = 0;                    //���   �߰� ȹ��Lv
        private static float heartAddLv = 0;                   //����   �߰� ȹ��Lv
        private static float defAddLv = 0;                     //���� �߰� ȹ��Lv
        private static float speedAddLv = 0;                   //�ӵ�   �߰� ȹ��Lv

        //--%��ŭ �߰� ȹ�� <����>
        private float goldAddP = 0;                    //���   --%��ŭ �߰� ȹ��
        private float heartAddP = 0;                   //����   --%��ŭ �߰� ȹ��
        private float defAddP = 0;                     //���� --%��ŭ �߰� ȹ��
        private float speedAddP = 0;                   //�ӵ�   --%��ŭ �߰� ȹ��

        //Gold, Heart, Def�� ����, %�߰�     //�뷱�������κ��߰� �ʿ�
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
        [SerializeField] private int startGold = 0;
        [SerializeField] private int startHeart = 3;
        [SerializeField] private float startSpeed = 10f;
        [SerializeField] private float startDef = 0;

        //������ ����
        Vector3 moveDir;
        //���� �ڵ忡 ��������
        public static bool isWormMoving = false;

        private void Start()
        {
            gold = startGold;
            heart = startHeart;
            speed = startSpeed;
            def = startDef;
        }

        private void Update()
        {
            if (moveDir != Vector3.zero)
                transform.Translate(moveDir.normalized * Time.deltaTime * Speed, Space.World);

            //ġƮŰ
            if (Input.GetKey(KeyCode.M))
            {
                gold += 10000;
            }
        }

        void OnMove(InputValue value)
        {
            Vector2 input = value.Get<Vector2>();
            if (input != null)
            {
                moveDir = new Vector3(input.x, 0, input.y);
            }
            isWormMoving = true;
        }

        //[ float ���� int �� ��ȯ�Ҷ� �⺻������ �Ҽ��� ���ڸ��� ������ �Ѵ�. ]

        // ��� �߰� - ���ȹ�� => ���� ����ִ� ��� += ���� ��� + ���� ��� * (goldAddLv * 00%) 
        public void AddGold(int value)
        {
            gold += value + (int)(value * goldAddLv);
        }

        //      ���� - ��������
        public void SubtractGold(int value)
        {
            gold -= value;
        }

        // ��� �߰� - ����ȹ�� => ���� �����ִ� ��� += ���� ���� + ���� ���� * (goldAddLv * 00%)
        public void AddHeart(float value)
        {
            heart += value + (int)(value * heartAddLv);
        }

        //      ���� - ��ֹ� �ε��� => ����!!!
        public void SubtractHeart(float value)
        {
            heart -= value;
        }

        // �ӵ� ����
        public void AddSpeed(float value)
        {
            speed += value;
        }

        // ���� �߰� (�ۼ�Ʈ)
        public void AddDef(float value)
        {
            def += value;
        }
    }
}