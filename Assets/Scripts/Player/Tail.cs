using UnityEngine;

namespace WiggleQuest
{
    public class Tail : MonoBehaviour
    {
        // //////////////////�����κ�//////////////////////////////////
        //�� ���� ����
        public Tail beforeTail;
        public int tailNumber;





        // //////////////////�����̱�//////////////////////////////////
        void MoveTail()
        {
            //[1]frontTail.Movedistance ��ŭ �����̵�(t�� �̵��ѰŸ� / [�� ������ �Ÿ�])
        }
        private float betweenDis;
        private void OnEnable()
        {
            //�Ÿ�
            betweenDis = (beforeTail.transform.position - this.transform.position).magnitude;
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
