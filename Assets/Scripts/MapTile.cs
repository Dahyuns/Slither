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

        //�񱳴�� - ����ġ
        private float camerabeforePosX;
        private float camerabeforePosZ;

        //ī�޶��� �̵��Ÿ� - �������� ������Ʈ
        private float cameradisX;
        private float cameradisZ;


        // �� 9�� ���簢�� ������� ������,
        // ��� Ÿ���� �������� ���.(��� �ٲ�)
        // ��� Ÿ�� ������ �����¿� ��������� ���Ŀ� ���� ��Ÿ���� ���ʿ� ����
        // �ڿ��� ����
        // (( 4���� �ؼ� ��ǥ�޴� ����� ������??

        //3*3 �迭 ���� �� �� ��ġ�� �°� (�׸���UI���) ������ ����
        //��� ����(�迭�� 5��°?) ��ġ �Ѿ��
        //�����?

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
            if (MapChecking())
            {
                //�� �̵�
                MapMoving();
            }
        }

        //�̵��Ÿ��� offset�̻��̶��
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
            //ĵ������ ������ġ
            Vector3 thisPos = thisRect.position;
            #region offset��ŭ �߰�
            if (cameradisX >= offset)
            {
                thisPos.x += offset;
                //������ġ = ������ġ ����
                camerabeforePosX = cameraTransform.position.x;
            }
            else if (cameradisX <= -offset)
            {
                thisPos.x -= offset;
                //������ġ = ������ġ ����
                camerabeforePosX = cameraTransform.position.x;
            }
            if (cameradisZ >= offset)
            {
                thisPos.z += offset;
                //������ġ = ������ġ ����
                camerabeforePosZ = cameraTransform.position.z;
            }
            else if (cameradisZ <= -offset)
            {
                thisPos.z -= offset;
                //������ġ = ������ġ ����
                camerabeforePosZ = cameraTransform.position.z;
            }
            #endregion

            thisRect.position = thisPos; //ĵ����������
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