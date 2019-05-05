using System.Collections.Generic;
using ZigBeeNet.Digi.XBee.CodeGenerator.Enumerations;

namespace ZigBeeNet.Digi.XBee.CodeGenerator.Entities
{
    public interface ICodeCommentEntity
    {
        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        CodeCommentTag Tag { get; set; }

        /// <summary>
        /// Gets or sets the documentation text.
        /// </summary>
        /// <value>
        /// The documentation text.
        /// </value>
        string DocumentationText { get; set; }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        IDictionary<CodeCommentAttribute, string> Attributes { get; }
    }
}