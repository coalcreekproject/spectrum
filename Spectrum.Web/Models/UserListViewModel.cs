using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spectrum.Web.Models
{
    public class UserListViewModel
    {
        public List<UserViewModel> Available;
        public List<UserViewModel> Assigned;
    }
}