using UnityEngine;

namespace Slither
{
    public class Tails : MonoBehaviour
    {
        /*//숫자부여한 순서대로 그 전 위치
        public Vector3 firstPosition; 
        private Vector3 NextPosition;*/

        //지금 이 꼬리
        public GameObject thisTail;
        //이 앞의 꼬리
        public GameObject frontTail;

        public GameObject tailsPrefab; //꼬리 프리팹

        void MoveTail()
        {
            //frontTail.Movedistance 만큼 보간이동(t가 이동한거리 / [둘 사이의 거리])
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
