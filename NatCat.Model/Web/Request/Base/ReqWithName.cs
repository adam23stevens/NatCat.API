using System;
using System.ComponentModel.DataAnnotations;

namespace NatCat.DAL.Web.Request.Base
{
	public class ReqWithName
	{
		[Required(ErrorMessage = "Name is required in request")]
		public required string Name {get;set;}
	}
}
	
