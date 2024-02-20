using System.Collections;
using UnityEngine;

namespace WiggleQuest
{
    public class TailControl : MonoBehaviour
    {
        private enum Count
        { Up, Down, None }
        public Transform wormFace;    //지렁이 얼굴
        public GameObject tailPrefab; //꼬리 프리팹

        private Tail headTail = null;  //연결리
        private Tail lastTail = null; //가장 뒤의 꼬리 저장

        [SerializeField]
        private int tailCount = 0;   //'생성할' 꼬리 개수
        private int tailMakeCount = 0; //'만든' 꼬리 개수
        public int tailStartCount = 3; //'시작시'의 꼬리 개수

        private Vector3 tailSize;

        private void Start()
        {
            //초기화 : '생성할' 꼬리 개수 = '시작시'의 꼬리 개수
            tailCount = tailStartCount;

            //사이즈 구해서 그 뒤에 생성 : 사이즈에 scale곱함
            tailSize = Vector3.Scale(tailPrefab.GetComponent<MeshRenderer>().bounds.size, tailPrefab.transform.localScale);
        }

        //count체크하고 메서드호출
        private void Update()
        {
            //시험용 : 만들 꼬리 개수 up
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                tailCount++;
            }

            switch (CheckCount())
            {
                case Count.None:
                    break;

                case Count.Up:
                    StartCoroutine(AddTail());
                    break;

                case Count.Down:
                    DestroyTail();
                    break;
            }
        }
        Count CheckCount()
        {
            //꼬리수가 만들어져있는 수보다 적으면
            int countValue = tailCount - tailMakeCount;
            if (countValue == 0)
                return Count.None;
            else if (countValue > 0)
                return Count.Up;
            else //if (countValue < 0)
                return Count.Down;
        }

        IEnumerator AddTail()
        {
            //[2]노드활용!- 연결리스트
            //이번 꼬리 생성! : 얼굴 자식으로
            Tail newtail = Instantiate(tailPrefab, wormFace).GetComponent<Tail>();

            //만든 꼬리가 하나도 없다면(처음 만드는 꼬리) : 맨처음만 호출
            if (tailMakeCount == 0)
            {
                //얼굴이 전의 꼬리임
                newtail.beforeTail = wormFace.GetComponent<Tail>();
                //이번 꼬리의 위치는? : 얼굴 위치에 + 꼬리 사이즈 (랜덤위치 / 반대쪽 예정)
                Vector3 faceSize = Vector3.Scale(wormFace.GetComponent<MeshRenderer>().bounds.size,
                                                                     wormFace.transform.localScale);//이거 잘못된듯!!?

                newtail.transform.position = new Vector3
                    (wormFace.transform.position.x + (faceSize.x / 2 + tailSize.x / 2) , //x
                     0f, 0f);
                     //wormFace.transform.position.y + ((faceSize.y + tailSize.y) / 2));
            }
            //만든 꼬리가 있다면 == beforeTail이 존재한다면
            else if (tailMakeCount > 0)
            {
                //head는 이 꼬리의 앞순서 
                newtail.beforeTail = headTail;
                //이번 꼬리의 위치는? : 전의 꼬리 위치에 + 꼬리 사이즈 (랜덤위치 / 반대쪽 예정) 
                newtail.transform.position = newtail.beforeTail.transform.position +
                                                new Vector3(tailSize.x, 0f, -tailSize.y);
            }
            //예외처리
            else if (tailMakeCount < 0)
            {
                Debug.Log("AddTail Code Error");
            }

            tailMakeCount++;//카운트 +1
            //이번 꼬리는 몇번쨰?
            newtail.tailNumber = tailMakeCount;

            //이 꼬리를 head와 last에 저장
            headTail = newtail; //생성용
            lastTail = newtail; //삭제용

            yield return null;
        }

        void DestroyTail()
        {
            //Head를 전의 꼬리로
            headTail = headTail.beforeTail;
            //이번꼬리 삭제    
            Destroy(lastTail);

            //삭제돼, null상태인 lastTail에 Head저장
            lastTail = headTail;
            //'만든' 꼬리 개수 감소
            tailMakeCount--;
        }
    }
}