using UnityEngine;

namespace WiggleQuest
{
    public class Tail : MonoBehaviour
    {
        //�߰�����
        //������ ���̱� (�ڷ�ƾ?

        // //////////////////�����κ�//////////////////////////////////
        // ���Ḯ��Ʈ ���
        public Tail beforeTail;
        public int tailNumber;


        // //////////////////�����̱�//////////////////////////////////

        //before�� this���� �Ÿ�
        private float betweenDis;

        //worm�� ������ �Ÿ�
        private float moveDis;
        
        //����
        private Worm worm;

        private void Start()
        {
            //�󱼻��� ��� ���� '�ʱ�ȭ'
            if (beforeTail != null)
            {
                //�հ� this�� �Ÿ� float
                betweenDis = 0.2f; //(beforeTail.transform.position - this.transform.position).magnitude;
                worm = GameObject.Find("Worm").GetComponent<Worm>();
            }
        }


        private void Update()
        {
            //�󱼻��� ��� ���� ����
            if (beforeTail != null)
            {
                if (Worm.isWormMoving == true)
                {
                    moveDis = worm.MoveDis();

                    MoveTail();
                }
            }
        }

        void MoveTail()
        {
            //[1]frontTail.Movedistance ��ŭ �����̵�(t�� �̵��ѰŸ� / [�� ������ �Ÿ�])
            Vector3 dir = (beforeTail.transform.position - this.transform.position);
            if (dir.magnitude > betweenDis)
            {
                this.transform.position += dir.normalized * moveDis * Worm.Speed * Time.deltaTime;
                Debug.Log("Move" + tailNumber);
            }
        }
    }
}
