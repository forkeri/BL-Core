using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BL_Core.Language
{
    public class LanguageUtil:MonoBehaviour
    {
        /*  MultilineAttribute:对于制定的变量进行多行显示
         *  SerializeField:
         */

        [SerializeField, MultilineAttribute,Header("语言包")]
        private string text = "";

        public string LanguageText { set; get; }
    }
}
