using ZigBeeNet.Digi.XBee.CodeGenerator.Enumerations;

namespace ZigBeeNet.Digi.XBee.CodeGenerator.Entities
{
    public class CodeCommentAttributeEntity : ICodeCommentAttributeEntity
    {
        /// <summary>
        /// Gets or sets the code comment attribute.
        /// </summary>
        /// <value>
        /// The code comment attribute.
        /// </value>
        public CodeCommentAttribute CodeCommentAttribute { get; set; }

        /// <summary>
        /// Gets or sets the attribute text.
        /// </summary>
        /// <value>
        /// The attribute text.
        /// </value>
        public string AttributeText { get; set; }
    }
}
