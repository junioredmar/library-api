using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Library.API.Model
{
    public class BookModel : BaseModel
    {
        [DataMember(IsRequired = true)]
        public string Isbn { get; set; }

        [DataMember(IsRequired = true)]
        public string Title { get; set; }

        [DataMember(IsRequired = true)]
        public string Genre { get; set; }

        [DataMember(IsRequired = true)]
        public IEnumerable<string> Authors { get; set; }

        public bool IsBorrowed { get; set; }

        public string BorrowedBy { get; set; }

        public string Comment { get; set; }

        public DateTime ReturningDate { get; set; }
    }
}
