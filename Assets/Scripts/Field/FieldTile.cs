using System.Collections.Generic;
using UnityEngine;


namespace WiggleQuest
{
    public class FieldTile : MonoBehaviour
    {
        //추가구현?
        //저장해놨다가 다시 가져가는 방법? -  오브젝트 풀링이라고 하던가?
        //                              this.gameObject.SetActive(false);

        //FieldTile : 해당 필드타일 생성시 규칙에 따라 랜덤으로 아이템 생성
        //             feed gold trap(fire,...) 위치 랜덤/ 수량 조절 가능하게 /
        //            플레이어에서 일정거리 멀어지면 (필드 위 모든 아이템과 함께)삭제

        //참조 
        private GameObject worm;
        private FieldControl fieldControl;

        //프리팹
        public GameObject firePrefab;
        public GameObject feedPrefab;
        public GameObject goldPrefab;

        //삭제 기준 거리
        [SerializeField] private float standardDis = 1000f;

        private Vector3 thisHalfSize;

        //해당 필드내 아이템 저장 리스트
        private List<GameObject> FieldItemList;
        private List<Vector3> itemPosList;

        private void Start()
        {
            worm = GameObject.Find("Worm");
            fieldControl = GameObject.Find("Field").GetComponent<FieldControl>();
            //사이즈의 반(거리 측정용)
            thisHalfSize = ( Vector3.Scale(GetComponent<MeshFilter>().sharedMesh.bounds.size,
                                         transform.localScale) ) / 2;
        }

        private void Update()
        {
            //맵 위치 체크 및 삭제
            if (fieldControl.isCreateTileMove && CheckDis())
            {
                DestroyField();
            }
        }

        //필드와 Worm 사이의 거리 체크
        private bool CheckDis()
        {
            float distance = (transform.position - worm.transform.position).magnitude;
            if (distance > standardDis)
                return true;
            else
                return false;
        }

        private void DestroyField()
        {
            //전부 삭제
            
            //필드내 아이템 삭제
            //for (int i = 0; i < FieldItemList.Count; ++i)
            {
                //Destroy(FieldItemList[i]);
            }

            //리스트 내 필드 위치 삭제
            for (int i = 0; i < fieldControl.fieldTiles.Count; i++)
            {
                if (fieldControl.fieldTiles[i] == this.transform.position)
                {
                    fieldControl.fieldTiles.Remove(fieldControl.fieldTiles[i]);
                }
            }

            //필드 삭제
            Destroy(this.gameObject);
        }

        //필드위에 아이템 생성 + 리스트에 저장    (규칙에따라랜덤)
        public void CreateItems()
        {
            //밸런스 조정 가능 부분 - 따로 빼기
            int numFire = 2;//Random.Range(0, 3);  //필드당 (0~3)개 생성
            int numGold = 2;//Random.Range(0, 1);  //필드당 (0~1)개 생성
            int numFeed = 2;//Random.Range(0, 3);  //필드당 (0~2)개 생성

            //총 개수
            int totalList = numFeed + numFire + numGold;

            FieldItemList = new List<GameObject>(numFeed + numFire + numGold);

            for (int i = 0; i < totalList; i++)
            {
                //생성 + 아이템 리스트에 추가
                FieldItemList.Add(Instantiate(firePrefab));

                //랜덤한 좌표 생성
                float numX = Random.Range(this.transform.position.x - thisHalfSize.x,
                                        this.transform.position.x + thisHalfSize.x);
                float numZ = Random.Range(this.transform.position.z - thisHalfSize.z,
                                        this.transform.position.z + thisHalfSize.z);

                //생성한 좌표, 좌표 리스트에 추가
                itemPosList.Add(new Vector3(numX, 0f, numZ));

                //생성 아이템 위치 조정
                FieldItemList[i].transform.position = new Vector3(numX, 0f, numZ);

                //생성해야할 개수 삭제
                totalList--;
            }
            Debug.Log(totalList);
        }

    }
}