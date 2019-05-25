using System;
using System.Collections.Generic;

namespace Library.Core.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public IEnumerable<string> Authors { get; set; }

        public bool IsBorrowed { get; set; }

        public string BorrowedBy { get; set; }

        public string Comment { get; set; }

        public DateTime ReturningDate { get; set; }

    }
}
