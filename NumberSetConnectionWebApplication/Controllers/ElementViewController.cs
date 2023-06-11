using Newtonsoft.Json;
using NumberSetConnectionWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NumberSetConnectionWebApplication.Controllers
{
    public class ElementViewController : Controller
    {
        // GET: ElementView
        public ActionResult Index(String elements)
        {
            List<Element> model = JsonConvert.DeserializeObject<List<Element>>(elements);
            return View(model);
        }

        public ActionResult RedirectToFunctionView(String _currentElement, String _allElements, string _functionName)
        {
            return RedirectToAction("Index", "Functions", new { currentElement = _currentElement, allElements = _allElements, functionName = _functionName });
        }
    }
}