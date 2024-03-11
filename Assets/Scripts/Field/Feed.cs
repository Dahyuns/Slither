using UnityEngine;

namespace WiggleQuest
{
    public class Feed : MonoBehaviour
    {
        public GameObject destroyEffPrefab;
        public Material[] mat = new Material[4];

        [SerializeField] private int feedValue = 1;

        private void Awake()
        {
           SetColor();
        }

        private void SetColor()
        {
            int num = Random.Range(0, mat.Length);
            gameObject.GetComponent<MeshRenderer>().material = mat[num];
        }

        private void OnCollisionEnter(Collision collision)
        {

            Worm worm = collision.gameObject.GetComponent<Worm>();

            if (worm != null)
            {
                //格见(部府)眠啊
                worm.AddHeart(feedValue);

                GameObject effect = Instantiate(destroyEffPrefab, this.transform.position, Quaternion.identity);

                Destroy(this.gameObject);

                Destroy(effect,2f);
            }
        }
    }
}