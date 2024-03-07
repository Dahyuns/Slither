using System.Collections.Generic;
using UnityEngine;

namespace WiggleQuest
{
    enum TilePos //타일 move시 쓰임
    {
        Up, Down, Left, Right, None
    }

    public class FieldControl : MonoBehaviour
    {
        // [Create Tile 이동 및 위치에 따른 Field Tile 생성]

        //참조
        public MapTile mapTileControl; //이동거리 계산을 위한 참조
        public GameObject fieldTilePrefab;
        public Transform fieldTileParent; //필드타일묶음

        //생성위치의 중앙좌표
        private Vector3 CreateTileCenter = new Vector3(60f, -10f, 60f);

        //중앙좌표로부터의 각 좌표 - 필드타일 생성위치, 중앙좌표 'CreateTileCenter'으로부터의 거리(로컬)
        private Vector3[] creatTilesPos = new Vector3[]
        { new Vector3(-40f, 0f, 40f), new Vector3(0f, 0f, 40f), new Vector3(40f, 0f, 40f),
          new Vector3(-40f, 0f, 0),                             new Vector3(40f, 0f, 0f) ,
          new Vector3(-40f, 0f, -40f),new Vector3(0f, 0f, -40f),new Vector3(40f, 0f, -40f) };

        //FieldTile위치 저장 - FieldTile삭제시 같이 삭제!
        public List<Vector3> fieldTiles = new List<Vector3>();

        //맵사이즈와의 배율
        [SerializeField] private float mapSizeMag = 4;

        private float offset; //CreateTile위치변경단위

        private TilePos tilePOSDir; //CreateTile 이동 방향, None은 빈값

        public bool isCreateTileMove = false;

        private void Start()
        {
            offset = 10 * mapSizeMag;
            tilePOSDir = TilePos.None;

            //처음 위치에 생성 : 중앙 1개 + 측면 8개
            CreateFieldTile(CreateTileCenter);
            CheckNCreateField();
        }

        private void Update()
        {
            //none으로 다시 초기화 전까지 실행X(코루틴사용한다면?) 
            if (tilePOSDir == TilePos.None) // mapTileControl.isMapMoving == true && 
            {
                //만약 맵이 맵크기의 4배만큼 움직였다면?
                if (CheckMoveCount())
                {
                    //CreateTile도 움직임 + 얼마나 움직였는지 초기화
                    MoveCreateTile();

                    //움직인 위치 기준으로 그곳에 FieldTile이 있는지 확인 후
                    //'없다면' 생성
                    CheckNCreateField();
                    isCreateTileMove = true;
                }
                else
                    isCreateTileMove = false;

            }
        }


        //얼마나 움직였는지 체크, 어느방향인지 저장/ 맵 move카운트 초기화 (크기가 다름)
        bool CheckMoveCount()
        {
            if (mapTileControl.countX >= mapSizeMag)
            {
                tilePOSDir = TilePos.Right;
                mapTileControl.countX -= (int)mapSizeMag;
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
            //움직였다면 그만큼 얘도 움직임 + 얼마나 움직였는지 초기화
            switch (tilePOSDir)
            {
                case TilePos.Up:
                    CreateTileCenter.z += offset;
                    break;
                case TilePos.Down:
                    CreateTileCenter.z -= offset;
                    break;


                case TilePos.Right:
                    CreateTileCenter.x += offset;
                    break;
                case TilePos.Left:
                    CreateTileCenter.x -= offset;
                    break;
            }

            //어디로 움직였는지 초기화 - none으로 재설정 , 예외가능성
            tilePOSDir = TilePos.None;
        }

        void CheckNCreateField()
        {
            for (int i = 0; i < creatTilesPos.Length; i++) //createTile 위치 개수
            {
                //타일의 월드 위치 = 월드내 중심 + 각 로컬 위치
                Vector3 eachTilePos = CreateTileCenter + creatTilesPos[i];

                if (CheckFieldTile(eachTilePos)) //해당 위치에 타일이 있는지 검사
                {
                    CreateFieldTile(eachTilePos); //없으면 생성
                }
            }
        }
        
        //true반환시 field Tile 생성
        bool CheckFieldTile(Vector3 newTilePos) 
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

        void CreateFieldTile(Vector3 newTilePos)
        {
            GameObject newTile = Instantiate(fieldTilePrefab, newTilePos, Quaternion.identity,fieldTileParent);
            FieldTile newfieldTile = newTile.GetComponent<FieldTile>();

            if (newfieldTile != null) //제대로 생성됐다면
            {
                //리스트에 추가
                fieldTiles.Add(newTilePos);
                //필드내 아이템들 생성
                //newfieldTile.CreateItems();
            }
        }
    }
}
