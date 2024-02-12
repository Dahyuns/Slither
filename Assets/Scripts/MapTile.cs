using UnityEngine;

namespace WiggleQuest
{
    /*public enum MoveXY
    {
        X_Plus, X_Minus,
        Z_Plus, Z_Minus,
        None
    }*/
    public class MapTile : MonoBehaviour
    {
        //맵타일 프리팹
        public GameObject mapTilePrefab;

        //참조할 카메라
        public Transform cameraTransform;

        //이 캔버스의 위치정보
        private RectTransform thisRect;

        //맵 개수
        [SerializeField] private int mapTileX = 4;
        [SerializeField] private int mapTileY = 4;
        //맵 사이즈 (:간격)에 따라 이동
        [SerializeField] private float offset = 10f;

        //비교대상 - 전위치
        private float camerabeforePosX;
        private float camerabeforePosZ;

        //카메라의 이동거리 - 매프레임 업데이트
        private float cameradisX;
        private float cameradisZ;


        // 맵 9개 정사각형 모양으로 만든후,
        // 가운데 타일을 기준으로 삼고.(계속 바뀜)
        // 가운데 타일 밖으로 상하좌우 어느쪽으로 가냐에 따라서 맵타일이 그쪽에 생성
        // 뒤에는 삭제
        // (( 4개로 해서 좌표받는 방법도 있을듯??

        //3*3 배열 생성 후 그 위치에 맞게 (그리드UI사용) 프리팹 생성
        //가운데 기준(배열의 5번째?) 위치 넘어가면
        //재생성?

        void Start()
        {
            thisRect = GetComponent<RectTransform>();
            //시작 카메라 위치 저장
            camerabeforePosX = cameraTransform.position.x;
            camerabeforePosZ = cameraTransform.position.z;
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
            //이동거리 = 현재위치 - 이전 위치
            cameradisX = cameraTransform.position.x - camerabeforePosX;
            cameradisZ = cameraTransform.position.z - camerabeforePosZ;

            //이동거리가 offset이상이라면
            if (MapChecking())
            {
                //맵 이동
                MapMoving();
            }
        }

        //이동거리가 offset이상이라면
        private bool MapChecking()
        {
            if (cameradisX >= offset || cameradisX <= -offset || cameradisZ >= offset || cameradisZ <= -offset)
                return true;
            else
                return false;
            /*if (cameradisX >= 10f) return MoveXY.X_Plus;
            else if (cameradisX <= -10f) return MoveXY.X_Minus;
            else if (cameradisZ >= 10f) return MoveXY.Z_Plus;
            else if (cameradisZ <= -10f) return MoveXY.Z_Minus;
            else return MoveXY.None;*/
        }

        private void MapMoving()
        {
            //캔버스의 현재위치
            Vector3 thisPos = thisRect.position;
            #region offset만큼 추가
            if (cameradisX >= offset)
            {
                thisPos.x += offset;
                //이전위치 = 현재위치 저장
                camerabeforePosX = cameraTransform.position.x;
            }
            else if (cameradisX <= -offset)
            {
                thisPos.x -= offset;
                //이전위치 = 현재위치 저장
                camerabeforePosX = cameraTransform.position.x;
            }
            if (cameradisZ >= offset)
            {
                thisPos.z += offset;
                //이전위치 = 현재위치 저장
                camerabeforePosZ = cameraTransform.position.z;
            }
            else if (cameradisZ <= -offset)
            {
                thisPos.z -= offset;
                //이전위치 = 현재위치 저장
                camerabeforePosZ = cameraTransform.position.z;
            }
            #endregion

            thisRect.position = thisPos; //캔버스에적용
            /*//if (whichMoving == MoveXY.None) return;
            switch (whichMoving)
            {
                case MoveXY.X_Plus:
                    thisPos.x += 10f;
                    break;

                case MoveXY.X_Minus:
                    thisPos.x -= 10f;
                    break;

                case MoveXY.Z_Plus:
                    thisPos.z += 10f;
                    break;

                case MoveXY.Z_Minus:
                    thisPos.z -= 10f;
                    break;
            }*/
        }
    }
}