using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASMClassifier
{
    public enum NASMTokenTypes
    {
        Default,
        Comment,
        Number,
        Character,
        String,
        Operator,
        CPUInstruction,
        MathInstruction,
        Register,
        Directive,
        DirectiveOperand,
        ExtInstruction,
        Label
    }
}
