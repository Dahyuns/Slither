using UnityEngine;

namespace WiggleQuest
{
    public class TitleTail : MonoBehaviour
    {
        // //////////////////�����κ�//////////////////////////////////
        // ���Ḯ��Ʈ ��� : Ÿ��Ʋ�κп��� ��������(Control �Ⱦ�)
        public TitleTail beforeTail;
        public int tailNumber;


        // //////////////////�����̱�//////////////////////////////////
        private float tailSpeed = 100f;

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
                    betweenDis = 7f;
                }
                else if (tailNumber == 1) //tailNumber�� 1�϶� ���ذŸ�
                {
                    betweenDis = 10f;
                }

                if (dir.magnitude > betweenDis)
                {
                    MoveTail();
                }
            }
        }

        void MoveTail()
        {
            this.transform.position += dir.normalized * tailSpeed * Time.deltaTime;
        }
    }
}
