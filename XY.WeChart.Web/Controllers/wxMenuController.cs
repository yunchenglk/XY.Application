using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Services;
using XY.Entity;
using XY.Entity.WeChart;
using System.Collections;

namespace XY.WeChart.Web.Controllers
{
    public class wxMenuController : _baseController
    {
        // GET: wxMenu
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult syncMenuOnline(List<List<_poseMenu>> menus)
        {
            WxJsonResult result = new WxJsonResult();
            ButtonGroup bg = new ButtonGroup();
            IList<BaseButton> topList = new List<BaseButton>();
            IList<SingleButton> subList = new List<SingleButton>();

            for (int i = 0; i < menus.Count; i++)
            {
                List<_poseMenu> list = menus[i].OrderBy(m => m.sort).ToList();
                //过滤掉不符合条件的

                for (int x = list.Count - 1; x >= 1; x--)
                {
                    if (string.IsNullOrEmpty(list[x].title))
                        list.RemoveAt(x);
                    if (string.IsNullOrEmpty(list[x].key) && string.IsNullOrEmpty(list[x].url))
                        list.RemoveAt(x);
                }


                if (!string.IsNullOrEmpty(list[0].title))
                {
                    if (list.Count() == 1) //没有子菜单
                    {
                        if (!string.IsNullOrEmpty(list.First().key))//click
                        {
                            SingleClickButton topSingleButton = new SingleClickButton();
                            topSingleButton.name = list.First().title;
                            topSingleButton.key = list.First().key;
                            topList.Add(topSingleButton);
                        }
                        else if (!string.IsNullOrEmpty(list[0].url))//view
                        {
                            SingleViewButton topSingleButton = new SingleViewButton();
                            topSingleButton.name = list.First().title;
                            topSingleButton.url = list.First().url;
                            topList.Add(topSingleButton);
                        }
                    }
                    else//有子菜单
                    {
                        subList = new List<SingleButton>();
                        SubButton topButton = new SubButton(list.First().title);
                        list.Remove(list.First());
                        for (int j = 0; j < list.Count(); j++)
                        {
                            if (!string.IsNullOrEmpty(list[j].url))//view
                            {
                                SingleViewButton sub = new SingleViewButton();
                                sub.name = list[j].title;
                                sub.url = list[j].url;
                                subList.Add(sub);
                            }
                            else if (!string.IsNullOrEmpty(list[j].key))//click
                            {
                                SingleClickButton sub = new SingleClickButton();
                                sub.name = list[j].title;
                                sub.key = list[j].key;
                                subList.Add(sub);
                            }
                        }
                        if (subList.Count > 0)
                        {
                            topButton.sub_button.AddRange(subList);
                            topList.Add(topButton);
                        }
                    }

                }
            }
            bg.button.AddRange(topList);
            if (bg.button.Count() > 0)
                result = WeChartAPI.CreateMenu(GetToken(), bg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public JsonResult getMenuOnline()
        {
            GetMenuResult result = WeChartAPI.GetMenu(GetToken());
            if (result != null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }
    }
    public class _poseMenu
    {
        public int sort { get; set; }
        public string title { get; set; }
        public string key { get; set; }
        public string url { get; set; }


    }
}