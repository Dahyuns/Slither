using UnityEngine;

namespace WiggleQuest
{
    public class FieldControl : MonoBehaviour
    {
        //�߰�����
        //���� ���� Ÿ�� 'Create Field' �������� Field Tile����
        //��Ÿ���� ��Ÿ�� �����ŭ �����̸�, Create Field�� Move
        //                 �� ������ ���� Field Tile ��ǥ�� �������� Ȯ�� (���� ��ġ ����Ʈ�� ����? - ������ Ž�� �˰��� ã�ƺ���), ������ ����

        enum TilePos
        {
            Upper,      //��
            Right,      //����
            Left,       //��
            Lower,       //�Ʒ� 
            None
        }

        // [Field Tile �����κ�]

        //����
        public FieldTile fieldTile;

        private float startPosX;          //  ��
        private float midStartPosX;       //    ��
        private float midEndPosX;         //      ��
        private float endPosX;            //        ��

        private float endPosY;            //  �� �� ��
        private float midEndPosY;         //     �� 
        private float midStartPosY;       //     �� 
        private float startPosY;          //  �� �� ��

        float TilePosX;
        float TilePosY;

        private void Start()
        {

        }

        private void Update()
        {
            TilePosX = fieldTile.transform.position.x;
            TilePosY = fieldTile.transform.position.y;
            //���� ��Ÿ���� �����̸� 
            //if (MapTile.isMoving == true)
            {
                //�׸�ŭ �굵 ������ (ũ�Ⱑ �ٸ�, �굵 üũ�ϰ� ����������)
                CreateTileMove();

                //üũ�� �ʵ� ����

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
            FieldTile newfieldTile = Instantiate(fieldTile);
        }

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

        /* 
        collider ������ Create false��ȯ?      */

    }
}
