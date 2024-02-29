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
            Upper,       //��
            Right,       //����
            Left,        //��
            Lower,       //�Ʒ� 
            None
        }

        // [Field Tile �����κ�]

        //����
        public GameObject fieldTilePrefab;
        public GameObject gridCanvas;
        public Transform Transform;

        private GameObject[] createTiles;
        private MapTile mapTileControl;

        private Vector2 startPosX = new(0, 0);   
        private Vector2 midStartPosX = new(0, 0);      
        private Vector2 midEndPosX = new(0, 0);        
        private Vector2 endPosX = new(0, 0);  
        private Vector2 endPosY = new(0, 0);            
        private Vector2 midEndPosY = new(0, 0);         
        private Vector2 midStartPosY = new(0, 0);       
        private Vector2 startPosY = new(0, 0);          

        float TilePosX;
        float TilePosY;

       // private Tuple<TilePos>;

        private void Start()
        {
            createTiles = transform.GetComponentsInChildren<GameObject>();
            mapTileControl = gridCanvas.GetComponent<MapTile>();
        }

        private void Update()
        {
            TilePosX = fieldTilePrefab.transform.position.x;
            TilePosY = fieldTilePrefab.transform.position.y;


            //���� ��Ÿ���� �����̸� 
            if (mapTileControl.CameraMoveCheck() == true)
            {
                //�׸�ŭ �굵 ������ (ũ�Ⱑ �ٸ�, �굵 üũ�ϰ� ����������)
                CreateTileMove();

                //���� ���������� üũ�� �ʵ� ����
                switch (Check())
                {
                    case TilePos.Upper:
                        CeateTile(TilePos.Upper);
                        break;
                    case TilePos.Right:
                        CeateTile(TilePos.Right);
                        break;
                    case TilePos.Left:
                        CeateTile(TilePos.Left);
                        break;
                    case TilePos.Lower:
                        CeateTile(TilePos.Lower);
                        break;
                    case TilePos.None:
                        break;

                }
            }
        }

        void CreateTileMove()
        {

        }

        void CeateTile(TilePos tilePos)
        {
            //��ġ������ ����
            GameObject newTile = Instantiate(fieldTilePrefab);
            FieldTile newfieldTile = newTile.GetComponent<FieldTile>();
        }

        //��
        TilePos Check() 
        {
            //��
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

            else { return TilePos.None; }
        }

        /* collider
        */

    }
}
