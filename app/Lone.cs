using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    internal class Lone
    {
        public Reader Reader { get; set; }
        public string Book { get; set; }
        public int Loan_day { get; set; }
        public int Return_day { get; set; }
        public Lone(Reader reader, string book, int loan_day, int return_day)
        {
            this.Reader = reader;
            this.Book = book;
            this.Loan_day=loan_day;
            this.Return_day=return_day;
        }
    }
}
