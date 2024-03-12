using UnityEngine;

namespace WiggleQuest
{
    public class Tail : MonoBehaviour
    {
        // //////////////////�����κ�//////////////////////////////////
        // ���Ḯ��Ʈ ���
        public Tail beforeTail;
        public int tailNumber;


        // //////////////////�����̱�//////////////////////////////////
        //����
        private TailControl tailControl;

        //before�� this���� ���ذŸ�
        private float betweenDis;
        Vector3 dir;


        private void Update()
        {
            //�󱼻��� ��� ���� ����
            if (beforeTail != null)
            {
                dir = (beforeTail.transform.position - this.transform.position);

                if (tailNumber > 1) //tailNumber�� 1���� Ŭ�� ���ذŸ�
                {
                    betweenDis = 0.2f;
                }
                else if (tailNumber == 1) //tailNumber�� 1�϶� ���ذŸ�
                {
                    betweenDis = (TailControl.firstTailPos).magnitude;
                }

                //�ղ������� �Ÿ��� ���� �Ÿ����� ª�ٸ�
                if (dir.magnitude > betweenDis)
                {
                    MoveTail();
                }
            }
        }

        void MoveTail()
        {
            //[1]
            this.transform.position += dir.normalized * Worm.Speed * Time.deltaTime;

            /*[2]frontTail.Movedistance ��ŭ �����̵�(t�� �̵��ѰŸ� / [�� ������ �Ÿ�])
             * 
             * worm�� ������ �Ÿ�
             * private float wormMoveDis;
             * 
             * public float MoveDis() : Worm
              {    return (beforeLocate - transform.position).magnitude; }  */
        }
    }
}
