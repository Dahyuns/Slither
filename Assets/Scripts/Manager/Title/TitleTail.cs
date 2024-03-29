using UnityEngine;

namespace WiggleQuest
{
    public class TitleTail : MonoBehaviour
    {
        // //////////////////생성부분//////////////////////////////////
        // 연결리스트 사용 : 타이틀부분에선 직접연결(Control 안씀)
        public TitleTail beforeTail;
        public int tailNumber;


        // //////////////////움직이기//////////////////////////////////
        private float tailSpeed = 100f;

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
                    betweenDis = 7f;
                }
                else if (tailNumber == 1) //tailNumber가 1일때 기준거리
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
