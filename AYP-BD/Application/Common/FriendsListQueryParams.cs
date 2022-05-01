using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class FriendsListQueryParams
    {
        public string SearchPhrase { get; set; }
        public int PageSize { get; set; } = 50;
        public int PageNumber { get; set; } = 1;
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
    public enum SortDirection
    {
        ASC,
        DESC
    }
}
