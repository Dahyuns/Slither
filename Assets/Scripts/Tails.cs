using UnityEngine;

namespace WiggleQuest
{
    public class Tails : MonoBehaviour
    {
        public GameObject tailsPrefab; //���� ������?

        /*//���ںο��� ������� �� �� ��ġ
        public Vector3 firstPosition; 
        private Vector3 NextPosition;*/

        //���� �� ����
        public Transform thisTail;
        //�� ���� ����
        public Transform frontTail;

        //�հ� This ���̰Ÿ�
        private Vector3 Movedis;




        void MoveTail()
        {
            //frontTail.Movedistance ��ŭ �����̵�(t�� �̵��ѰŸ� / [�� ������ �Ÿ�])

        }

        private void OnEnable()
        {
            Movedis = frontTail.transform.position - thisTail.transform.position;
        }

        private void Update()
        {
            if (Worm.isWormMoving)
            {
                MoveTail();
                Worm.isWormMoving = false;
            }
        }
    }
}
