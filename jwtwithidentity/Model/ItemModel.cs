using System.ComponentModel.DataAnnotations;

namespace jwtwithidentity.Model
{
    public class ItemModel
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
         
        public string Email { get; set; }

        public string Mobile { get; set; }
    }
}
