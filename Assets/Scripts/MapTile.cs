using UnityEngine;

namespace WiggleQuest
{
    public class MapTile : MonoBehaviour
    {
        //��Ÿ�� ������
        public GameObject mapTilePrefab;

        [SerializeField] private int mapTileX = 3;
        [SerializeField] private int mapTileY = 3;
        private GameObject[,] tiles;

        private float offset;
        private float beforePosX;
        private float beforePosY;


        // �� 9�� ���簢�� ������� ������,
        // ��� Ÿ���� �������� ���.(��� �ٲ�)
        // ��� Ÿ�� ������ �����¿� ��������� ���Ŀ� ���� ��Ÿ���� ���ʿ� ����
        // �ڿ��� ����
        // (( 4���� �ؼ� ��ǥ�޴� ����� ������??

        public Transform cameraTransform;
        //3*3 �迭 ���� �� �� ��ġ�� �°� (�׸���UI���) ������ ����
        //��� ����(�迭�� 5��°?) ��ġ �Ѿ��
        //�����?
        //123
        //456
        //789
        // 1������ ����
        // 2������ ����
        // 3������ ����
        // 4������ ����
        // 5������ ����
        // 6������ ����
        // 7������ ����
        // 8������ ����
        // 9������ ����
        // �ٽ�!

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
            //offset = transsform; //�������
        }

        private void Update()
        {
            MapCheckNMovig();
        }

        private void MapCheckNMovig()
        {
            beforePosX = cameraTransform.position.x;
            float disX = cameraTransform.position.x - beforePosX; //�̵��� �Ÿ�
            if (disX >= 10) //�̵��Ÿ��� 10�̻��̶��?
            {
                //beforePosX = ������ġ
                //��浵 10��ŭ +
            }
            else if (disX <= -10) //�̵��Ÿ��� -10�̻��̶��?
            {
                //beforePosX = ������ġ
                //��浵 10��ŭ -
            }

            beforePosY = cameraTransform.position.y;
        }
    }
}