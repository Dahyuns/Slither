using UnityEngine;

namespace WiggleQuest
{
    public class Feed : MonoBehaviour
    {
        [SerializeField] private int feedValue = 100;


        //Color[] fourColors = { new Color ( 1, 0,     0,      1), //Red
        //                       new Color ( 0, 1,     0,      1), //green
        //                       new Color ( 1, 0.64f, 0,      1), //orange
        //                       new Color ( 1, 0.92f, 0.016f, 1) }; //yellow

        public Material[] mat = new Material[4];

        private void Awake()
        {
           SetColor();
        }

        /*//테스트용
        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.K))
            {
                SetColor();
            }
        }*/

        private void SetColor()
        {
            int num = Random.Range(0, mat.Length);
            gameObject.GetComponent<MeshRenderer>().material = mat[num];
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);

            Worm worm = collision.gameObject.GetComponent<Worm>();

            if (worm != null)
            {
                //목숨(꼬리)추가
                worm.AddHeart(feedValue);
                Debug.Log(Worm.Level);
            }
            //이펙트추가 - 몸통생성시에도
        }
    }
}