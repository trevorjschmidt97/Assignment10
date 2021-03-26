using System;
namespace Assignment10.Models.ViewModels
{
    public class PageNumberingInfo
    {
        public int NumItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalNumItems { get; set; }

        //Cal number of pages
        public int NumPages => (int) Math.Ceiling((float)TotalNumItems / NumItemsPerPage);
    }
}
