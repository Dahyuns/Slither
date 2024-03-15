using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WiggleQuest
{
    public class FieldTile : MonoBehaviour
    {
        //추가구현?
        //저장해놨다가 다시 가져가는 방법? -  오브젝트 풀링?
        //필드 아이템의 좌표 서로 겹치지않게 만들기 : sin그래프의 x좌표 리스트 만들기! x겹치지 않기
        // ㄴsin그래프 사용 200sin ? x = y   : ?는 주기에 곱하는 배수 , 원점은 시작지점

        //참조 
        private GameObject mainCamera;
        private FieldControl fieldControl;
        private GameObject groupTrap;
        private GameObject groupDrop;

        //프리팹
        public GameObject firePrefab;
        public GameObject feedPrefab;
        public GameObject goldPrefab;

        private Vector3 thisSize;

        //필드 드롭 간격 (커질수록 촘촘해짐, 기본은 1)
        public float cycleMulti = 1;

        //해당 필드내 아이템 저장 리스트
        [SerializeField] private List<GameObject> FieldItemList = new List<GameObject>();
        [SerializeField] private List<Vector3> itemPosList;

        private void Awake()
        {
            fieldControl = GameObject.Find("Field").GetComponent<FieldControl>();
            mainCamera = GameObject.Find("Main Camera");
            groupTrap = GameObject.Find("Trap");
            groupDrop = GameObject.Find("Drop");
            //시작지점 생성X (충돌 가능성)
            if (fieldControl.fieldTiles.Count == 0)
                return;
            else  //필드내 아이템들 생성
                StartCoroutine(CreateItems());
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
            if (distance > fieldControl.standardDis)
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
        public IEnumerator CreateItems()
        {
            //필드 타일 사이즈(거리 측정용)
            thisSize = Vector3.Scale(GetComponent<MeshFilter>().sharedMesh.bounds.size,
                                         transform.localScale);

            int numFire = fieldControl.numFire;
            int numGold = fieldControl.numGold;
            int numFeed = fieldControl.numFeed;

            //필드당 * ~ *개 생성
            //numFire = Random.Range(0, numFire);
            //numGold = Random.Range(0, numGold);
            //numFeed = Random.Range(0, numFeed);

            //fire
            for (int i = 0; i < numFire; i++)
            {
                //생성
                GameObject item = Instantiate(firePrefab, groupTrap.transform);

                //아이템 목록에 추가
                FieldItemList.Add(item);

                RanPosMake();

                //생성 아이템 위치 조정
                FieldItemList[i].transform.position = itemPosList[i];
                numFire--;
            }




            //총 개수
            int totalList = numFeed + numFire + numGold;

            for (int i = 0; i < totalList; i++)
            {
                GameObject item;

                //생성 + 아이템 리스트에 추가
                if (numFeed > 0)
                {
                    item = Instantiate(feedPrefab, groupDrop.transform);

                    Debug.Log(numFeed);
                    numFeed--;
                }
                else if (numFire > 0)
                {
                    item = Instantiate(firePrefab, groupTrap.transform);

                    numFire--;
                }

                else //if (numGold > 0)
                {
                    item = Instantiate(goldPrefab, groupDrop.transform);

                    Debug.Log(numGold);
                    numGold--;
                }

                FieldItemList.Add(item);

                //랜덤한 좌표 생성 //겹치지않게 만들기
                Vector3 thisPos = this.transform.position - (thisSize / 2); //왼쪽아래 기준
                                                                            // Sin함수 시작 지점(=z변의 길이의 반) * SIN함수 (총 거리의 배수 * 랜덤한 x값) + 원점이동값(=z변의 길이의 반)
                float numX = RanX();
                float numZ = (thisSize.z / 2) * Mathf.Sin(cycleMulti * numX) + (thisSize.z / 2);

                //생성한 좌표, 좌표 리스트에 추가
                itemPosList.Add(new Vector3(thisPos.x + numX, 0f, thisPos.z + numZ));

                //생성 아이템 위치 조정
                FieldItemList[i].transform.position = itemPosList[i];



                //생성해야할 개수 삭제

                totalList--;
            }
            yield return null;
        }

        private void RanPosMake()
        {
            //왼쪽아래 기준 좌표 생성 (sin그래프를 위해)
            Vector3 thisPos = this.transform.position - (thisSize / 2);

            //랜덤한 좌표 생성 //겹치지않게 만들기
            // Sin함수 시작 지점(=z변의 길이의 반) * SIN함수 (총 거리의 배수 * 랜덤한 x값) + 원점이동값(=z변의 길이의 반)
            float numX = RanX();
            float numZ = (thisSize.z / 2) * Mathf.Sin(cycleMulti * numX) + (thisSize.z / 2);

            //생성한 좌표, 좌표 리스트에 추가
            itemPosList.Add(new Vector3(thisPos.x + numX, 0f, thisPos.z + numZ));
        }

        private float RanX()
        {
            float numX = Random.Range(0, thisSize.x);
            bool isNullPos = false;

            while (isNullPos == false)
            {
                foreach (Vector3 itempos in itemPosList)
                {
                    if (numX < itempos.x + 1 || numX > itempos.x - 1)
                    {
                        continue;
                    }
                }
                isNullPos = true;
                return numX;
            }

            Debug.Log("FIELDTILE RanX 함수 버그");
            return 0;
        }
    }
}