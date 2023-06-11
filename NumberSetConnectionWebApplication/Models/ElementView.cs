using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberSetConnectionWebApplication.Models
{
    public class ElementView
    {
        public List<Element> Elements { get; set; }

        public ElementView (List<Element> elements)
        {
            this.Elements = elements;
        }
    }
}