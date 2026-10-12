using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Ucu.Poo.PersonExporter
{
    public class ExportMarkDown : ReportExporter
    {
        public override void ExportContent(IList<Person> people, string outputPath)
        {
            GenerateMarkdown(people, outputPath);
        }

        // Escapa caracteres especiales de Markdown en un texto simple.
        private static string EscapeMarkdown(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            string[] charsToEscape = new[] { "|", "*", "_", "`" };
            string result = value;

            foreach (string c in charsToEscape)
            {
                result = result.Replace(c, "\\" + c, StringComparison.Ordinal);
            }

            return result;
        }

        private void GenerateMarkdown(IList<Person> people, string outputPath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("# Person Report");
            sb.AppendLine();
            sb.AppendLine("| First Name | Last Name | Age |");
            sb.AppendLine("|-----------|-----------|-----|");

            foreach (Person person in people)
            {
                sb.AppendLine($"| {EscapeMarkdown(person.FirstName)} | {EscapeMarkdown(person.LastName)} | {person.Age} |");
            }

            string markdown = sb.ToString();
            File.WriteAllText(outputPath, markdown, Encoding.UTF8);
        }
    }
}