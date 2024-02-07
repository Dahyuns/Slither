using UnityEngine;

namespace WiggleQuest
{
    public class MapTile : MonoBehaviour
    {
        //맵타일 프리팹
        public GameObject mapTilePrefab;

        [SerializeField] private int mapTileX = 3;
        [SerializeField] private int mapTileY = 3;
        private GameObject[,] tiles;

        private float offset;
        private float beforePosX;
        private float beforePosY;


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
            tiles = new GameObject[mapTileX, mapTileY];
            for (int y = 0; y < mapTileY; y++)
            {
                for (int x = 0; x < mapTileX; x++)
                {
                    GameObject tile = Instantiate(mapTilePrefab, this.transform);
                    tiles[x, y] = tile;
                }
            }       
            Transform transsform = mapTilePrefab.transform;
            //offset = transsform; //여기부터
        }

        private void Update()
        {
            MapCheckNMovig();
        }

        private void MapCheckNMovig()
        {
            beforePosX = cameraTransform.position.x;
            float disX = cameraTransform.position.x - beforePosX; //이동한 거리
            if (disX >= 10) //이동거리가 10이상이라면?
            {
                //beforePosX = 현재위치
                //배경도 10만큼 +
            }
            else if (disX <= -10) //이동거리가 -10이상이라면?
            {
                //beforePosX = 현재위치
                //배경도 10만큼 -
            }

            beforePosY = cameraTransform.position.y;
        }
    }
}