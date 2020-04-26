namespace DotVVM.Utils.AspxConverter.Parser.Tokens
{
    public class CommentToken : AspxBlockToken
    {
        public CommentToken(int position, string openBraceFragment, string content, string closeBraceFragment) : base(position, openBraceFragment, string.Empty, content, closeBraceFragment)
        {
        }
    }
}