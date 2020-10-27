// <copyright file="NextMoveController.cs" company="AggieEngineer2K">
// Copyright (c) AggieEngineer2K. All rights reserved.
// </copyright>

namespace Nim.Web.Controllers.WebAPI
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The next move controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NextMoveController : ControllerBase
    {
        /// <summary>
        /// The POST action.
        /// </summary>
        /// <param name="heaps">The heap sizes.</param>
        /// <returns>An ActionResult.</returns>
        [HttpPost]
        public ActionResult Post([FromBody]int[] heaps)
        {
            var nextMove = Nim.Solver.NextMove.Solve(heaps);
            if (nextMove.HasValue)
            {
                return this.Ok(new
                {
                    nextMove.Value.heap,
                    nextMove.Value.number,
                });
            }
            else
            {
                return this.Ok(new { });
            }
        }
    }
}
