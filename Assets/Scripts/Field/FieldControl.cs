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
            Lower       //아래 
        }

        // [Field Tile 생성부분]



        //모든 좌표를 검사 - 겹치는 좌표 존재, 변수 많아짐.. 어쩌지?

        //upper 좌표 4개?
        private float createPosX1;
        private float createPosX2;
        private float createPosY1;
        private float createPosY2;
        //Right 좌표 4개?
        //Left  좌표 4개?
        //Lower 좌표 4개?


        //가독성 떨어지지 않고 가장 효율적인 방법을 찾고싶다!!!!!!!!!!!!!!!!!!!!!

        /* 
        collider 닿으면 Create false반환?      */

    }
}
