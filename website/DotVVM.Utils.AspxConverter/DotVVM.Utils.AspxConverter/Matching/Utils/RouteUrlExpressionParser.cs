using System;
using System.Collections.Generic;

namespace DotVVM.Utils.AspxConverter.Matching.Utils
{
    public class RouteUrlExpressionParser
    {

        public static bool TryParse(string bindingExpression, out string routeName, out Dictionary<string, string> routeValues)
        {
            routeName = null;
            routeValues = new Dictionary<string, string>();

            var (type, expression) = ParseBindingExpression(bindingExpression);
            if (!string.Equals(type, "RouteUrl", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            
            if (string.IsNullOrEmpty(expression))
                return false;

            var pieces = expression.Split(new char[] { ',' });
            foreach (string piece in pieces)
            {
                string[] subs = piece.Split(new char[] { '=' });
                // Make sure we have exactly <key> = <value>
                if (subs.Length != 2)
                {
                    return false;
                }

                var key = subs[0].Trim();
                var value = subs[1].Trim();

                if (string.IsNullOrEmpty(key))
                {
                    return false;
                }

                if (key.Equals("RouteName", StringComparison.OrdinalIgnoreCase))
                {
                    routeName = value;
                }
                else
                {
                    routeValues[key] = value;
                }
            }

            return (routeName != null);
        }

        private static (string type, string value) ParseBindingExpression(string expression)
        {
            if (expression.StartsWith("<%$"))
            {
                expression = expression[3..];
            }
            if (expression.EndsWith("%>"))
            {
                expression = expression[..^2];
            }
            expression = expression.Trim();

            var type = expression.IndexOf(':');
            if (type == -1)
            {
                return (string.Empty, expression);
            }
            return (expression[..type].Trim(), expression[(type + 1)..].Trim());
        }
    }
}