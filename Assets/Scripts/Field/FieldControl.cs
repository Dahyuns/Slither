using UnityEngine;

namespace WiggleQuest
{
    public class FieldControl : MonoBehaviour
    {
        //추가구현
        //생성 기준 타일 'Create Field' 구역내에 Field Tile생성
        //맵타일이 맵타일 사이즈만큼 움직이면, Create Field가 Move
        //                 ㄴ 움직인 곳이 Field Tile 좌표와 동일한지 확인 (생성 위치 리스트에 저장? - 적절한 탐색 알고리즘 찾아보기), 없으면 생성

        enum TilePos
        {
            Upper,      //위
            Right,      //오른
            Left,       //왼
            Lower,       //아래 
            None
        }

        // [Field Tile 생성부분]

        //참조
        public FieldTile fieldTile;

        private float startPosX;          //  ㅣ
        private float midStartPosX;       //    ㅣ
        private float midEndPosX;         //      ㅣ
        private float endPosX;            //        ㅣ

        private float endPosY;            //  ㅡ ㅡ ㅡ
        private float midEndPosY;         //     ㅡ 
        private float midStartPosY;       //     ㅡ 
        private float startPosY;          //  ㅡ ㅡ ㅡ

        float TilePosX;
        float TilePosY;

        private void Start()
        {

        }

        private void Update()
        {
            TilePosX = fieldTile.transform.position.x;
            TilePosY = fieldTile.transform.position.y;
            //만약 맵타일이 움직이면 
            //if (MapTile.isMoving == true)
            {
                //그만큼 얘도 움직임 (크기가 다름, 얘도 체크하고 움직여야함)
                CreateTileMove();

                //체크후 필드 생성

                switch (Check())
                {
                    case TilePos.Upper:
                        CeateTile(TilePos.Upper);
                        break;
                    case TilePos.Right:
                        CeateTile(TilePos.Right);
                        break;
                    case TilePos.Left:
                        CeateTile(TilePos.Left);
                        break;
                    case TilePos.Lower:
                        CeateTile(TilePos.Lower);
                        break;
                    case TilePos.None:
                        break;

                }
            }
        }

        void CreateTileMove()
        {

        }

        void CeateTile(TilePos tilePos)
        {
            //위치에따른 생성
            FieldTile newfieldTile = Instantiate(fieldTile);
        }

        TilePos Check() 
        {
            //위
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

            else { return TilePos.None; }
        }

        /* 
        collider 닿으면 Create false반환?      */

    }
}
