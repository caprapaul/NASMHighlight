using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace NASMClassifier
{
    /// <summary>
    /// Classification type definition export for NASMEditorClassifier
    /// </summary>
    internal static class NASMClassificationDefinition
    {
        #region Type definition

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("nasm.default")]
        internal static ClassificationTypeDefinition NasmDefault = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("nasm.comment")]
        internal static ClassificationTypeDefinition NasmComment = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("nasm.number")]
        internal static ClassificationTypeDefinition NasmNumber = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("nasm.operator")]
        internal static ClassificationTypeDefinition NASMOperator = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("nasm.cpu-instruction")]
        internal static ClassificationTypeDefinition NASMCPUInstruction = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("nasm.math-instruction")]
        internal static ClassificationTypeDefinition NASMMathInstruction = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("nasm.register")]
        internal static ClassificationTypeDefinition NASMRegister = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("nasm.directive")]
        internal static ClassificationTypeDefinition NASMDirective = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("nasm.directive-operand")]
        internal static ClassificationTypeDefinition NASMDirectiveOperand = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("nasm.label")]
        internal static ClassificationTypeDefinition NASMLabel = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("nasm.string")]
        internal static ClassificationTypeDefinition NASMString = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("nasm.character")]
        internal static ClassificationTypeDefinition NASMCharacter = null;

        #endregion

    }
}
