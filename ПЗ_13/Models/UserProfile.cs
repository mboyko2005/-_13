using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПЗ_13.Models
{
	public class UserProfile
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Settings { get; set; }

		public UserProfile Clone()
		{
			return new UserProfile
			{
				Name = this.Name,
				Email = this.Email,
				Settings = this.Settings
			};
		}
	}
}


