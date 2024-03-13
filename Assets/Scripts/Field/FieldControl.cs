using System.Collections.Generic;
using UnityEngine;

namespace WiggleQuest
{
    enum TilePos //타일 move시 쓰임
    {
        Up, Down, Left, Right, None//초기화용
    }

    // Create Tile 이동 및 위치에 따른 Field Tile 생성
    public class FieldControl : MonoBehaviour
    {
        //참조
        public MapTile mapTileControl; //이동거리 계산을 위한 참조
        public GameObject fieldTilePrefab;
        public Transform fieldTileParent; //Hierarchy내 필드타일 묶음

        //[CreateTile 생성,삭제]
        [SerializeField] private float mapSizeMag = 4;          //맵사이즈와의 배율
        private float offset;                                   //CreateTile 이동 크기 (1번당)
        private TilePos tilePOSDir;                             //CreateTile 이동 방향, None은 빈값

        public bool isCreateTileMove = false;                   //CreateTile 이동 여부

        //[FieldTile 생성, 삭제]
        private Vector3 createTileCenter;                       //생성위치의 중앙좌표

        private Vector3[] creatTilesPos = new Vector3[]         //필드타일 생성위치 : 중앙좌표로부터의 좌표(로컬)
        { new Vector3(-40f, 0f, 40f), new Vector3(0f, 0f, 40f), new Vector3(40f, 0f, 40f),
          new Vector3(-40f, 0f, 0),                             new Vector3(40f, 0f, 0f) ,
          new Vector3(-40f, 0f, -40f),new Vector3(0f, 0f, -40f),new Vector3(40f, 0f, -40f) };     

        public List<Vector3> fieldTiles = new List<Vector3>();  //FieldTile 위치 저장 (FieldTile삭제시 같이 삭제됨)
        public float standardDis = 1000f;                       // FieldTile 삭제 기준 거리

        //FieldTile 하나당 아이템 생성 최대 개수 (수량 조절 가능)
        public int numFire = 5;
        public int numGold = 1;
        public int numFeed = 4;

        private void Start()
        {
            offset = 10 * mapSizeMag;
            tilePOSDir = TilePos.None;
            createTileCenter = new Vector3(60f, -10f, 60f); //생성위치의 중앙좌표

            //[첫 위치 FieldTile 생성] : 중앙 1개 + 측면 8개
            CreateField(createTileCenter);
            CheckNCreate();
        }

        private void Update()
        {
            //none으로 다시 초기화 전까지 실행X
            if (tilePOSDir == TilePos.None)
            {
                //만약 맵이 맵크기의 4배만큼 움직였다면?
                if (CheckMoveCount())
                {
                    //CreateTile도 움직임 + 얼마나 움직였는지(Count) 초기화
                    MoveCreateTile();

                    //움직인 위치 기준으로 그곳에 FieldTile이 있는지 확인 후, '없다면' 생성
                    CheckNCreate();

                    isCreateTileMove = true;
                }
                else
                    isCreateTileMove = false;
            }
        }


         
        bool CheckMoveCount()
        {
            if (mapTileControl.countX >= mapSizeMag)        //얼마나 움직였는지 체크
            {
                tilePOSDir = TilePos.Right;                 //어느방향인지 저장
                mapTileControl.countX -= (int)mapSizeMag;   //맵 move카운트 초기화 (크기가 다름)
                return true;
            }
            else if (mapTileControl.countX <= -mapSizeMag)
            {
                tilePOSDir = TilePos.Left;
                mapTileControl.countX += (int)mapSizeMag;
                return true;
            }
            else if (mapTileControl.countY >= mapSizeMag)
            {
                tilePOSDir = TilePos.Up;
                mapTileControl.countY -= (int)mapSizeMag;
                return true;
            }
            else if (mapTileControl.countY <= -mapSizeMag)
            {
                tilePOSDir = TilePos.Down;
                mapTileControl.countY += (int)mapSizeMag;
                return true;
            }
            else
            {
                return false;
            }
        }

        void MoveCreateTile()
        {
            switch (tilePOSDir) //방향에따라 이동
            {
                case TilePos.Up:
                    createTileCenter.z += offset;
                    break;
                case TilePos.Down:
                    createTileCenter.z -= offset;
                    break;

                case TilePos.Right:
                    createTileCenter.x += offset;
                    break;
                case TilePos.Left:
                    createTileCenter.x -= offset;
                    break;
            }

            //어디로 움직였는지 초기화 - none으로 재설정
            tilePOSDir = TilePos.None;
        }

        void CheckNCreate()
        {
            for (int i = 0; i < creatTilesPos.Length; i++) //createTile 위치 개수
            {
                //타일의 월드 위치 = 월드내 중심 + 각 로컬 위치
                Vector3 eachTilePos = createTileCenter + creatTilesPos[i];

                if (CheckNullField(eachTilePos)) //해당 위치에 타일이 있는지 검사
                { 
                    CreateField(eachTilePos); //없으면 생성
                }
            }
        }

        //true반환시 field Tile 생성
        bool CheckNullField(Vector3 newTilePos)
        {
            //저장된 좌표(만들어진 타일의 좌표)와 동일하다면 만들지X
            for (int i = 0; i < fieldTiles.Count; i++)
            {
                if (fieldTiles[i] == newTilePos)
                {
                    return false;
                }
            }

            //저장된 좌표에 새로운 좌표가 없으면 만든다.
            return true;
        }

        void CreateField(Vector3 newTilePos)
        {
            GameObject newTile = Instantiate(fieldTilePrefab, newTilePos, Quaternion.identity, fieldTileParent);
            FieldTile newfieldTile = newTile.GetComponent<FieldTile>();

            if (newfieldTile != null) //제대로 생성됐다면
            {
                //리스트에 추가
                fieldTiles.Add(newTilePos);
            }
        }
    }
}