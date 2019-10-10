using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace NASMClassifier
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "nasm.default")]
    [Name("nasm.default")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class NASMDefault : ClassificationFormatDefinition
    {

        public NASMDefault()
        {
            DisplayName = "NASM-Default";
            ForegroundColor = Colors.White;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "nasm.comment")]
    [Name("nasm.comment")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class NASMComment : ClassificationFormatDefinition
    {

        public NASMComment()
        {
            DisplayName = "NASM-Comment";
            ForegroundColor = Color.FromRgb(98, 114, 164);
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "nasm.number")]
    [Name("nasm.number")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class NASMNumber : ClassificationFormatDefinition
    {

        public NASMNumber()
        {
            DisplayName = "NASM-Number";
            ForegroundColor = Color.FromRgb(189, 147, 249);
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "nasm.operator")]
    [Name("nasm.operator")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class NASMOperator : ClassificationFormatDefinition
    {

        public NASMOperator()
        {
            DisplayName = "NASM-Operator";
            ForegroundColor = Color.FromRgb(180, 180, 180);
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "nasm.cpu-instruction")]
    [Name("nasm.cpu-instruction")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class NASMCPUInstruction : ClassificationFormatDefinition
    {

        public NASMCPUInstruction()
        {
            DisplayName = "NASM-CPU Instruction";
            ForegroundColor = Color.FromRgb(80, 250, 123);
            IsBold = true;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "nasm.math-instruction")]
    [Name("nasm.math-instruction")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class NASMMathInstruction : ClassificationFormatDefinition
    {

        public NASMMathInstruction()
        {
            DisplayName = "NASM-Math Instruction";
            ForegroundColor = Color.FromRgb(39, 139, 39);
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "nasm.register")]
    [Name("nasm.register")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class NASMRegister : ClassificationFormatDefinition
    {

        public NASMRegister()
        {
            DisplayName = "NASM-Register";
            ForegroundColor = Color.FromRgb(250, 250, 210);
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "nasm.directive")]
    [Name("nasm.directive")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class NASMDirective : ClassificationFormatDefinition
    {

        public NASMDirective()
        {
            DisplayName = "NASM-Directive";
            ForegroundColor = Color.FromRgb(139, 233, 253);
            IsBold = true;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "nasm.directive-operand")]
    [Name("nasm.directive-operand")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class NASMDirectiveOperand : ClassificationFormatDefinition
    {

        public NASMDirectiveOperand()
        {
            DisplayName = "NASM-Directive Operand";
            ForegroundColor = Color.FromRgb(139, 176, 253);
            IsBold = true;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "nasm.label")]
    [Name("nasm.label")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class NASMLabel : ClassificationFormatDefinition
    {

        public NASMLabel()
        {
            DisplayName = "NASM-Label";
            ForegroundColor = Color.FromRgb(255, 184, 108);
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "nasm.string")]
    [Name("nasm.string")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class NASMString : ClassificationFormatDefinition
    {

        public NASMString()
        {
            DisplayName = "NASM-String";
            ForegroundColor = Color.FromRgb(252, 252, 153);
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "nasm.character")]
    [Name("nasm.character")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class NASMCharacter : ClassificationFormatDefinition
    {

        public NASMCharacter()
        {
            DisplayName = "NASM-Character";
            ForegroundColor = Color.FromRgb(252, 252, 153);
        }
    }
}
