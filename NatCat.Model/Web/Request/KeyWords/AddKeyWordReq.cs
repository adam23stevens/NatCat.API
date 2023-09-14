using System;
using System.ComponentModel.DataAnnotations;

namespace NatCat.DAL.Web.Request.Base
{
    namespace NatCat.API.Model.Request.KeyWords
    {
        public class AddKeyWordReq : ReqWithParentId
        {
            [Required(ErrorMessage = "Word is required")]
            public required string Word { get; set; }
            public int Difficulty { get; set; }
        }
    }
}
