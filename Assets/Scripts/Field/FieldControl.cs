using UnityEngine;

namespace WiggleQuest
{
    public class FieldControl : MonoBehaviour
    {
        //�߰�����
        //���� ���� Ÿ�� 'Create Field' �������� Field Tile����
        //��Ÿ���� ��Ÿ�� ������(����4��)��ŭ �����̸�, Create Field�� Move
        //        �� ������ ���� Field Tile ��ǥ�� �������� Ȯ�� (���� ��ġ ����Ʈ�� ����? - ������ Ž�� �˰��� ã�ƺ���), ������ ����
        /*private float startPosX = 20f;  private float midPosX = 60f;   private float endPosX = 100f;
        //private float TilePosX; //TilePosX = fieldTilePrefab.transform.position.x;
        //private float TilePosY; //TilePosY = fieldTilePrefab.transform.position.y;*/

        // [Field Tile �����κ�]
        enum TilePos
        {
            Up, Down, Left, Right, None
        }

        //����
        public GameObject fieldTilePrefab;
        public MapTile mapTileControl;
        public GameObject createTile;//�׽�Ʈ��

        //createTile�� ��ǥ
        private Vector3[,] creatTilesPos = new Vector3[3,3];
        
        //�ʻ�������� ����
        [SerializeField] private float mapSizeMag = 4;

        //createTile�� ��ġ�������
        private float offset;

        TilePos tilePOSDir = TilePos.None;

        //�ʱ�ȭ
        private void Start()
        {
            offset = 10 * mapSizeMag;
            Debug.Log(offset);
            //��ǥ����(����, ���� �Ʒ��� ���� �� �߽���ǥ) - �ʵ�Ÿ�� ������ġ
            //{(20f,100f) , (60f,100f), (100f,100f)}
            //{(20f,60f)  , (60f,60f),  (100f,60f)}
            //{(20f,20f)  , (60f,20f),  (100f,20f)}
            for (int y = 0; y < creatTilesPos.GetLength(0); y++)
            {
                for (int x = 0; x < creatTilesPos.GetLength(1); x++)
                {
                    float posNumX = 20 + (40 * x);
                    float posNumY = 20 + (40 * y);
                    creatTilesPos[x, y] = new Vector3(posNumX,-10f,posNumY);
                }
            }
        }

        private void Update()
        {
            //���� ��Ÿ���� �����̸� 
            if (mapTileControl.isMapMoving == true)
            {
                //�󸶳� ��������? : 4��ũ�⸸ŭ ��������?
                if (MapMoveCountCheck() && tilePOSDir == TilePos.None)
                {
                    //�������ٸ� �굵 ������ + �󸶳� ���������� �ʱ�ȭ
                    CreateTileMove();

                    //Ÿ���� �ִ��� �˻�

                    //������ ����

                    //������ �н�
                }
            }
        }


        //�󸶳� ���������� üũ, ����������� ����/ �� moveī��Ʈ �ʱ�ȭ (ũ�Ⱑ �ٸ�)
        bool MapMoveCountCheck()
        {
            if (mapTileControl.countX >= mapSizeMag)
            {
                tilePOSDir = TilePos.Right; 
                mapTileControl.countX -= (int)mapSizeMag;
                return true;
            }
            else if(mapTileControl.countX <= -mapSizeMag)
            {
                tilePOSDir = TilePos.Left;
                mapTileControl.countX += (int)mapSizeMag;
                return true;
            }
            else if (mapTileControl.countY >= mapSizeMag)
            {
                tilePOSDir = TilePos.Up;
                mapTileControl.countY -= (int)mapSizeMag;
                return true;
            }
            else if (mapTileControl.countY <= -mapSizeMag)
            {
                tilePOSDir = TilePos.Down;
                mapTileControl.countY += (int)mapSizeMag;
                return true;
            }
            else
            {
                return false;
            }
        }

        void CreateTileMove()
        {
            Vector3 thisPosition = createTile.transform.position;
            //�������ٸ� �׸�ŭ �굵 ������ + �󸶳� ���������� �ʱ�ȭ
            switch (tilePOSDir)
            {
                case TilePos.Up:
                    thisPosition.z += offset;
                    break;
                case TilePos.Down:
                    thisPosition.z -= offset;
                    break;


                case TilePos.Right:
                    thisPosition.x += offset;
                    break;
                case TilePos.Left:
                    thisPosition.x -= offset;
                    break;

                case TilePos.None:
                    //MapMoveCountCheck()�� true�� none��ȯ����.
                    Debug.Log("Error : None��ȯ��!!");
                    break;
            }
            createTile.transform.position = thisPosition;

            //���� ���������� �ʱ�ȭ - none���� �缳�� , ���ܰ��ɼ�
            tilePOSDir = TilePos.None;
        }

        void CreateFieldTile()
        {
            for (int y = 0; y < creatTilesPos.GetLength(0); y++)
            {
                for (int x = 0; x < creatTilesPos.GetLength(1); x++)
                {
                    //if (createTiles[x, y].transform == null)
                    {
                        //������ ���� �ʵ�Ÿ�� ���� �κи� ����
                        //CreateFieldTile();
                    }
                }
            }

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
