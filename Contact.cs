﻿namespace TempProject
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}