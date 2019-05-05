using ZigBeeNet.Digi.XBee.CodeGenerator.Enumerations;

namespace ZigBeeNet.Digi.XBee.CodeGenerator.Entities
{
    public interface ICodeCommentAttributeEntity
    {
        /// <summary>
        /// Gets or sets the attribute text.
        /// </summary>
        /// <value>
        /// The attribute text.
        /// </value>
        string AttributeText { get; set; }

        /// <summary>
        /// Gets or sets the code comment attribute.
        /// </summary>
        /// <value>
        /// The code comment attribute.
        /// </value>
        CodeCommentAttribute CodeCommentAttribute { get; set; }
    }
}