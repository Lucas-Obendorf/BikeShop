using System.Web.Mvc;
using BikeShop.Infrastructure;
using BikeShop.Logic;
using BikeShop.Logic.Interfaces;

namespace BikeShop.Controllers
{
    public class InventoryController : Controller
    {
        private IBikeRequests _bikeRequests;
        public IBikeRequests BikeRequests
        {
            get { return _bikeRequests ?? (_bikeRequests = LogicFactory.GetBikeRequests());}
            set { _bikeRequests = value; }
        }

        public ActionResult Index()
        {
            return View("ShowInventory");
        }

        public ActionResult BikeForm()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetInventory()
        {
            return Json(BikeRequests.GetInfoForAllBikes());
        }

        [HttpPost]
        public JsonResult GetBikeInfo(int id)
        {
            return Json(BikeRequests.GetBikeInfo(id));
        }

        [HttpPost]
        public JsonResult SaveBike(BikeInfo bike)
        {
            return Json(BikeRequests.SaveBike(bike));
        }

        [HttpPost]
        public JsonResult DeleteBike(int id)
        {
            return Json(BikeRequests.DeleteBike(id));
        }
    }
}
