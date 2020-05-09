using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS
{
    public class Copy
    {
        public string Id { get; set; }
        public StatusBook Status { get; set; }
        public DateTime? DueDateReserve { get; set; }
        public Copy(string id)
        {
            this.Id = id;
        }
    }
}
