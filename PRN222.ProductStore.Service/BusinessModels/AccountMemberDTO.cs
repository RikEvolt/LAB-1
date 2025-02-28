using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Service.BusinessModels
{
	public partial class AccountMemberDTO
	{
		public string? MemberId { get; set; }

		[Required(ErrorMessage = "Mật khẩu không được để trống")]
		public string MemberPassword { get; set; }

		public string? FullName { get; set; }

		[Required(ErrorMessage = "Email không được để trống")]
		[EmailAddress(ErrorMessage = "Email không hợp lệ")]
		public string EmailAddress { get; set; }

		public int? MemberRole { get; set; }
	}
}
