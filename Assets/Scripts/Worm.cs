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

        //--%��ŭ �߰� ȹ��
        private static float goldAddP = 0;                    //���   --%��ŭ �߰� ȹ��
        private static float heartAddP = 0;                   //����   --%��ŭ �߰� ȹ��
        private static float defAddP = 0;                     //���� --%��ŭ �߰� ȹ��
        private static float speedAddP = 0;                   //�ӵ�   --%��ŭ �߰� ȹ��

        //Gold, Heart, Def�� ����, %�߰�
        //�뷱�������κ�
        public void AddP(AddPercent what)
        {
            //�������?..�������� �ϴ°ɷ� ���߿� �ٲٻ�
            SubtractGold(100);

            switch (what)
            {
                //���
                case AddPercent.Gold:
                    goldAddP += 0;
                    break;

                //����
                case AddPercent.Heart:
                    heartAddP += 0;
                    break;

                //����
                case AddPercent.Def:
                    defAddP += 0;
                    break;

                //�ӵ�
                case AddPercent.Speed:
                    speedAddP += 0;
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

        // ��� �߰� - ���ȹ�� => ���� ����ִ� ��� += ���� ��� + ���� ��� * goldAddP
        public void AddGold(int value)
        {
            gold += value + (int)(value * goldAddP);
        }

        //      ���� - ��������
        public void SubtractGold(int value)
        {
            gold -= value;
        }

        // ��� �߰� - ����ȹ�� => ���� �����ִ� ��� += ���� ���� + ���� ���� * goldAddP
        public void AddHeart(float value)
        {
            heart += value + (int)(value * heartAddP);
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