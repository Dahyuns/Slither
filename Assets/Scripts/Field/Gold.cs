using UnityEngine;

namespace WiggleQuest
{
    public class Gold : MonoBehaviour
    {
        //����
        private GameObject worm;

        [SerializeField] private int goldValue = 100;

        private void Awake()
        {
            worm = GameObject.Find("PlayerLook");
        }

        private void Update()
        {
            Vector3 dir = worm.transform.position - this.transform.position;

            Quaternion rot = Quaternion.LookRotation(dir.normalized);
            this.transform.rotation = rot*Quaternion.Euler(50,0,0) ;
        }


        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);
            Worm worm = collision.gameObject.GetComponent<Worm>();
            if (worm != null)
            {
                //����߰�
                worm.AddGold(goldValue);
            }

            //����Ʈ �߰�
        }
    }
}