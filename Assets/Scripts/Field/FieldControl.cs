using UnityEngine;

namespace WiggleQuest
{
    public class FieldControl : MonoBehaviour
    {
        //�߰�����
        //�߰�, ���� ���� ���� 

        //feed gold trap(fire,)
        //     �� ��ġ ����/ ���� ���� �����ϰ� / ��ġ��X
        //                                      �� ��ġ�� destroy?


        //Create Field : Create �������� ���� �� DestroyŸ���� ����
        //���� Ÿ���� �̰Ű�, 
        //�����ϸ� createŸ���� ���� or �̵�
        //**�װ��� destroy�� �ִ��� Ȯ��, ������ ����
        //collider�� ������ false��ȯ?


        enum TilePos
        {
            Upper,      //��
            Right,      //����
            Left,       //��
            Lower       //�Ʒ� 
        }

        private float createPosX;
        private float createPosY;


        //Destroy Field : �� DestroyŸ���� �÷��̾�� �����Ÿ� �־����� (�ʵ� �� ��� �����۰� �Բ�)����
        private float destroyPosX;
        private float destroyPosY;

        private float destroyDistance; //�����Ÿ�


    }
}
