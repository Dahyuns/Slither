using UnityEngine;
using TMPro;

namespace WiggleQuest
{
    public class PriceTextControl : MonoBehaviour //, IPointerEnterHandler, IPointerExitHandler
    {
        //�߰�����
        //�ؽ�Ʈ����

        //����
        private RectTransform thisRectTransform;
        private GameObject panel;
        private TextMeshProUGUI priceText;

        [SerializeField]
        private Vector2 offset;

        public static Vector2 mousePoint;

        void Start()
        {
            thisRectTransform = GetComponent<RectTransform>();
            panel = this.transform.Find("PriceBg").gameObject; //GetComponentInChildren<GameObject>();

            priceText = panel.GetComponentInChildren<TextMeshProUGUI>();
            //�Ⱥ��̰� ����
            panel.SetActive(false);
        }

        void Update()
        {
            //priceText�޾ƿͼ� ��� //�� �迭 �����;���
            if (CheckMouse() == true)
            {
                if (panel.activeInHierarchy == false)
                {
                    panel.SetActive(true);
                }
                SetPosPriceGroup();
            }
            else if (CheckMouse() == false)
            {
                if (panel.activeInHierarchy == true)
                {
                    panel.SetActive(false);
                }
            }
        }

        bool CheckMouse()
        {
            bool isInButton = RectTransformUtility.RectangleContainsScreenPoint(thisRectTransform, mousePoint);
            return isInButton;
        }

        void SetPosPriceGroup()
        {
            panel.transform.position = mousePoint + offset;
        }


        ////mouse in
        //���콺�� ��ư �ȿ� �ִٸ�
        //void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        //{
        //    Debug.Log($"IPointerEnter  {eventData.ToString()}");
        //}
        //
        ////mouse out
        //void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        //{
        //    isInButton = false;
        //    Debug.Log($"IPointerExit  {eventData.ToString()}");
        //}

    }
}