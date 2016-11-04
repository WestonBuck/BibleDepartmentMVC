using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Badge_System.Models
{
    public class SearchViewModel
    {
        public List<User> SearchResults { get; set; }
        public string SearchTerm { get; set; }
    }
}