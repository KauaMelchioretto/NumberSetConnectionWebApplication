using Newtonsoft.Json;
using NumberSetConnectionWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NumberSetConnectionWebApplication.Controllers
{
    public class NetworkController : Controller
    {
        // GET: Network
        public ActionResult Index(int? quantityElements)
        {
            if (quantityElements != null)
            {
                Network elements = CreateElements(quantityElements.GetValueOrDefault());
                return RedirectToAction("Index", "ElementView", new { elements = JsonConvert.SerializeObject(elements.Elements.ToList()) });
            }

            return View();
        }

        public Network CreateElements(int quantityElements)
        {
            Network network = new Network(quantityElements);

            return network;
        }
    }
}