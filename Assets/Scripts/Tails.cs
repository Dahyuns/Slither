using UnityEngine;

namespace WiggleQuest
{
    public class Tails : MonoBehaviour
    {
        public GameObject tailsPrefab; //꼬리 프리팹?

        /*//숫자부여한 순서대로 그 전 위치
        public Vector3 firstPosition; 
        private Vector3 NextPosition;*/

        //지금 이 꼬리
        public Transform thisTail;
        //이 앞의 꼬리
        public Transform frontTail;

        //앞과 This 사이거리
        private Vector3 Movedis;




        void MoveTail()
        {
            //frontTail.Movedistance 만큼 보간이동(t가 이동한거리 / [둘 사이의 거리])

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
