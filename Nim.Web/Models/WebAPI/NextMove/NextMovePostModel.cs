// <copyright file="NextMovePostModel.cs" company="AggieEngineer2K">
// Copyright (c) AggieEngineer2K. All rights reserved.
// </copyright>

namespace Nim.Web.Models.WebAPI.NextMove
{
    using System.Collections.Generic;

    /// <summary>
    /// A POST model.
    /// </summary>
    public class NextMovePostModel
    {
        /// <summary>
        /// Gets or sets the heaps.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Model binder needs to be able to set this property value.")]
        public ICollection<int> Heaps { get; set; }
    }
}
