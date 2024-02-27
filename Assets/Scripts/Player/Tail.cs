using UnityEngine;

namespace WiggleQuest
{
    public class Tail : MonoBehaviour
    {
        //추가구현
        //버벅임 줄이기 (코루틴?

        // //////////////////생성부분//////////////////////////////////
        // 연결리스트 사용
        public Tail beforeTail;
        public int tailNumber;


        // //////////////////움직이기//////////////////////////////////

        //before과 this사이 거리
        private float betweenDis;

        //worm이 움직인 거리
        private float moveDis;
        
        //참조
        private Worm worm;

        private void Start()
        {
            //얼굴빼고 모든 꼬리 '초기화'
            if (beforeTail != null)
            {
                //앞과 this의 거리 float
                betweenDis = 0.2f; //(beforeTail.transform.position - this.transform.position).magnitude;
                worm = GameObject.Find("Worm").GetComponent<Worm>();
            }
        }


        private void Update()
        {
            //얼굴빼고 모든 꼬리 동작
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
            //[1]frontTail.Movedistance 만큼 보간이동(t가 이동한거리 / [둘 사이의 거리])
            Vector3 dir = (beforeTail.transform.position - this.transform.position);
            if (dir.magnitude > betweenDis)
            {
                this.transform.position += dir.normalized * moveDis * Worm.Speed * Time.deltaTime;
                Debug.Log("Move" + tailNumber);
            }
        }
    }
}
