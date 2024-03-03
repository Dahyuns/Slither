using UnityEngine;

namespace WiggleQuest
{
    public class FieldControl : MonoBehaviour
    {
        //�߰�����
        //���� ���� Ÿ�� 'Create Field' �������� Field Tile����
        //��Ÿ���� ��Ÿ�� ������(����4��)��ŭ �����̸�, Create Field�� Move
        //                 �� ������ ���� Field Tile ��ǥ�� �������� Ȯ�� (���� ��ġ ����Ʈ�� ����? - ������ Ž�� �˰��� ã�ƺ���), ������ ����

        enum TilePos
        {
            /*UpRight,
            UpMid,
            UpLeft,

            MidRight,
            MidLeft,

            DownRight,
            DowmMid,
            DowmLeft,*/
            Up,          //��
            Right,       //����
            Left,        //��
            Down,        //�Ʒ� 
            None
        }

        // [Field Tile �����κ�]

        //����
        public GameObject fieldTilePrefab;
        public GameObject gridCanvas;
        public Transform Transform;

        private GameObject[] createTiles;
        private MapTile mapTileControl;


        //createTile
        private Vector2[,] creatTilePos = new Vector2[3,3];
        private float startPosX = 20f;
        private float startPosY = 20f;
        private float midPosX = 60f;
        private float midPosY = 60f;
        private float endPosX = 100f;
        private float endPosY = 100f;

        //private float TilePosX; //TilePosX = fieldTilePrefab.transform.position.x;
        //private float TilePosY; //TilePosY = fieldTilePrefab.transform.position.y;

        //�ʻ������� ����
        [SerializeField] private float mapSizeMag = 4;
        private float offset;

        TilePos tilePOS = TilePos.None;

        //�ʱ�ȭ
        private void Start()
        {
            offset = 10 * mapSizeMag;
            createTiles = transform.GetComponentsInChildren<GameObject>();
            mapTileControl = gridCanvas.GetComponent<MapTile>();

            //createTiles
            //{(20f,100f) , (60f,100f), (100f,100f)}
            //{(20f,60f)  , (60f,60f),  (100f,60f)}
            //{(20f,20f)  , (60f,20f),  (100f,20f)}
            for (int y = 0; y < createTiles.GetLength(0); y++)
            {
                for (int x = 0; x < createTiles.GetLength(1); x++)
                {
                    float posNumX = 20 + (40 * x);
                    float posNumY = 20 + (40 * y);
                    creatTilePos[x, y] = new Vector2(posNumX,posNumY);
                }
            }
        }

        private void Update()
        {
            //���� ��Ÿ���� �����̸� //�󸶳� ��������? : 4��ũ�⸸ŭ ��������?
            if (mapTileControl.isMapMoving == true)
            {
                if (TilePosCheck())
                {
                    //�������ٸ� �׸�ŭ �굵 ������ + �󸶳� ���������� �ʱ�ȭ
                    CreateTileMove();

                    //������ ���� �ʵ�Ÿ�� ���� �κи� ����
                    CreateFieldTile();
                }
            }
        }

        //�󸶳� ���������� üũ (ũ�Ⱑ �ٸ�, üũ�ϰ� ����������)
        bool TilePosCheck()
        {
            if (mapTileControl.countX == 4)
            {
                tilePOS = TilePos.Right;
                return true;
            }
            else if (mapTileControl.countX == -4)
            {
                tilePOS = TilePos.Left;
                return true;
            }
            else if (mapTileControl.countY == 4)
            {
                tilePOS = TilePos.Up;
                return true;
            }
            else if (mapTileControl.countY == -4)
            {
                tilePOS = TilePos.Down;
                return true;
            }
            else
            {
                return false;
            }
            /*//��
            if (TilePosX > startPosX && TilePosX < endPosX &&
                TilePosY > midEndPosY && TilePosX < endPosY)
            {
                return TilePos.Upper;
            }


            //����
            else if (TilePosX > midEndPosX && TilePosX < endPosX &&
                    TilePosY > startPosY && TilePosX < endPosY)
            {
                return TilePos.Right;
            }


            //��
            else if (TilePosX > startPosX && TilePosX < midStartPosX &&
                    TilePosY > startPosY && TilePosX < endPosY)
            {
                return TilePos.Left;
            }


            //�Ʒ�
            else if (TilePosX > startPosX && TilePosX < endPosX &&
                    TilePosY > startPosY && TilePosX < midStartPosY)
            {
                return TilePos.Lower;
            }

            else { return TilePos.None; }*/
        }

        void CreateTileMove()
        {
            //��Ÿ�� ��ġ�� �ٷ� �̵�
            //this.transform.position = gridCanvas.transform.position;

            //�������ٸ� �׸�ŭ �굵 ������ + �󸶳� ���������� �ʱ�ȭ//�ڷ�ƾ
            switch (tilePOS)
            {
                case TilePos.Up:

                    //�󸶳� ���������� �ʱ�ȭ
                    mapTileControl.countY = 0;
                    break;
                case TilePos.Down:

                    //�󸶳� ���������� �ʱ�ȭ
                    mapTileControl.countY = 0;
                    break;


                case TilePos.Right:

                    //�󸶳� ���������� �ʱ�ȭ
                    mapTileControl.countX = 0;
                    break;
                case TilePos.Left:

                    //�󸶳� ���������� �ʱ�ȭ
                    mapTileControl.countX = 0;
                    break;

                case TilePos.None:
                    break;

            }

            //���� ���������� �ʱ�ȭ
            tilePOS = TilePos.None;
        }

        void CreateFieldTile()
        {
            //�ڷ�ƾ?
            //�� Ÿ���� ��ġ(�����ϴ���) üũ�� �ʵ� ����
            //��ġ������ ����
            GameObject newTile = Instantiate(fieldTilePrefab);
            FieldTile newfieldTile = newTile.GetComponent<FieldTile>();
        }



        /* collider
        */

    }
}
