// <copyright file="HomeController.cs" company="AggieEngineer2K">
// Copyright (c) AggieEngineer2K. All rights reserved.
// </copyright>

namespace Nim.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Nim.Web.Models;

    /// <summary>
    /// The Home controller.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">An injected dependency on <see cref="ILogger{HomeController}"/>.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// The Index action.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/>.</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// The Privacy action.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/>.</returns>
        public IActionResult Privacy()
        {
            return this.View();
        }

        /// <summary>
        /// The Error action.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/>.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
