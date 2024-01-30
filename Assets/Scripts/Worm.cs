using UnityEngine;
using UnityEngine.InputSystem;

namespace Slither
{
    public class Worm : MonoBehaviour
    {
        private static int gold;                          //���
        private static float heart;                       //���
        private static float speed;                       //�ӵ�
        private static float def;                         //����
        private static int level = (int)heart;                         //���� = ��� �ݳ�����
                                                          
        //�̰� ������ �δ°� ����?                          
        private static float goldAddP;                    //���  --%��ŭ �߰� ȹ��
        private static float heartAddP;                   //����  --%��ŭ �߰� ȹ��
        private static float defAddP;                     //���� --%��ŭ �߰� ȹ��

        //�б�����
        public int   Gold  { get { return gold;  } }
        public float Heart { get { return heart; } }
        public float Speed { get { return speed; } }
        public float Def   { get { return def;   } }
        public int   Level { get { return level; } }

        //���۽� 
        [SerializeField] private int startGold = 0;
        [SerializeField] private int startHeart = 5;
        [SerializeField] private float startSpeed = 10f;
        [SerializeField] private float startDef = 0;

        Vector3 moveDir;

        private void Awake()
        {
            //�̰� �Ǵ��� �ñݽ�
            //inputActions = GetComponent<WormControls>();
       }

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
        }


        // ��� �߰� - ���ȹ�� => ���� ����ִ� ��� += ���� ��� + ���� ��� * goldAddP

        // ��� ���� - ��������

        // ��� �߰� - ����ȹ�� => ���� �����ִ� ��� += ���� ���� + ���� ���� * goldAddP

        // ��� ���� - ��ֹ� �ε���

        // �ӵ� ����

        // ���� �߰� (�ۼ�Ʈ)
    }
}