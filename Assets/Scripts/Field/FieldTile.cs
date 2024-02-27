using System.Collections.Generic;
using UnityEngine;


namespace WiggleQuest
{
    public class FieldTile : MonoBehaviour
    {
        //FieldTile : 현 타일이 플레이어에서 일정거리 멀어지면 (필드 위 모든 아이템과 함께)삭제

        //참조
        public GameObject worm;

        //프리팹
        public GameObject firePrefab;
        public GameObject feedPrefab;
        public GameObject goldPrefab;

        //삭제 기준 거리
        [SerializeField] private float standardDis = 20f;

        private List<GameObject> FieldItemList;

        private void Start()
        {
            //랜덤 생성 + 리스트에 저장
            CreateFieldItems();

        }

        private void Update()
        {
            //맵 위치 체크 및 삭제
            if (CheckFieldTilePosWithWorm())
            {
                DestroyFieldItems();
            }
        }


        //맵위치체크
        private bool CheckFieldTilePosWithWorm()
        {
            float distance = (transform.position - worm.transform.position).magnitude;
            if (distance > standardDis)
                return true;
            else
                return false;
        }

        //feed gold trap(fire,...)
        //     ㄴ 위치 랜덤/ 수량 조절 가능하게 / 겹치면X
        //                                      ㄴ 겹치면 destroy? 
        //                                          ㄴ 규칙 생성!! , 위치 정보 저장해서 겹치면 생성 X


        private void CreateFieldItems()
        {
            //밸런스 조정 가능 부분 - 따로 빼기
            int numFire = Random.Range(0, 3);
            int numGold = Random.Range(0, 3);
            int numFeed = Random.Range(0, 3);

            int numList = 0;

            FieldItemList = new List<GameObject>(numFeed + numFire + numGold);

            for (int i = 0; i < numFire; i++)
            {
                FieldItemList.Add(Instantiate(firePrefab));

                FieldItemList[numList].transform.position = new Vector3(0f, 0f, 0f);
                //랜덤위치 조정. 근데 규칙은 있어야함

                numList++;
            }
        }

        private void DestroyFieldItems()
        {
            //전부 삭제
            //저장해놨다가 다시 가져가는 방법도? -  오브젝트 풀링이라고 하던가?
            //this.gameObject.SetActive(false);
            for (int i = 0; i < FieldItemList.Count; ++i)
            {
                Destroy(FieldItemList[i]);
            }
            Destroy(this.gameObject);
        }
    }
}