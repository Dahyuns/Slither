using UnityEngine;

namespace WiggleQuest
{
    public class FieldControl : MonoBehaviour
    {
        //추가구현
        //생성 기준 타일 'Create Field' 구역내에 Field Tile생성
        //맵타일이 맵타일 사이즈(현재4번)만큼 움직이면, Create Field가 Move
        //                 ㄴ 움직인 곳이 Field Tile 좌표와 동일한지 확인 (생성 위치 리스트에 저장? - 적절한 탐색 알고리즘 찾아보기), 없으면 생성

        enum TilePos
        {
            /*UpRight,
            UpMid,
            UpLeft,

            MidRight,
            MidLeft,

            DownRight,
            DowmMid,
            DowmLeft,*/
            Up,          //위
            Right,       //오른
            Left,        //왼
            Down,        //아래 
            None
        }

        // [Field Tile 생성부분]

        //참조
        public GameObject fieldTilePrefab;
        public GameObject gridCanvas;
        public Transform Transform;

        private GameObject[] createTiles;
        private MapTile mapTileControl;


        //createTile
        private Vector2[,] creatTilePos = new Vector2[3,3];
        private float startPosX = 20f;
        private float startPosY = 20f;
        private float midPosX = 60f;
        private float midPosY = 60f;
        private float endPosX = 100f;
        private float endPosY = 100f;

        //private float TilePosX; //TilePosX = fieldTilePrefab.transform.position.x;
        //private float TilePosY; //TilePosY = fieldTilePrefab.transform.position.y;

        //맵사이즈의 배율
        [SerializeField] private float mapSizeMag = 4;
        private float offset;

        TilePos tilePOS = TilePos.None;

        //초기화
        private void Start()
        {
            offset = 10 * mapSizeMag;
            createTiles = transform.GetComponentsInChildren<GameObject>();
            mapTileControl = gridCanvas.GetComponent<MapTile>();

            //createTiles
            //{(20f,100f) , (60f,100f), (100f,100f)}
            //{(20f,60f)  , (60f,60f),  (100f,60f)}
            //{(20f,20f)  , (60f,20f),  (100f,20f)}
            for (int y = 0; y < createTiles.GetLength(0); y++)
            {
                for (int x = 0; x < createTiles.GetLength(1); x++)
                {
                    float posNumX = 20 + (40 * x);
                    float posNumY = 20 + (40 * y);
                    creatTilePos[x, y] = new Vector2(posNumX,posNumY);
                }
            }
        }

        private void Update()
        {
            //만약 맵타일이 움직이면 //얼마나 움직였냐? : 4배크기만큼 움직였냐?
            if (mapTileControl.isMapMoving == true)
            {
                if (TilePosCheck())
                {
                    //움직였다면 그만큼 얘도 움직임 + 얼마나 움직였는지 초기화
                    CreateTileMove();

                    //움직인 곳에 필드타일 없는 부분만 생성
                    CreateFieldTile();
                }
            }
        }

        //얼마나 움직였는지 체크 (크기가 다름, 체크하고 움직여야함)
        bool TilePosCheck()
        {
            if (mapTileControl.countX == 4)
            {
                tilePOS = TilePos.Right;
                return true;
            }
            else if (mapTileControl.countX == -4)
            {
                tilePOS = TilePos.Left;
                return true;
            }
            else if (mapTileControl.countY == 4)
            {
                tilePOS = TilePos.Up;
                return true;
            }
            else if (mapTileControl.countY == -4)
            {
                tilePOS = TilePos.Down;
                return true;
            }
            else
            {
                return false;
            }
            /*//위
            if (TilePosX > startPosX && TilePosX < endPosX &&
                TilePosY > midEndPosY && TilePosX < endPosY)
            {
                return TilePos.Upper;
            }


            //오른
            else if (TilePosX > midEndPosX && TilePosX < endPosX &&
                    TilePosY > startPosY && TilePosX < endPosY)
            {
                return TilePos.Right;
            }


            //왼
            else if (TilePosX > startPosX && TilePosX < midStartPosX &&
                    TilePosY > startPosY && TilePosX < endPosY)
            {
                return TilePos.Left;
            }


            //아래
            else if (TilePosX > startPosX && TilePosX < endPosX &&
                    TilePosY > startPosY && TilePosX < midStartPosY)
            {
                return TilePos.Lower;
            }

            else { return TilePos.None; }*/
        }

        void CreateTileMove()
        {
            //맵타일 위치로 바로 이동
            //this.transform.position = gridCanvas.transform.position;

            //움직였다면 그만큼 얘도 움직임 + 얼마나 움직였는지 초기화//코루틴
            switch (tilePOS)
            {
                case TilePos.Up:

                    //얼마나 움직였는지 초기화
                    mapTileControl.countY = 0;
                    break;
                case TilePos.Down:

                    //얼마나 움직였는지 초기화
                    mapTileControl.countY = 0;
                    break;


                case TilePos.Right:

                    //얼마나 움직였는지 초기화
                    mapTileControl.countX = 0;
                    break;
                case TilePos.Left:

                    //얼마나 움직였는지 초기화
                    mapTileControl.countX = 0;
                    break;

                case TilePos.None:
                    break;

            }

            //어디로 움직였는지 초기화
            tilePOS = TilePos.None;
        }

        void CreateFieldTile()
        {
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
