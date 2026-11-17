using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    internal class Reader
    {
        public int Reader_id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public Reader(int reader_id, string name, Status status)
        {
            this.Reader_id = reader_id;
            this.Name = name;
            this.Status = status;
        }
    }
}
