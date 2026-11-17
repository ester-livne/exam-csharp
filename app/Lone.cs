using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    internal class Lone
    {
        public bool Returned { get; set; }   
        public int ReaderId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string Book { get; set; }
        public int Loan_day { get; set; }
        public int Return_day { get; set; }
        public Lone(int ReaderId,string name, string type, string book, int loan_day, int return_day)
        {
            this.ReaderId = ReaderId;
            this.name = name;
            this.type = type;
            this.Book = book;
            this.Loan_day=loan_day;
            this.Return_day=return_day;
        }
    }
}
