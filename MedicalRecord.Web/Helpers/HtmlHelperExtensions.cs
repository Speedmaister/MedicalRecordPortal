using MedicalRecord.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MedicalRecord.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        private static QuadrantDrawer drawer = new QuadrantDrawer();

        public static HtmlString DrawQuadrantRowWithNumbers(this HtmlHelper helper, ToothModel[] teeth, Quadrants quadrant, IDictionary<string, object> htmlAttributes = null)
        {
            return new HtmlString(drawer.DrawNumbersRow(teeth, quadrant, htmlAttributes));
        }

        public static HtmlString DrawQuadrantStatusesRow(this HtmlHelper helper, Dictionary<Quadrants, ToothModel[]> teethStatus, Quadrants longQuadrant, Quadrants shortQuadrant, IDictionary<string, object> htmlAttributes = null)
        {
            return new HtmlString(drawer.DrawStatusesRow(teethStatus, longQuadrant, shortQuadrant, htmlAttributes));
        }
    }
}