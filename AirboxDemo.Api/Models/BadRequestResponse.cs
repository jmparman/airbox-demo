using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace AirboxDemo.Api.Models
{
    public class BadRequestResponse
    {
        public IEnumerable<string> Errors { get; }
      
        public BadRequestResponse()
        {
            this.Errors = new string[0];
        }

        public BadRequestResponse(ModelStateDictionary modelState)
        {
            this.Errors = modelState.SelectMany(state => state.Value.Errors.Select(error => error.ErrorMessage));
        }
    }
}
