using MedicalRecord.Web.Controllers;
using MedicalRecord.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MedicalRecord.Web.Helpers
{
    public class QuadrantDrawer
    {
        private const string EmptyCell = "<td></td>";
        private const string CustomCellWithNumberFormat = "<td class=\" disabled\" >{0}{1}</td>";
        private const string CustomCellWithStatusFormat = "<td class=\" tooth-state\" >{0}</td>";

        private const string disabledAttribute = "disabled";
        private const string enabledAttribute = "enabled";
        private const string classAttribute = "class";

        private static Quadrants[] LeftQuadrants = { Quadrants.Fifth, Quadrants.First, Quadrants.Fourth, Quadrants.Eighth };
        private static Quadrants[] RightQuadrants = { Quadrants.Sixth, Quadrants.Second, Quadrants.Third, Quadrants.Seventh };

        private static Quadrants[] TopQuadrants = { Quadrants.Fifth, Quadrants.First, Quadrants.Sixth, Quadrants.Second };
        private static Quadrants[] BottomQuadrants = { Quadrants.Fourth, Quadrants.Third, Quadrants.Eighth, Quadrants.Seventh };

        private static Quadrants[] ShortQuadrants = { Quadrants.Fifth, Quadrants.Sixth, Quadrants.Eighth, Quadrants.Seventh };
        private static Quadrants[] LongQuadrants = { Quadrants.First, Quadrants.Second, Quadrants.Third, Quadrants.Fourth };

        private StringBuilder sb;

        public QuadrantDrawer()
        {
            sb = new StringBuilder();
        }

        public string DrawNumbersRow(ToothModel[] teeth, Quadrants quadrant, IDictionary<string, object> htmlAttributes = null)
        {
            if (!ValidateNumbersRowQuadrants(quadrant, teeth, out string errorMessage))
            {
                throw new Exception(errorMessage);
            }

            bool isLeftQuadrant = LeftQuadrants.Contains(quadrant);
            if (isLeftQuadrant)
            {
                WriteLeftQuadrantCells(teeth, quadrant, htmlAttributes);
            }
            else
            {
                WriteRightQuadrantCells(teeth, quadrant, htmlAttributes);
            }

            string drawnQuadrant = FlushQuadrantString();
            return drawnQuadrant;
        }

        private bool ValidateNumbersRowQuadrants(Quadrants quadrant, ToothModel[] teeth, out string errorMessage)
        {
            errorMessage = null;
            if (teeth.Any(x => x.Quadrant != quadrant))
            {
                errorMessage = $"Not tooth models are from the same given {quadrant} quadrant.";
            }

            return (errorMessage == null);
        }

        public string DrawStatusesRow(Dictionary<Quadrants, ToothModel[]> teethStatus, Quadrants longQuadrant, Quadrants shortQuadrant, IDictionary<string, object> htmlAttributes = null)
        {
            if (!ValidateStatusesRowsQuadrants(shortQuadrant, longQuadrant, out string errorMessage))
            {
                throw new Exception(errorMessage);
            }

            // Depending upon the position of the quadrants from the ordinate axis
            // the for-loop's index will be incremented or decremented thus covering both left and right quadrants cases.
            int rowStart = 8;
            int rowEnd = 0;
            int incrementValue = -1;
            if (IsRightQuadrant(shortQuadrant))
            {
                rowStart = 1;
                rowEnd = 9;
                incrementValue = 1;
            }
            
            for (; rowStart != rowEnd; rowStart += incrementValue)
            {
                string statusCode = GetStatusCode(teethStatus, longQuadrant, shortQuadrant, rowStart);
                var td = string.Format(CustomCellWithStatusFormat, statusCode);
                if (htmlAttributes != null && htmlAttributes.ContainsKey(classAttribute))
                {
                    td = InsertClass(td, htmlAttributes[classAttribute]);
                }

                sb.Append(td);
            }

            string drawnQuadrant = FlushQuadrantString();
            return drawnQuadrant;
        }

        private bool ValidateStatusesRowsQuadrants(Quadrants shortQuadrant, Quadrants longQuadrant, out string errorMessage)
        {
            errorMessage = null;
            if (!IsShortQuadrant(shortQuadrant))
                errorMessage = $"{shortQuadrant} is not a short quadrant";

            if (!IsLongQuadrant(longQuadrant))
                errorMessage = $"{longQuadrant} is not a long quadrant";

            bool areInTheSameHorizontalQuadrant = (IsTopQuadrant(shortQuadrant) && IsTopQuadrant(longQuadrant)) || (IsBottomQuadrant(shortQuadrant) && IsBottomQuadrant(longQuadrant));
            if (!areInTheSameHorizontalQuadrant)
                errorMessage = $"Quadrants({shortQuadrant},{longQuadrant}) are not in the same plane according to the abscissa axis.";

            bool areInTheSameVerticalQuadrant = (IsLeftQuadrant(shortQuadrant) && IsLeftQuadrant(longQuadrant)) || (IsRightQuadrant(shortQuadrant) && IsRightQuadrant(longQuadrant));
            if (!areInTheSameVerticalQuadrant)
                errorMessage = $"Quadrants({shortQuadrant},{longQuadrant}) are not in the same plane according to the ordinate axis.";

            return (errorMessage == null);
        }

        private string FlushQuadrantString()
        {
            string quadrantString = sb.ToString();
            sb.Clear();

            return quadrantString;
        }

        private static string GetStatusCode(Dictionary<Quadrants, ToothModel[]> teethStatus, Quadrants longQuadrant, Quadrants shortQuadrant, int rowStart)
        {
            var longQuadrantTooth = teethStatus[longQuadrant].FirstOrDefault(x => x.OrderNumber == rowStart);
            var shortQuadrantTooth = teethStatus[shortQuadrant].FirstOrDefault(x => x.OrderNumber == rowStart);
            string statusCode = null;
            if (shortQuadrantTooth == null || longQuadrantTooth.IsActive)
            {
                statusCode = longQuadrantTooth.StateCode;
            }
            else
            {
                statusCode = shortQuadrantTooth.StateCode;
            }

            return statusCode;
        }

        private void WriteLeftQuadrantCells(ToothModel[] teeth, Quadrants quadrant, IDictionary<string, object> htmlAttributes = null)
        {
            int quadrantLength = GetQuadrantLength(quadrant);
            if (IsShortQuadrant(quadrant))
            {
                AppendEmptyCells(sb);
            }

            for (int i = quadrantLength; i > 0; i--)
            {
                var td = CreateTableCell(teeth, quadrant, htmlAttributes, i);
                sb.Append(td);
            }
        }

        private void WriteRightQuadrantCells(ToothModel[] teeth, Quadrants quadrant, dynamic htmlAttributes = null)
        {
            int quadrantLength = GetQuadrantLength(quadrant);
            for (int i = 1; i <= quadrantLength; i++)
            {
                var td = CreateTableCell(teeth, quadrant, htmlAttributes, i);
                sb.Append(td);
            }

            if (IsShortQuadrant(quadrant))
            {
                AppendEmptyCells(sb);
            }
        }

        private void AppendEmptyCells(StringBuilder sb)
        {
            sb.Append(EmptyCell);
            sb.Append(EmptyCell);
            sb.Append(EmptyCell);
        }

        private string CreateTableCell(ToothModel[] teeth, Quadrants quadrant, dynamic htmlAttributes, int i)
        {
            var td = string.Format(CustomCellWithNumberFormat, (int)quadrant, i);
            if (htmlAttributes != null && htmlAttributes.ContainsKey(classAttribute))
            {
                td = InsertClass(td, htmlAttributes[classAttribute]);
            }

            if (teeth[i - 1].IsActive)
            {
                td = td.Replace(disabledAttribute, enabledAttribute);
            }

            return td;
        }

        private int GetQuadrantLength(Quadrants quadrant)
        {
            int quadrantLength = PatientModel.PermanentTeethCountInQuadrant;
            if (IsShortQuadrant(quadrant))
            {
                quadrantLength = PatientModel.PrimaryTeethCountInQuadrant;
            }

            return quadrantLength;
        }

        private string InsertClass(string htmlElement, object classValue)
        {
            string classAttributeDeclaration = "class=\"";
            int indexOfClassAttribute = htmlElement.IndexOf(classAttributeDeclaration);
            int indexOfClassValue = indexOfClassAttribute + classAttributeDeclaration.Length;
            return htmlElement.Insert(indexOfClassValue, classValue.ToString());
        }

        private bool IsShortQuadrant(Quadrants quadrant) => ShortQuadrants.Contains(quadrant);
        private bool IsRightQuadrant(Quadrants quadrant) => RightQuadrants.Contains(quadrant);
        private bool IsLongQuadrant(Quadrants quadrant) => LongQuadrants.Contains(quadrant);
        private bool IsLeftQuadrant(Quadrants quadrant) => LeftQuadrants.Contains(quadrant);
        private bool IsBottomQuadrant(Quadrants quadrant) => BottomQuadrants.Contains(quadrant);
        private bool IsTopQuadrant(Quadrants quadrant) => TopQuadrants.Contains(quadrant);
    }
}