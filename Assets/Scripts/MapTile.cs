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
        void Start()
        {
        
        }
    }
}