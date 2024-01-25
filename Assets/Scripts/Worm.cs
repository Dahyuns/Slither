using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Slither
{
    public class Worm : MonoBehaviour
    {
        private static int gold;  //���
        private static float heart; //���
        private static float speed; //�ӵ�
        private static float def;   //����

        //�̰� ������ �δ°� ����?
        private static float goldAddP; //���  --%��ŭ �߰� ȹ��
        private static float heartAddP;//����  --%��ŭ �߰� ȹ��
        private static float defAddP;  //���� --%��ŭ �߰� ȹ��

        //�б�����
        public int Gold { get { return gold; } }
        public float Heart { get { return heart; } }
        public float Speed { get { return speed; } }
        public float Def { get { return def; } }

        //���۽� 
        [SerializeField] private int startGold = 0;
        [SerializeField] private int startHeart = 0;
        [SerializeField] private float startSpeed = 1;
        [SerializeField] private float startDef = 0;

        Vector3 moveDir;

        private WormControls inputActions;

        private void Start()
        {
            gold = startGold;
            heart = startHeart;
            speed = startSpeed;
            def = startDef;
            inputActions = new WormControls();
        }

        #region
        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }
        #endregion

        private void Update()
        {
            if (moveDir != Vector3.zero)
                transform.Translate(moveDir.normalized * Time.deltaTime * Speed, Space.World);
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