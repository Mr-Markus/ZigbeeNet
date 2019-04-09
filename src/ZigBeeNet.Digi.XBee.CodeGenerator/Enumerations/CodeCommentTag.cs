using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Digi.XBee.CodeGenerator.Enumerations
{
    /// <summary>
    /// The available code comment tags
    /// </summary>
    public enum CodeCommentTag
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/recommended-tags-for-documentation-comments
        C,
        Code,
        Example,
        Exception,
        Include,
        List,
        Para,
        Param,
        Paramref,
        Permission,
        Remarks,
        Returns,
        See,
        Seealso,
        Summary,
        Typeparam,
        Typeparamref,
        Value
    }
}
