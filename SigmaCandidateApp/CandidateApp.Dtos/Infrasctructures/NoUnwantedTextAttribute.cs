using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CandidateApp.Dtos.Infrasctructures
{
    public class NoUnwantedTextAttribute : ValidationAttribute
    {
        private readonly List<string> _unwantedWords = new List<string> { "spam", "hate", "violence" };
        private readonly List<string> _hackingKeywords = new List<string> { "DROP TABLE", "SELECT *", "UNION SELECT", "INSERT INTO", "DELETE FROM", "UPDATE", "ALTER", "SCRIPT", "eval", "document.location", "window.location", "onmouseover", "onclick" };


        public NoUnwantedTextAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // 1. Check for unwanted words
            if (value is string comment)
            {
                // Check if the comment contains any unwanted words (case insensitive)
                foreach (var word in _unwantedWords)
                {
                    if (comment.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return new ValidationResult(ErrorMessage ?? "The comment contains unwanted text.", new[] { validationContext.MemberName });
                    }
                }


                // 2. Check for hacking-related keywords (SQL injection, script injections, etc.)
                foreach (var keyword in _hackingKeywords)
                {
                    if (Regex.IsMatch(comment, @"\b" + Regex.Escape(keyword) + @"\b", RegexOptions.IgnoreCase))
                    {
                        return new ValidationResult(ErrorMessage ?? "The comment contains potentially harmful content (e.g., SQL injection, script tags).", new[] { validationContext.MemberName });
                    }
                }

                // 3. Check for script tags (like <script>, JavaScript code)
                if (Regex.IsMatch(comment, @"<\s*script.*?>.*?<\s*/\s*script\s*>", RegexOptions.IgnoreCase))
                {
                    return new ValidationResult(ErrorMessage ?? "The comment contains script tags, which are not allowed.", new[] { validationContext.MemberName });
                }
            }

            // Return null if the comment is valid
            return ValidationResult.Success;
        }
    }
}
