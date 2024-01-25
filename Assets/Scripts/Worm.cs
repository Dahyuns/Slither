using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Slither
{
    public class Worm : MonoBehaviour
    {
        private static int gold;  //°ñµå
        private static float heart; //¸ñ¼û
        private static float speed; //¼Óµµ
        private static float def;   //¹æ¾î·Â

        //ÀÌ°Å »óÁ¡¿¡ µÎ´Â°Ô ³´³ª?
        private static float goldAddP; //°ñµå  --%¸¸Å­ Ãß°¡ È¹µæ
        private static float heartAddP;//¸ÔÀÌ  --%¸¸Å­ Ãß°¡ È¹µæ
        private static float defAddP;  //¹æ¾î·Â --%¸¸Å­ Ãß°¡ È¹µæ

        //ÀÐ±âÀü¿ë
        public int Gold { get { return gold; } }
        public float Heart { get { return heart; } }
        public float Speed { get { return speed; } }
        public float Def { get { return def; } }

        //½ÃÀÛ½Ã 
        [SerializeField] private int startGold = 0;
        [SerializeField] private int startHeart = 0;
        [SerializeField] private float startSpeed = 1;
        [SerializeField] private float startDef = 0;

        Vector3 moveDir;

        private WormControls inputActions;

        private void Start()
        {
            gold = startGold;
            heart = startHeart;
            speed = startSpeed;
            def = startDef;
            inputActions = new WormControls();
        }

        #region
        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }
        #endregion

        private void Update()
        {
            if (moveDir != Vector3.zero)
                transform.Translate(moveDir.normalized * Time.deltaTime * Speed, Space.World);
        }

        void OnMove(InputValue value)
        {
            Vector2 input = value.Get<Vector2>();
            if (input != null)
            {
                moveDir = new Vector3(input.x, 0, input.y);
            }
        }


        // °ñµå Ãß°¡ - °ñµåÈ¹µæ => ¿ø·¡ µé°íÀÖ´Â °ñµå += ¾òÀº °ñµå + ¾òÀº °ñµå * goldAddP

        // °ñµå °¨¼Ò - »óÁ¡±¸¸Å

        // ¸ñ¼û Ãß°¡ - ¸ÔÀÌÈ¹µæ => ¿ø·¡ °®°íÀÖ´Â ¸ñ¼û += ¾òÀº ¸ÔÀÌ + ¾òÀº ¸ÔÀÌ * goldAddP

        // ¸ñ¼û °¨¼Ò - Àå¾Ö¹° ºÎµúÈû

        // ¼Óµµ Áõ°¡

        // ¹æ¾î·Â Ãß°¡ (ÆÛ¼¾Æ®)
    }
}