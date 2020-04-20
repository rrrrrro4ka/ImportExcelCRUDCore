using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entityes
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Middlename { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string SecondEmail { get; set; }
        public string Eyes { get; set; }
        public string Hair { get; set; }
        public string Nose { get; set; }
        public string Head { get; set; }
        public string Tattoo { get; set; }
        public string BestSkills { get; set; }
        public string Car { get; set; }
        public string LoveAnimal { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
