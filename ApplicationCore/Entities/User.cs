using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities {
    public class User {
        public int Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public bool IsLocked { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;

        //Navigation Properties
        public virtual ICollection<Favorite> Favorites { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }


    }
}
