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
        // ** 따로 넣어야할듯 : 이 스크립트 얼굴에도 붙힘 **
        /*void MoveTail()
        {
            //[1]frontTail.Movedistance 만큼 보간이동(t가 이동한거리 / [둘 사이의 거리])
        }
        private float betweenDis;
        private void OnEnable()
        {
            //거리
            betweenDis = (beforeTail.transform.position - this.transform.position).magnitude;
        }

        private void Update()
        {
            if (Worm.isWormMoving)
            {
                MoveTail();
                Worm.isWormMoving = false;
            }
        }*/
    }
}
