using System.Collections;
using UnityEngine;

namespace WiggleQuest
{
    public class TailControl : MonoBehaviour
    {
        //추가구현
        //꼬리 랜덤 생성

        private enum Count
        { Up, Down, None }
        public GameObject wormFace;    //지렁이 얼굴
        public GameObject tailPrefab; //꼬리 프리팹

        private Tail headTail = null;  //연결리스트 Head
        private Tail lastTail = null; //가장 뒤의 꼬리 저장

        private int tailCount = 0;   //'생성할' 꼬리 개수
        private int tailMakedCount = 0; //'만든' 꼬리 개수

        private Vector3 tailSize;
        private Vector3 faceSize;
        public static Vector3 firstTailPos;

        //코루틴 실행 조건
        private bool isAddingTail = false;

        private void Start()
        {
            //사이즈 구해서 그 뒤에 생성 : 사이즈에 scale곱함
            tailSize = Vector3.Scale(tailPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size, 
                                     tailPrefab.transform.localScale);
            //얼굴 사이즈
            faceSize = Vector3.Scale(wormFace.GetComponent<MeshFilter>().sharedMesh.bounds.size,
                                                                 wormFace.transform.localScale);
        }

        //count체크하고 메서드호출
        private void Update()
        {
            //현재 레벨 = 생성해야하는 꼬리 개수
            tailCount = Worm.Level;

            switch (CheckCount())
            {
                case Count.None:
                    break;

                case Count.Up:
                    if (isAddingTail == false)
                    {
                        StartCoroutine(AddTail());
                    }
                    break;

                case Count.Down:
                    if (tailMakedCount == 0)
                    {
                        //Debug.Log("더 이상 삭제할 수 없습니다. : GameOver");
                        return;
                    }
                    DestroyTail();
                    break;
            }
        }

        Count CheckCount()
        {
            //꼬리수가 만들어져있는 수보다 
            int countValue = tailCount - tailMakedCount;

            if (countValue == 0)
                return Count.None;

            else if (countValue > 0)
                return Count.Up;

            else //if (countValue < 0)
                return Count.Down;
        }
        
        //[2]연결리스트 노드활용  
        IEnumerator AddTail()
        {
            isAddingTail = true;

            //생성
            GameObject newobj = Instantiate(tailPrefab);
            Tail newtail = newobj.GetComponent<Tail>();


            // 만든 꼬리가 하나도 없다면(처음 만드는 꼬리) : 맨처음만 호출
            if (tailMakedCount == 0)
            {
                //얼굴이 전의 꼬리임
                newtail.beforeTail = wormFace.GetComponent<Tail>();

                // 방향 랜덤 조절
                // 여기는 원점 기준! 0<x<r , 0<y<r 
                float r = tailSize.x / 2 + faceSize.x / 2; //반지름
                float x = Random.Range(0f, r); // x좌표(1,4사분면)
                //y = (루트) r2 - x2 (4사분면)
                float y = Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow(x, 2));

                //new꼬리 (: 기존좌표에 더할 새로운 좌표)
                firstTailPos = new Vector3(x, 0, -y);

                //이번 꼬리의 위치는? : 얼굴 위치에 + 꼬리 사이즈 (랜덤위치 / 4사분면)
                newobj.transform.position = wormFace.transform.position + firstTailPos;
            }

            // 만든 꼬리가 있다면 == beforeTail이 존재한다면
            else if (tailMakedCount > 0)
            {
                //head(직전에 만든 꼬리)는 새 꼬리의 beforeTail 
                newtail.beforeTail = headTail;

                //이번 꼬리의 위치는? :
                //전의 꼬리 위치에
                //      방향 : 전꼬리 위치에서 직선방향, 크기 : 꼬리사이즈  
                Vector3 dir = newtail.beforeTail.transform.position - newtail.beforeTail.beforeTail.transform.position;
                dir = new Vector3(dir.x, 0f, dir.z); //높이무시

                if(tailMakedCount == 1) //2번째 꼬리
                {
                    dir = Vector3.ClampMagnitude(dir, tailSize.x);
                }

                newobj.transform.position = newtail.beforeTail.transform.position + dir;
            }
            //예외처리
            else if (tailMakedCount < 0)
            {
                Debug.Log("AddTail Code Error");
            }



            tailMakedCount++;//카운트 +1
            newtail.tailNumber = tailMakedCount; //이번 꼬리는 몇번쨰?

            //이 꼬리를 head와 last에 저장
            headTail = newtail; //생성용
            lastTail = newtail; //삭제용

            isAddingTail = false;
            yield return null;
        }

        void DestroyTail()
        {
            //머리만 남았을 경우엔 삭제하지 않는다.
            if (tailMakedCount != 0)
            {
                //Head를 전의 꼬리로
                headTail = headTail.beforeTail;
                //이번꼬리 삭제    
                Destroy(lastTail.gameObject);

                //삭제돼, null상태인 lastTail에 Head저장
                lastTail = headTail;
                //'만든' 꼬리 개수 감소
                tailMakedCount--;
            }
            //else
            //{
            //    Debug.Log("Destroy할수없음");
            //}
        }
    }
}