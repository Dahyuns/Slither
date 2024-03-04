using UnityEngine;

namespace WiggleQuest
{
    public class FieldControl : MonoBehaviour
    {
        //추가구현
        //생성 기준 타일 'Create Field' 구역내에 Field Tile생성
        //맵타일이 맵타일 사이즈(현재4번)만큼 움직이면, Create Field가 Move
        //        ㄴ 움직인 곳이 Field Tile 좌표와 동일한지 확인 (생성 위치 리스트에 저장? - 적절한 탐색 알고리즘 찾아보기), 없으면 생성
        /*private float startPosX = 20f;  private float midPosX = 60f;   private float endPosX = 100f;
        //private float TilePosX; //TilePosX = fieldTilePrefab.transform.position.x;
        //private float TilePosY; //TilePosY = fieldTilePrefab.transform.position.y;*/

        // [Field Tile 생성부분]
        enum TilePos
        {
            Up, Down, Left, Right, None
        }

        //참조
        public GameObject fieldTilePrefab;
        public MapTile mapTileControl;
        public GameObject createTile;//테스트용

        //createTile의 좌표
        private Vector3[,] creatTilesPos = new Vector3[3,3];
        
        //맵사이즈와의 배율
        [SerializeField] private float mapSizeMag = 4;

        //createTile의 위치변경단위
        private float offset;

        TilePos tilePOSDir = TilePos.None;

        //초기화
        private void Start()
        {
            offset = 10 * mapSizeMag;
            Debug.Log(offset);
            //좌표생성(로컬, 왼쪽 아래로 부터 각 중심좌표) - 필드타일 생성위치
            //{(20f,100f) , (60f,100f), (100f,100f)}
            //{(20f,60f)  , (60f,60f),  (100f,60f)}
            //{(20f,20f)  , (60f,20f),  (100f,20f)}
            for (int y = 0; y < creatTilesPos.GetLength(0); y++)
            {
                for (int x = 0; x < creatTilesPos.GetLength(1); x++)
                {
                    float posNumX = 20 + (40 * x);
                    float posNumY = 20 + (40 * y);
                    creatTilesPos[x, y] = new Vector3(posNumX,-10f,posNumY);
                }
            }
        }

        private void Update()
        {
            //만약 맵타일이 움직이면 
            if (mapTileControl.isMapMoving == true)
            {
                //얼마나 움직였냐? : 4배크기만큼 움직였냐?
                if (MapMoveCountCheck() && tilePOSDir == TilePos.None)
                {
                    //움직였다면 얘도 움직임 + 얼마나 움직였는지 초기화
                    CreateTileMove();

                    //타일이 있는지 검사

                    //없으면 생성

                    //있으면 패스
                }
            }
        }


        //얼마나 움직였는지 체크, 어느방향인지 저장/ 맵 move카운트 초기화 (크기가 다름)
        bool MapMoveCountCheck()
        {
            if (mapTileControl.countX >= mapSizeMag)
            {
                tilePOSDir = TilePos.Right; 
                mapTileControl.countX -= (int)mapSizeMag;
                return true;
            }
            else if(mapTileControl.countX <= -mapSizeMag)
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

        void CreateTileMove()
        {
            Vector3 thisPosition = createTile.transform.position;
            //움직였다면 그만큼 얘도 움직임 + 얼마나 움직였는지 초기화
            switch (tilePOSDir)
            {
                case TilePos.Up:
                    thisPosition.z += offset;
                    break;
                case TilePos.Down:
                    thisPosition.z -= offset;
                    break;


                case TilePos.Right:
                    thisPosition.x += offset;
                    break;
                case TilePos.Left:
                    thisPosition.x -= offset;
                    break;

                case TilePos.None:
                    //MapMoveCountCheck()가 true면 none반환안함.
                    Debug.Log("Error : None반환됨!!");
                    break;
            }
            createTile.transform.position = thisPosition;

            //어디로 움직였는지 초기화 - none으로 재설정 , 예외가능성
            tilePOSDir = TilePos.None;
        }

        void CreateFieldTile()
        {
            for (int y = 0; y < creatTilesPos.GetLength(0); y++)
            {
                for (int x = 0; x < creatTilesPos.GetLength(1); x++)
                {
                    //if (createTiles[x, y].transform == null)
                    {
                        //움직인 곳에 필드타일 없는 부분만 생성
                        //CreateFieldTile();
                    }
                }
            }

            //코루틴?
            //각 타일의 위치(존재하는지) 체크후 필드 생성
            //위치에따른 생성
            GameObject newTile = Instantiate(fieldTilePrefab);
            FieldTile newfieldTile = newTile.GetComponent<FieldTile>();
        }



        /* collider
        */

    }
}
