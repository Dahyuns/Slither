using UnityEngine;

namespace WiggleQuest
{
    public class Feed : MonoBehaviour
    {
        public GameObject destroyEffPrefab;
        public Material[] mat = new Material[4];

        [SerializeField] private float feedValue = 0.5f;

        private void Awake()
        {
           SetColor();
        }

        private void SetColor()
        {
            int num = Random.Range(0, mat.Length);
            gameObject.GetComponent<MeshRenderer>().material = mat[num];
        }

        private void OnTriggerEnter(Collider collision)
        {

            Worm worm = collision.gameObject.GetComponent<Worm>();

            if (worm != null)
            {
                //格见(部府)眠啊
                worm.AddHeart(feedValue);

                Destroy(this.gameObject);
                Instantiate(destroyEffPrefab, this.transform.position, Quaternion.identity);
            }
        }
    }
}