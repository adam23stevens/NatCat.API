using System;
namespace NatCat.Model.Exception
{
	public class BadRequestException : ApplicationException
	{
		public BadRequestException(string exceptionMessage) : base (exceptionMessage)
		{
		}
	}
}

