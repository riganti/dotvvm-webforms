using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DotVVM.Utils.AspxConverter.Workspace
{
    public class CodeGenUtils
    {

        public static string Indent(int levels, string text)
        {
            var sb = new StringBuilder();

            var reader = new StringReader(text);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                for (var i = 0; i < levels; i++)
                {
                    sb.Append("    ");
                }
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

    }
}