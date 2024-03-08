using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;


namespace WiggleQuest
{
    public class FieldTile : MonoBehaviour
    {
        //추가구현?
        //저장해놨다가 다시 가져가는 방법? -  오브젝트 풀링이라고 하던가?
        //                              this.gameObject.SetActive(false);
        //랜덤한 좌표 겹치지않게 만들기

        //FieldTile : 해당 필드타일 생성시 규칙에 따라 랜덤으로 아이템 생성
        //             feed gold trap(fire,...) 위치 랜덤/ 수량 조절 가능하게 /
        //            플레이어에서 일정거리 멀어지면 (필드 위 모든 아이템과 함께)삭제

        //참조 
        private GameObject mainCamera;
        private FieldControl fieldControl;
        private GameObject groupTrap;
        private GameObject groupDrop;

        //프리팹
        public GameObject firePrefab;
        public GameObject feedPrefab;
        public GameObject goldPrefab;

        //필드당 생성 개수(최대)
        [SerializeField] private int numFire = 5;
        [SerializeField] private int numGold = 1;
        [SerializeField] private int numFeed = 4;

        //삭제 기준 거리
        [SerializeField] private float standardDis = 1000f;

        private Vector3 thisSize;

        //해당 필드내 아이템 저장 리스트
        [SerializeField] private List<GameObject> FieldItemList = new List<GameObject>();
        [SerializeField] private List<Vector3> itemPosList;

        //feed Mat 저장
        Material[] mat;

        private void Awake()
        {
            mainCamera = GameObject.Find("Main Camera");
            fieldControl = GameObject.Find("Field").GetComponent<FieldControl>();
            groupTrap = GameObject.Find("Trap");
            groupDrop = GameObject.Find("Drop");
           // mat = feedPrefab.GetComponent<Renderer>().materials;
        }

        private void Update()
        {
            //맵 위치 체크 및 [삭제]
            if (fieldControl.isCreateTileMove && CheckDis())
            {
                DestroyField();
            }
        }

        //필드와 camera 사이의 거리 체크
        private bool CheckDis()
        {
            float distance = (transform.position - mainCamera.transform.position).magnitude;
            if (distance > standardDis)
                return true;
            else
                return false;
        }

        private void DestroyField()
        {
            //전부 삭제
            
            //필드내 아이템 삭제
            for (int i = 0; i < FieldItemList.Count; ++i)
            {
                Destroy(FieldItemList[i]);
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
            //필드 타일 사이즈(거리 측정용)
            thisSize = Vector3.Scale(GetComponent<MeshFilter>().sharedMesh.bounds.size,
                                         transform.localScale);

            //필드당 * ~ *개 생성
            numFire = Random.Range(1, numFire);
            numGold = Random.Range(1, numGold);
            numFeed = Random.Range(1, numFeed);

            //총 개수
            int totalList = numFeed + numFire + numGold;

            for (int i = 0; i < totalList; i++)
            {
                GameObject item;

                //생성 + 아이템 리스트에 추가
                if (numFire > 0)
                {
                    item = Instantiate(firePrefab, groupTrap.transform);
                    FieldItemList.Add(item);

                    numFire--;
                }
                else if (numFeed > 0)
                {
                    item = Instantiate(feedPrefab, groupDrop.transform);
                    FieldItemList.Add(item);

                    numFeed--;
                }
                else if (numGold > 0)
                {
                    //생성 + 아이템 리스트에 추가
                    item = Instantiate(goldPrefab, groupDrop.transform);
                    FieldItemList.Add(item);

                    numGold--;
                }


                //랜덤한 좌표 생성 //겹치지않게 만들기
                float numX = Random.Range(0, thisSize.x);
                float numZ = Random.Range(0, thisSize.z);
                Vector3 thisPos = this.transform.position - (thisSize / 2); //왼쪽아래 기준

                //생성한 좌표, 좌표 리스트에 추가
                itemPosList.Add(new Vector3(thisPos.x + numX, 0f, thisPos.z + numZ));

                //생성 아이템 위치 조정
                FieldItemList[i].transform.position = itemPosList[i];

                //생성해야할 개수 삭제
                totalList--;
            }
        }


    }
}