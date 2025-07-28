using PMACS_V2.Models;
using System.Web.Mvc;

namespace PMACS_V2.Utilities
{
    public class ProblemDetailsActionResult : ActionResult
    {
        private readonly ProblemDetails _problemDetails;

        public ProblemDetailsActionResult(ProblemDetails problemDetails)
        {
            _problemDetails = problemDetails;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.StatusCode = _problemDetails.Status;
            response.ContentType = "application/problem+json";
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(_problemDetails);
            response.Write(json);
        }
    }
}