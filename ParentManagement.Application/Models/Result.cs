using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentManagement.Application.Models
{
    public class Result
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public ErrorCode Code { get; set; }

        /// <summary>
        /// Optional machine-oriented details for logging/diagnostics.
        /// Not intended for direct user display.
        /// </summary>
        public string? Details { get; set; }
    }

    public enum ErrorCode
    {
        Ok,
        InvalidOrder,
        EmptyOrder,
        SchoolNotFound,
        ProductNotFound,
        OutOfStock,
        PaymentFailed
    }
}
