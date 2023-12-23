using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotVVM.Utils.AspxConverter.Matching.Utils
{
    public class BindingTranslationVisitor : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            // Item.Property or BindItem.Property
            if (node.Expression is IdentifierNameSyntax { Identifier: { Value: "Item" or "BindItem" } })
            {
                return node.Name;
            }

            return base.VisitMemberAccessExpression(node);
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            // Eval(expression) or Eval(expression, formatString)
            // Bind(expression) or Bind(expression, formatString)
            if (node.Expression is IdentifierNameSyntax { Identifier: { Value: "Eval" or "Bind" } })
            {
                if (node.ArgumentList.Arguments[0].Expression is LiteralExpressionSyntax propName)
                {
                    if (node.ArgumentList.Arguments.Count == 1)
                    {
                        return TranslateEval(propName, null);
                    }
                    else if (node.ArgumentList.Arguments.Count == 2
                        && node.ArgumentList.Arguments[1].Expression is LiteralExpressionSyntax formatString)
                    {
                        return TranslateEval(propName, formatString);
                    }
                }
            }

            // DataBinder.Eval(_, expression) or DataBinder.Eval(_, expression, formatString)
            // DataBinder.Bind(_, expression) or DataBinder.Bind(_, expression, formatString)
            if (node.Expression is MemberAccessExpressionSyntax
            {
                Name: { Identifier: { Value: "Eval" or "Bind" } },
                Expression: IdentifierNameSyntax { Identifier: { Value: "DataBinder" } }
            })
            {
                if (node.ArgumentList.Arguments[1].Expression is LiteralExpressionSyntax propName)
                {
                    if (node.ArgumentList.Arguments.Count == 2)
                    {
                        return TranslateEval(propName, null);
                    }
                    else if (node.ArgumentList.Arguments.Count == 3
                        && node.ArgumentList.Arguments[2].Expression is LiteralExpressionSyntax formatString)
                    {
                        return TranslateEval(propName, formatString);
                    }
                }
            }

            return base.VisitInvocationExpression(node);
        }

        private SyntaxNode TranslateEval(LiteralExpressionSyntax expression, LiteralExpressionSyntax formatString)
        {
            if (formatString == null)
            {
                return SyntaxFactory.IdentifierName((string)expression.Token.Value);
            }
            else
            {
                return
                    SyntaxFactory.InvocationExpression(
                        SyntaxFactory.MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            SyntaxFactory.IdentifierName("string"),
                            SyntaxFactory.IdentifierName("Format")
                        ),
                        SyntaxFactory.ArgumentList(
                            SyntaxFactory.SeparatedList(new[]
                            {
                                SyntaxFactory.Argument(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        SyntaxFactory.Literal((string)formatString.Token.Value)
                                    )
                                ),
                                SyntaxFactory.Argument(
                                    SyntaxFactory.IdentifierName((string)expression.Token.Value)
                                )
                            })
                        )
                    );
            }
        }
    }
}
