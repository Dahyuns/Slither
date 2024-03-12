using UnityEngine;

namespace WiggleQuest
{
    public class Tail : MonoBehaviour
    {
        // //////////////////생성부분//////////////////////////////////
        // 연결리스트 사용
        public Tail beforeTail;
        public int tailNumber;


        // //////////////////움직이기//////////////////////////////////
        //참조
        private TailControl tailControl;

        //before과 this사이 기준거리
        private float betweenDis;
        Vector3 dir;


        private void Update()
        {
            //얼굴빼고 모든 꼬리 동작
            if (beforeTail != null)
            {
                dir = (beforeTail.transform.position - this.transform.position);

                if (tailNumber > 1) //tailNumber가 1보다 클때 기준거리
                {
                    betweenDis = 0.2f;
                }
                else if (tailNumber == 1) //tailNumber가 1일때 기준거리
                {
                    betweenDis = (TailControl.firstTailPos).magnitude;
                }

                //앞꼬리와의 거리가 기준 거리보다 짧다면
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

            /*[2]frontTail.Movedistance 만큼 보간이동(t가 이동한거리 / [둘 사이의 거리])
             * 
             * worm이 움직인 거리
             * private float wormMoveDis;
             * 
             * public float MoveDis() : Worm
              {    return (beforeLocate - transform.position).magnitude; }  */
        }
    }
}
