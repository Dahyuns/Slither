using UnityEngine;

namespace WiggleQuest
{
    // ��� Ÿ���� �������� ��� ������ ����. �����¿� ������ΰ��� ���� ��Ÿ���� �������� �̵�
    // �׸���UI��� 
    public class MapTile : MonoBehaviour
    {
        //��Ÿ�� ������
        public GameObject mapTilePrefab;

        //������ ī�޶�
        public Transform cameraTransform;

        //�� ĵ������ ��ġ����
        private RectTransform thisRect;

        //�� ����
        [SerializeField] private int mapTileX = 4;
        [SerializeField] private int mapTileY = 4;
        //�� ������ (:����)�� ���� �̵�
        [SerializeField] private float offset = 10f;

        //�񱳴�� - ����ġ, ������ �Ǵ� ��ġ
        private float camerabeforePosX;
        private float camerabeforePosZ;

        //ī�޶��� �̵��Ÿ� - �������� ������Ʈ
        private float cameradisX;
        private float cameradisZ;

        public int countX = 0;
        public int countY = 0;

        public bool isMapMoving = false;
        
        void Start()
        {
            thisRect = GetComponent<RectTransform>();
            //���� ī�޶� ��ġ ����
            camerabeforePosX = cameraTransform.position.x;
            camerabeforePosZ = cameraTransform.position.z;
            #region �� ����
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
            //�̵��Ÿ� = ������ġ - ���� ��ġ
            cameradisX = cameraTransform.position.x - camerabeforePosX;
            cameradisZ = cameraTransform.position.z - camerabeforePosZ;

            //�̵��Ÿ��� offset�̻��̶��
            if (CameraMoveCheck())
            {
                //�� �̵�
                MapMoving();
                isMapMoving = true;
            }
            else
            {
                isMapMoving = false;
            }
        }

        //�̵��Ÿ��� offset�̻��̶��
        public bool CameraMoveCheck()
        {
            if (cameradisX >= offset || cameradisX <= -offset || cameradisZ >= offset || cameradisZ <= -offset)
                return true;
            else
                return false;
        }

        private void MapMoving()
        {
            //ĵ������ ������ġ
            Vector3 thisPos = thisRect.position;
            #region offset��ŭ �߰�
            if (cameradisX >= offset)
            {
                thisPos.x += offset;
                //������ġ = ������ġ ����
                camerabeforePosX = cameraTransform.position.x;
                countX++;
            }
            else if (cameradisX <= -offset)
            {
                thisPos.x -= offset;
                //������ġ = ������ġ ����
                camerabeforePosX = cameraTransform.position.x;
                countX--;
            }

            if (cameradisZ >= offset)
            {
                thisPos.z += offset;
                //������ġ = ������ġ ����
                camerabeforePosZ = cameraTransform.position.z;
                countY++;
            }
            else if (cameradisZ <= -offset)
            {
                thisPos.z -= offset;
                //������ġ = ������ġ ����
                camerabeforePosZ = cameraTransform.position.z;
                countY--;
            }
            #endregion

            thisRect.position = thisPos; 
        }
    }
}