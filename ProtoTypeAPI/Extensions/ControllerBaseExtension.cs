using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ProtoType.Model.DSTI;

namespace ProtoTypeAPI.Extensions
{
    /// <summary>
    /// Controller Base Extension
    /// </summary>
    public static class ControllerBaseExtension
    {
        /// <summary>
        /// Return Not Found Result
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static IActionResult NotFoundObject(this ControllerBase controller)
        {
            Error error = new();
            error.Code = (int)HttpStatusCode.NotFound;
            error.Message = "Not Found";
            error.Type = "Handled Exception";
            error.ErrorID = DateTime.UtcNow.Ticks;

            return controller.NotFound(error);
        }
    }
}

