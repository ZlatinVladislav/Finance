using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Finance.Unit.Test.Controllers.Helpers
{
    public class ControllerTestBase
    {
        protected void Is<T>(IActionResult result, HttpStatusCode statusCode) where T : StatusCodeResult
        {
            var r = result as T;
            Assert.NotNull(r);
            r.Should().NotBeNull();
            r.StatusCode.Should().Be((int)statusCode);
        }

        protected TPayload Is<TResult, TPayload>(IActionResult result, HttpStatusCode statusCode) where TResult : ObjectResult
        {
            var r = result as TResult;
            r.Should().NotBeNull();
            r.StatusCode.Should().Be((int)statusCode);
            r.Value.Should().BeAssignableTo<TPayload>();
            return (TPayload)r.Value;
        }
    }
}
