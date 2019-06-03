using System.Collections.Generic;
using ZigBeeNet.Digi.XBee.CodeGenerator.Enumerations;

namespace ZigBeeNet.Digi.XBee.CodeGenerator.Entities
{
    /// <summary>
    /// Entity for a code comment
    /// </summary>
    public class CodeCommentEntity : ICodeCommentEntity
    {
        #region constructor

        /// <summary>
        ///  Initializes a new instance of the <see cref="CodeCommentEntity"/> class.
        /// </summary>
        public CodeCommentEntity()
        {
            Attributes = new Dictionary<CodeCommentAttribute, string>();
        }

        #endregion constructor

        #region properties

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public CodeCommentTag Tag { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string DocumentationText { get; set; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public IDictionary<CodeCommentAttribute, string> Attributes { get; }

        #endregion properties
    }
}
