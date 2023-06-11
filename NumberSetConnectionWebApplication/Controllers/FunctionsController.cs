using Newtonsoft.Json;
using NumberSetConnectionWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NumberSetConnectionWebApplication.Controllers
{
    public class FunctionsController : Controller
    {
        // GET: Connection
        public ActionResult Index(String currentElement, String allElements, String functionName, int? _secondNumber)
        {
            List<Element> elements = JsonConvert.DeserializeObject<List<Element>>(allElements);
            Element element = JsonConvert.DeserializeObject<Element>(currentElement);
            Functions data = new Functions();
            data.FirstNumber = element.Value;
            data.Elements = elements;
            data.FunctionName = functionName;
            _ = _secondNumber.HasValue == true ? data.SecondNumber = (int)_secondNumber : 0;

            return View(data);
        }

        public ActionResult Create(int firstNumber, int secondNumber, String _elements)
        {
            List<Element> elements = JsonConvert.DeserializeObject<List<Element>>(_elements);
            Element _currentElement = elements.Find(x => x.Value == firstNumber);

            if (firstNumber > 0 && firstNumber <= elements.Count && secondNumber <= elements.Count && firstNumber != secondNumber)
            {
                if (!_currentElement.DirectlyConnected.Contains(secondNumber))
                {
                    elements = Network.Connect(firstNumber, secondNumber, elements);

                    return RedirectToAction("Index", "ElementView", new { elements = JsonConvert.SerializeObject(elements) });
                }
                else
                {
                    TempData["Message"] = "The connection has been exist!";
                    return RedirectToAction("Index", new { currentElement = JsonConvert.SerializeObject(_currentElement), allElements = _elements, functionName = "Create", _secondNumber = secondNumber });
                }
            }
            else
            {
                TempData["Message"] = "The numbers don't match with the elements interval";
                return RedirectToAction("Index", new { currentElement = JsonConvert.SerializeObject(_currentElement), allElements = _elements, functionName = "Create", _secondNumber = secondNumber });
            }
        }

        public ActionResult Query(int firstNumber, int secondNumber, String _elements)
        {
            List<Element> elements = JsonConvert.DeserializeObject<List<Element>>(_elements);
                Element _currentElement = elements.Find(x => x.Value == firstNumber);

            if (firstNumber > 0 && firstNumber <= elements.Count && secondNumber <= elements.Count && firstNumber != secondNumber)
            {
                bool isConnected = Network.Query(firstNumber, secondNumber, elements);
                if (isConnected)
                {
                    TempData["IsConnected"] = "The numbers are connected!";
                }
                else
                {
                    TempData["IsConnected"] = "The numbers are NOT connected!";
                }

                return RedirectToAction("Index", new { currentElement = JsonConvert.SerializeObject(_currentElement), allElements = _elements, functionName = "Query", _secondNumber = secondNumber });
            }
            else
            {
                TempData["Message"] = "The numbers don't match with the elements interval";
                return RedirectToAction("Index", new { currentElement = JsonConvert.SerializeObject(_currentElement), allElements = _elements, functionName = "Query", _secondNumber = secondNumber });
            }
        }
    }
}