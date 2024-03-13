using UnityEngine;

namespace WiggleQuest
{
    // ��� Ÿ���� �������� ��� ������ ����. �����¿� ������ΰ��� ���� ��Ÿ���� �������� �̵�
    // �׸���UI��� 
    public class MapTile : MonoBehaviour
    {
        //��Ÿ�� ������
        public GameObject mapTilePrefab;

        //������ ��ǥ
        public Transform targetTransform;

        //�� ĵ������ ��ġ����
        private RectTransform thisRect;

        //�� ����
        [SerializeField] private int mapTileX = 4;
        [SerializeField] private int mapTileY = 4;

        //�� ������ (:����)�� ���� �̵�
        [SerializeField] private float offset = 10f;

        //Ÿ�ٰ��� �Ÿ� - �������� ������Ʈ
        private float targetDisX;
        private float targetDisZ;

        public int countX = 0;
        public int countY = 0;

        public bool isMapMoving = false;
        
        void Start()
        {
            thisRect = GetComponent<RectTransform>();
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
            //Ÿ�ٰ��� �Ÿ� = Ÿ�� ��ġ - ��(this) ��ġ
            targetDisX = targetTransform.position.x - this.transform.position.x;
            targetDisZ = targetTransform.position.z - this.transform.position.z;

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
            if (targetDisX >= offset || targetDisX <= -offset || targetDisZ >= offset || targetDisZ <= -offset)
                return true;
            else
                return false;
        }

        private void MapMoving()
        {
            //ĵ������ ������ġ
            Vector3 thisPos = thisRect.position;
            #region offset��ŭ �߰�
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