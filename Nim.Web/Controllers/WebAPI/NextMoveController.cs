// <copyright file="NextMoveController.cs" company="AggieEngineer2K">
// Copyright (c) AggieEngineer2K. All rights reserved.
// </copyright>

namespace Nim.Web.Controllers.WebAPI
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Nim.Web.Models.WebAPI.NextMove;

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
        /// <param name="model">The POST model.</param>
        /// <returns>An ActionResult.</returns>
        [HttpPost]
        public ActionResult Post([FromBody]NextMovePostModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.Ok(Nim.Solver.NextMove.Solve(model.Heaps));
        }
    }
}
