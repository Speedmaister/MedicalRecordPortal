using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalRecord.Web.Models
{
    public class SearchModel
    {
        public string SearchText { get; set; }
    }

    public class AutoCompleteOption
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }


}