using UnityEngine;

namespace WiggleQuest
{
    // 가운데 타일을 기준으로 삼고 프리팹 생성. 상하좌우 어느쪽인가에 따라서 맵타일이 그쪽으로 이동
    // 그리드UI사용 
    public class MapTile : MonoBehaviour
    {
        //맵타일 프리팹
        public GameObject mapTilePrefab;

        //참조할 목표
        public Transform targetTransform;

        //이 캔버스의 위치정보
        private RectTransform thisRect;

        //맵 개수
        [SerializeField] private int mapTileX = 4;
        [SerializeField] private int mapTileY = 4;

        //맵 사이즈 (:간격)에 따라 이동
        [SerializeField] private float offset = 10f;

        //타겟과의 거리 - 매프레임 업데이트
        private float targetDisX;
        private float targetDisZ;

        public int countX = 0;
        public int countY = 0;

        public bool isMapMoving = false;
        
        void Start()
        {
            thisRect = GetComponent<RectTransform>();
            #region 맵 생성
            for (int y = 0; y < mapTileY; y++)
            {
                for (int x = 0; x < mapTileX; x++)
                {
                    Instantiate(mapTilePrefab, thisRect);
                }
            }
            #endregion
        }

        private void Update()
        {
            //타겟과의 거리 = 타겟 위치 - 맵(this) 위치
            targetDisX = targetTransform.position.x - this.transform.position.x;
            targetDisZ = targetTransform.position.z - this.transform.position.z;

            //이동거리가 offset이상이라면
            if (CameraMoveCheck())
            {
                //맵 이동
                MapMoving();
                isMapMoving = true;
            }
            else
            {
                isMapMoving = false;
            }
        }

        //이동거리가 offset이상이라면
        public bool CameraMoveCheck()
        {
            if (targetDisX >= offset || targetDisX <= -offset || targetDisZ >= offset || targetDisZ <= -offset)
                return true;
            else
                return false;
        }

        private void MapMoving()
        {
            //캔버스의 현재위치
            Vector3 thisPos = thisRect.position;
            #region offset만큼 추가
            if (targetDisX >= offset)
            {
                thisPos.x += offset;
                countX++;
            }
            else if (targetDisX <= -offset)
            {
                thisPos.x -= offset;
                countX--;
            }

            if (targetDisZ >= offset)
            {
                thisPos.z += offset;
                countY++;
            }
            else if (targetDisZ <= -offset)
            {
                thisPos.z -= offset;
                countY--;
            }
            #endregion

            thisRect.position = thisPos; 
        }
    }
}