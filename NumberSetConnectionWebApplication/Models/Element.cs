using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberSetConnectionWebApplication.Models
{
    public class Element
    {
        public int Value { get; set; }
        public List<int> DirectlyConnected { get; set; }
        public List<int> ConnectedBySerial { get; set; }

        [JsonConstructor]
        public Element (int value)
        {
            this.Value = value;
            this.DirectlyConnected = new List<int> { };
            this.ConnectedBySerial = new List<int> { };
        }
    }
}