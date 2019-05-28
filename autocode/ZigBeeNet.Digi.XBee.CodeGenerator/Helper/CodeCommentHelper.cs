using System.CodeDom;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Digi.XBee.CodeGenerator.Entities;

namespace ZigBeeNet.Digi.XBee.CodeGenerator.Helper
{
    public static class CodeCommentHelper
    {
        /// <summary>
        /// Builds the code comment statement.
        /// </summary>
        /// <param name="codeCommentEntity">The code comment entity.</param>
        /// <param name="isDocComment">if set to <c>true</c> is document comment.</param>
        /// <returns>A single code comment</returns>
        public static CodeCommentStatement BuildCodeCommentStatement(ICodeCommentEntity codeCommentEntity, bool isDocComment)
        {
            StringBuilder stringBuilder = new StringBuilder($"<{codeCommentEntity.Tag.ToString().ToLower()}");

            foreach (var item in codeCommentEntity.Attributes)
            {
                stringBuilder.AppendLine($"{item.Key.ToString().ToLower()}=\"{item.Value}\"");
            }
            stringBuilder.AppendLine(">");

            stringBuilder.Append($" {codeCommentEntity.DocumentationText}");

            stringBuilder.Append($" </{codeCommentEntity.Tag.ToString().ToLower()}>");

            return new CodeCommentStatement(stringBuilder.ToString(), isDocComment);
        }

        /// <summary>
        /// Builds the code comment statement collection.
        /// </summary>
        /// <param name="codeCommentEntities">The code comment entities.</param>
        /// <returns>A doc comment collection</returns>
        public static CodeCommentStatementCollection BuildCodeCommentStatementCollection(IEnumerable<ICodeCommentEntity> codeCommentEntities)
        {
            CodeCommentStatementCollection codeCommentStatementCollection = new CodeCommentStatementCollection();
            foreach (var codeCommentEntity in codeCommentEntities)
            {
                CodeCommentStatement codeCommentStatement = BuildCodeCommentStatement(codeCommentEntity, true);
                codeCommentStatementCollection.Add(codeCommentStatement);
            }

            return codeCommentStatementCollection;
        }
    }
}
