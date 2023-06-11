using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NumberSetConnectionWebApplication.Models
{
    public class Network
    {

        [Display(Name = "Quantity Elements"), Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int QuantityElements { get; set; }
        public List<Element> Elements { get; set; }

        public Network(int quantityElements)
        {
            this.QuantityElements = quantityElements;
            this.Elements = PopulateElements(quantityElements);
        }

        public List<Element> PopulateElements(int quantityElements)
        {
            List<Element> elements = new List<Element>();

            for (int i = 1; i <= quantityElements; i++)
            {
                Element element = new Element(i);
                elements.Add(element);
            }

            return elements;
        }

        public static List<Element> Connect(int firstNumber, int numberToConnect, List<Element> elements)
        {
            Element primaryElement = elements.Find(x => x.Value == firstNumber);
            Element secondaryElement = elements.Find(x => x.Value == numberToConnect);
            List<Element> serializedElements = elements.FindAll(x => primaryElement.DirectlyConnected.Contains(x.Value) && !secondaryElement.DirectlyConnected.Contains(secondaryElement.Value));
            serializedElements.AddRange(elements.FindAll(x => secondaryElement.ConnectedBySerial.Contains(x.Value) && !primaryElement.DirectlyConnected.Contains(secondaryElement.Value)));
            serializedElements.AddRange(elements.FindAll(x => primaryElement.ConnectedBySerial.Contains(x.Value) && !secondaryElement.DirectlyConnected.Contains(secondaryElement.Value)));
            serializedElements.AddRange(elements.FindAll(x => secondaryElement.DirectlyConnected.Contains(x.Value) && !primaryElement.DirectlyConnected.Contains(secondaryElement.Value)));
            serializedElements = serializedElements.Distinct().ToList();
            

            primaryElement.DirectlyConnected.Add(numberToConnect);
            secondaryElement.DirectlyConnected.Add(primaryElement.Value);

            primaryElement.ConnectedBySerial.AddRange(secondaryElement.DirectlyConnected.Where(x => !x.Equals(primaryElement.Value) && !secondaryElement.DirectlyConnected.Contains(secondaryElement.Value)));
            primaryElement.ConnectedBySerial.AddRange(secondaryElement.ConnectedBySerial.Where(x => !x.Equals(primaryElement.Value) && !secondaryElement.DirectlyConnected.Contains(secondaryElement.Value)));
            secondaryElement.ConnectedBySerial.AddRange(primaryElement.DirectlyConnected.Where(x => !x.Equals(secondaryElement.Value) && !primaryElement.DirectlyConnected.Contains(primaryElement.Value)));
            secondaryElement.ConnectedBySerial.AddRange(primaryElement.ConnectedBySerial.Where(x => !x.Equals(secondaryElement.Value) && !primaryElement.DirectlyConnected.Contains(primaryElement.Value)));

            primaryElement.ConnectedBySerial = primaryElement.ConnectedBySerial.Distinct().ToList();
            secondaryElement.ConnectedBySerial = secondaryElement.ConnectedBySerial.Distinct().ToList();

            foreach (Element element in serializedElements)
            {
                element.ConnectedBySerial.AddRange(primaryElement.ConnectedBySerial.Where(x => !x.Equals(element.Value) && !secondaryElement.DirectlyConnected.Contains(element.Value)));
                element.ConnectedBySerial.AddRange(primaryElement.DirectlyConnected.Where(x => !x.Equals(element.Value) && !secondaryElement.DirectlyConnected.Contains(element.Value)));
                element.ConnectedBySerial.AddRange(secondaryElement.ConnectedBySerial.Where(x => !x.Equals(element.Value) && !primaryElement.DirectlyConnected.Contains(element.Value)));
                element.ConnectedBySerial.AddRange(secondaryElement.DirectlyConnected.Where(x => !x.Equals(element.Value) && !primaryElement.DirectlyConnected.Contains(element.Value)));

                element.ConnectedBySerial = element.ConnectedBySerial.Distinct().ToList();
            }

            serializedElements = serializedElements.Distinct().ToList();

            int indexPrimaryElement = elements.FindIndex(x => x.Value == primaryElement.Value);
            int indexSecondaryElement = elements.FindIndex(x => x.Value == numberToConnect);
            elements[indexPrimaryElement] = primaryElement;
            elements[indexSecondaryElement] = secondaryElement;

            return elements;
        }

        public static Boolean Query(int firstNumber, int secondNumber, List<Element> elements)
        {
            Element primaryElement = elements.Find(x => x.Value == firstNumber);
            Element secondaryElement = elements.Find(x => x.Value == secondNumber);
            bool isConnected = secondaryElement.DirectlyConnected.Contains(primaryElement.Value) || secondaryElement.ConnectedBySerial.Contains(primaryElement.Value);

            return isConnected;
        }
    }
}