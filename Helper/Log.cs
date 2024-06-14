using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VnStockproxx
{
    public static class Log
    {
        public static void LogError(ModelStateDictionary ModelState)
        {
            Console.WriteLine($"vao day roi ne2");
            // Log lỗi validation
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Error in {state.Key}: {error.ErrorMessage}");
                }
            }
        }
    }
}
