using System.Collections.Generic;

namespace MVC_Badge_System.Models
{
    public class SearchViewModel
    {
        public List<User> SearchResults { get; set; }
        public string SearchTerm { get; set; }
    }
}