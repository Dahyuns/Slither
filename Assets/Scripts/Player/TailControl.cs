using UnityEngine;

namespace WiggleQuest
{
    public class TailControl : MonoBehaviour
    {
        public Transform worm;        //지렁이 얼굴!
        public GameObject tailPrefab; //꼬리 프리팹

        private Tail headTail = null;  //가장 뒤의 꼬리
        private Tail lastTail = null; //가장 뒤의 꼬리 저장

        public static int tailCount = 0; //꼬리 몇개?

        private void Start()
        {
            //시작시 count만큼 꼬리 생성
            for(int i = 0; i < tailCount; i++)
            {
                AddTail();
            }
        }

        void AddTail()
        {
            //[2]노드활용!- 연결리스트
            //이번 꼬리 생성!
            Tail newtail = Instantiate(tailPrefab, worm).GetComponent<Tail>();
            //이번 꼬리는 몇번쨰?
            newtail.tailNumber = tailCount;
            tailCount++; //카운트 +1
            Debug.Log(tailCount);
            //head는 이 꼬리의 앞순서 
            newtail.beforeTail = headTail;
            //이 꼬리를 head와 last에 저장
            headTail = newtail;
            lastTail = newtail;

            //이번 꼬리의 위치는
            //전꼬리가 바라보는 방향의 반대편으로 00만큼이다. 
            newtail.transform.position = newtail.beforeTail.transform.position;

        }

        void DestroyTail()
        {
            headTail = headTail.beforeTail;
            Destroy(lastTail);
            lastTail = headTail;
            tailCount--;
        }

    }
}