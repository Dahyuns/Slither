using UnityEngine;

namespace WiggleQuest
{
    public class Gold : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);
            //����߰�
            //����Ʈ �߰�
        }
    }
}