using UnityEngine;

namespace WiggleQuest
{
    public class Feed : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);
            //�����߰�
            //����Ʈ�߰� - ��������ÿ���
        }
    }
}