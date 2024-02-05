using UnityEngine;

namespace Slither
{
    public class MapTile : MonoBehaviour
    {
        //맵타일 프리팹
        public GameObject mapTilePrefab;
        private float mapTileSize;

        // 맵 9개 정사각형 모양으로 만든후,
        // 가운데 타일을 기준으로 삼고.(계속 바뀜)
        // 가운데 타일 밖으로 상하좌우 어느쪽으로 가냐에 따라서 맵타일이 그쪽에 생성
        // 뒤에는 삭제
        // (( 4개로 해서 좌표받는 방법도 있을듯??

        public Transform cameraTransform;
        //3*3 배열 생성 후 그 위치에 맞게 (그리드UI사용) 프리팹 생성
        //가운데 기준(배열의 5번째?) 위치 넘어가면
        //재생성?
        //123
        //456
        //789
        // 1쪽으로 가면
        // 2쪽으로 가면
        // 3쪽으로 가면
        // 4쪽으로 가면
        // 5쪽으로 가면
        // 6쪽으로 가면
        // 7쪽으로 가면
        // 8쪽으로 가면
        // 9쪽으로 가면
        // 다시!

        void Start()
        {
        
        }
    }
}