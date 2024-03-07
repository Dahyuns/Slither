using System.Collections.Generic;
using UnityEngine;

namespace WiggleQuest
{
    enum TilePos //Ÿ�� move�� ����
    {
        Up, Down, Left, Right, None
    }

    public class FieldControl : MonoBehaviour
    {
        // [Create Tile �̵� �� ��ġ�� ���� Field Tile ����]

        //����
        public MapTile mapTileControl; //�̵��Ÿ� ����� ���� ����
        public GameObject fieldTilePrefab;
        public Transform fieldTileParent; //�ʵ�Ÿ�Ϲ���

        //������ġ�� �߾���ǥ
        private Vector3 CreateTileCenter = new Vector3(60f, -10f, 60f);

        //�߾���ǥ�κ����� �� ��ǥ - �ʵ�Ÿ�� ������ġ, �߾���ǥ 'CreateTileCenter'���κ����� �Ÿ�(����)
        private Vector3[] creatTilesPos = new Vector3[]
        { new Vector3(-40f, 0f, 40f), new Vector3(0f, 0f, 40f), new Vector3(40f, 0f, 40f),
          new Vector3(-40f, 0f, 0),                             new Vector3(40f, 0f, 0f) ,
          new Vector3(-40f, 0f, -40f),new Vector3(0f, 0f, -40f),new Vector3(40f, 0f, -40f) };

        //FieldTile��ġ ���� - FieldTile������ ���� ����!
        public List<Vector3> fieldTiles = new List<Vector3>();

        //�ʻ�������� ����
        [SerializeField] private float mapSizeMag = 4;

        private float offset; //CreateTile��ġ�������

        private TilePos tilePOSDir; //CreateTile �̵� ����, None�� ��

        public bool isCreateTileMove = false;

        private void Start()
        {
            offset = 10 * mapSizeMag;
            tilePOSDir = TilePos.None;

            //ó�� ��ġ�� ���� : �߾� 1�� + ���� 8��
            CreateFieldTile(CreateTileCenter);
            CheckNCreateField();
        }

        private void Update()
        {
            //none���� �ٽ� �ʱ�ȭ ������ ����X(�ڷ�ƾ����Ѵٸ�?) 
            if (tilePOSDir == TilePos.None) // mapTileControl.isMapMoving == true && 
            {
                //���� ���� ��ũ���� 4�踸ŭ �������ٸ�?
                if (CheckMoveCount())
                {
                    //CreateTile�� ������ + �󸶳� ���������� �ʱ�ȭ
                    MoveCreateTile();

                    //������ ��ġ �������� �װ��� FieldTile�� �ִ��� Ȯ�� ��
                    //'���ٸ�' ����
                    CheckNCreateField();
                    isCreateTileMove = true;
                }
                else
                    isCreateTileMove = false;

            }
        }


        //�󸶳� ���������� üũ, ����������� ����/ �� moveī��Ʈ �ʱ�ȭ (ũ�Ⱑ �ٸ�)
        bool CheckMoveCount()
        {
            if (mapTileControl.countX >= mapSizeMag)
            {
                tilePOSDir = TilePos.Right;
                mapTileControl.countX -= (int)mapSizeMag;
                return true;
            }
            else if (mapTileControl.countX <= -mapSizeMag)
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

        void MoveCreateTile()
        {
            //�������ٸ� �׸�ŭ �굵 ������ + �󸶳� ���������� �ʱ�ȭ
            switch (tilePOSDir)
            {
                case TilePos.Up:
                    CreateTileCenter.z += offset;
                    break;
                case TilePos.Down:
                    CreateTileCenter.z -= offset;
                    break;


                case TilePos.Right:
                    CreateTileCenter.x += offset;
                    break;
                case TilePos.Left:
                    CreateTileCenter.x -= offset;
                    break;
            }

            //���� ���������� �ʱ�ȭ - none���� �缳�� , ���ܰ��ɼ�
            tilePOSDir = TilePos.None;
        }

        void CheckNCreateField()
        {
            for (int i = 0; i < creatTilesPos.Length; i++) //createTile ��ġ ����
            {
                //Ÿ���� ���� ��ġ = ���峻 �߽� + �� ���� ��ġ
                Vector3 eachTilePos = CreateTileCenter + creatTilesPos[i];

                if (CheckFieldTile(eachTilePos)) //�ش� ��ġ�� Ÿ���� �ִ��� �˻�
                {
                    CreateFieldTile(eachTilePos); //������ ����
                }
            }
        }
        
        //true��ȯ�� field Tile ����
        bool CheckFieldTile(Vector3 newTilePos) 
        {    
            //����� ��ǥ(������� Ÿ���� ��ǥ)�� �����ϴٸ� ������X
            for (int i = 0; i < fieldTiles.Count; i++)
            {
                if (fieldTiles[i] == newTilePos)
                {
                    return false;
                }
            }
            
            //����� ��ǥ�� ���ο� ��ǥ�� ������ �����.
            return true;
        }

        void CreateFieldTile(Vector3 newTilePos)
        {
            GameObject newTile = Instantiate(fieldTilePrefab, newTilePos, Quaternion.identity,fieldTileParent);
            FieldTile newfieldTile = newTile.GetComponent<FieldTile>();

            if (newfieldTile != null) //����� �����ƴٸ�
            {
                //����Ʈ�� �߰�
                fieldTiles.Add(newTilePos);
                //�ʵ峻 �����۵� ����
                //newfieldTile.CreateItems();
            }
        }
    }
}
