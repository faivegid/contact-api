using System.Collections.Generic;

namespace Contact.Logic.Validators.ErrorModel
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
