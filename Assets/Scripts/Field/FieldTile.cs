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

        //+ 랜덤아이템 생성 함수의 fire생성 확률을 높여야함. < 다른 아이템이 너무 빨리 나와서 한곳에 뭉침현상
        //  각각 수들의 비율 계산해서 확률에 반영하는 식으로?

        //참조 
        private GameObject mainCamera;
        private GameObject groupTrap;
        private GameObject groupDrop;
        private FieldControl fieldControl;

        private GameObject item;

        //프리팹
        public GameObject firePrefab;
        public GameObject feedPrefab;
        public GameObject goldPrefab;

        private Vector3 thisSize;

        //필드 드롭 간격 (커질수록 촘촘해짐, 기본은 1)
        private float cycleMulti = 1;

        //해당 필드내 아이템 저장 리스트
        [SerializeField] private List<GameObject> FieldItemList = new List<GameObject>();
        [SerializeField] private List<Vector3> itemPosList;

        int numFire;
        int numGold;
        int numFeed;

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
            {
                StartCoroutine(CreateItems()); //로딩과 함께 실행?
            }
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


        public enum DropItem
        {
            Fire, Gold, Feed, None
        }

        //랜덤 아이템 생성
        public GameObject RandomItemCreate()
        {
            List<GameObject> prefabList = new List<GameObject>() { firePrefab, goldPrefab, feedPrefab };
            if (numFire == 0)
                prefabList.Remove(firePrefab);
            if (numGold == 0)
                prefabList.Remove(goldPrefab);
            if (numFeed == 0)
                prefabList.Remove(feedPrefab);
            //Debug.Log($"numFire : {numFire}, numGold : {numGold}, numFeed : {numFeed}");
            if (prefabList.Count != 0)
            {
                //랜덤으로 골라서 리턴
                int rand = Random.Range(0, prefabList.Count);
                if (prefabList[rand] == firePrefab)
                {
                    numFire--;
                    return Instantiate(prefabList[rand], groupTrap.transform);
                }
                else
                {
                    if (prefabList[rand] == goldPrefab)
                        numGold--;
                    else if (prefabList[rand] == feedPrefab)
                        numFeed--;

                    return Instantiate(prefabList[rand], groupDrop.transform);
                }
            }
            else
            {
                Debug.Log("fieldTile - total크기보다 더 많이 실행 / prefabList오류");
                return null;
            } //예외체크
        }

        //필드위에 아이템 생성 + 리스트에 저장
        public IEnumerator CreateItems()
        {
            //필드 타일 사이즈(거리 측정용)
            thisSize = Vector3.Scale(GetComponent<MeshFilter>().sharedMesh.bounds.size,
                                         transform.localScale);

            numFire = fieldControl.numFire - Random.Range(0, fieldControl.numFire);
            numGold = fieldControl.numGold - Random.Range(0, fieldControl.numGold);
            numFeed = fieldControl.numFeed - Random.Range(0, fieldControl.numFeed);

            //총 개수
            int total = numFeed + numFire + numGold;

            float offset = 1f; //양옆,위아래 다른타일과 겹치지않게 띄우는 용도
            float unit = (thisSize.x - offset * 2) / total;

            
            for (int i = 0; i < total; i++)
            {
                //생성 + 아이템 리스트에 추가(제거할때 필요한 리스트)
                item = RandomItemCreate();
                FieldItemList.Add(item);

                //왼쪽아래 기준 좌표 생성 (sin그래프를 위해)
                Vector3 thisPos = this.transform.position - (thisSize / 2);

                // Sin함수 시작 지점(=z변의 길이의 반) * SIN함수 (총 거리의 배수 * 랜덤한 x값) + 원점이동값(=z변의 길이의 반)
                float numX = offset + unit * i;
                float numZ = (thisSize.z / 2 - offset *2) * Mathf.Sin(cycleMulti * numX) + (thisSize.z / 2);

                //생성 아이템 위치 조정
                FieldItemList[i].transform.position = new Vector3(thisPos.x + numX, 0f, thisPos.z + numZ);
            }
            yield return null;
        }
    }
}




/*
    int rand = Random.Range(0, 3);
    switch (rand)
    {
        case 0:
            if (numFire > 0)
            {
                item = Instantiate(goldPrefab, groupTrap.transform);
                numFire--;
            }
            break;

        case 1:
            if (numGold > 0)
            {
                item = Instantiate(goldPrefab, groupDrop.transform);
                numGold--;
            }
            break;

        case 2:
            if (numFeed > 0)
            {
                item = Instantiate(feedPrefab, groupDrop.transform);
                numFeed--;
            }
            break;
    }
*/
/*
                if (numFeed > 0)
                {
                    item = Instantiate(feedPrefab, groupDrop.transform);
                    numFeed--;
                }
                else if (numFire > 0)
                {
                    item = Instantiate(firePrefab, groupTrap.transform);
                    numFire--;
                }
                else if (numGold > 0)
                {
                    item = Instantiate(goldPrefab, groupDrop.transform);
                    numGold--;
                }
                else { Debug.Log("itemcreate Error"); }*/