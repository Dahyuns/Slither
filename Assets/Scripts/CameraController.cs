using UnityEngine;

namespace WiggleQuest
{
    public class CameraController : MonoBehaviour
    {
        //������ : ���� ��ü 
        public GameObject worm;

        private void Update()
        {
            //worm ���� ������
            this.transform.position = new Vector3(worm.transform.position.x, transform.position.y, worm.transform.position.z);      
        }
    }
}