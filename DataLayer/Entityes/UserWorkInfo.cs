using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entityes
{
    public class UserWorkInfo
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string Building { get; set; }
        public int Salary { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Phone { get; set; }
        public string SecondPhone { get; set; }
        public string Email { get; set; }
        public string SecondEmail { get; set; }
        public string Rank { get; set; }
        public string Floor { get; set; }
        public string Colleagues { get; set; }
        public string KindOf { get; set; }
        public string TypeOfWork { get; set; }
        public string LCA { get; set; }
        public string NDA { get; set; }
        public string Parking { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
