using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SUBH1");
            dt.Columns.Add("SUBH2");
            dt.Columns.Add("SUBH3");
            dt.Columns.Add("SUBH4");
            dt.Columns.Add("SUBH5");

            for (int i = 0; i < 5; i++)
            {
               DataRow dr= dt.NewRow();
                dr["SUBH1"] = "D" + i;
                dr["SUBH2"] = "D" + i;
                dr["SUBH3"] = "D" + i;
                dr["SUBH4"] = "D" + i;
                dr["SUBH5"] = "D" + i;
                dt.Rows.Add(dr);
            }


            List<WebGridColumn> columnsMain = new List<WebGridColumn>();
            columnsMain.Add(new WebGridColumn() { ColumnName = "MainH1", Style = " rowspan=3" });
            columnsMain.Add(new WebGridColumn() { ColumnName = "MainH2", Style = " rowspan=2" });
            ViewBag.ColumnsMain = columnsMain;
            List<WebGridColumn> columns = new List<WebGridColumn>();
            foreach (DataColumn col in dt.Columns)
            {
                columns.Add(new WebGridColumn()
                {
                    ColumnName = col.ColumnName,
                    Header = col.ColumnName
                });
            }
            //columns.Add(new WebGridColumn()
            //{
            //    Format = (item) =>
            //    {
            //        return new HtmlString(string.Format("<a href= {0}>Edit</a>", Url.Action("Edit", "Home", new
            //        {
            //            Id = item.SSN
            //        })));
            //    }
            //});
            ViewBag.Columns = columns;
            //Converting datatable to dynamic list     
            var dns = new List<dynamic>();
            dns = ConvertDtToList(dt);
            ViewBag.Total = dns;

            return View();
        }

        public List<dynamic> ConvertDtToList(DataTable dt)
        {
            var data = new List<dynamic>();
            foreach (var item in dt.AsEnumerable())
            {
                // Expando objects are IDictionary<string, object>  
                IDictionary<string, object> dn = new ExpandoObject();

                foreach (var column in dt.Columns.Cast<DataColumn>())
                {
                    dn[column.ColumnName] = item[column];
                }

                data.Add(dn);
            }
            return data;
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}