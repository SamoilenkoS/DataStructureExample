using System.Collections.Generic;

namespace EFDemo
{
    /*
     * Create method for list sorting fuction (User{FirstName,LastN,DateOfBirthday,PhoneNumber})
  *Generic version
Create delegate which will allow to set Console.ReadLine()\WriteLine() inside it.
There is User{FirstName,LastN,DateOfBirthday,PhoneNumber}. Create method to filter users list.
  *Create universal method to filter any collection (generic)
Create method which will repeat code part (should be as input parameter) while condition (should be as input parameter) is true N times.
     */
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }

        public override string ToString()
        {
            return $"{BlogId} {Url} {Rating}";
        }
    }
}
