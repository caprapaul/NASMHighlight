namespace NASMClassifier
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;

    internal sealed class NasmClassifier : ITagger<ClassificationTag>
    {
        ITextBuffer _buffer;
        ITagAggregator<NASMTokenTag> _aggregator;
        IClassificationTypeRegistryService _typeService;

        /// <summary>
        /// Construct the classifier and define search tokens
        /// </summary>
        internal NasmClassifier(ITextBuffer buffer,
                               ITagAggregator<NASMTokenTag> nasmTagAggregator,
                               IClassificationTypeRegistryService typeService)
        {
            _buffer = buffer;
            _aggregator = nasmTagAggregator;
            _typeService = typeService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        private IClassificationType GetClassificationType(NASMTokenTypes type)
        {
            switch (type)
            {
                case NASMTokenTypes.Default:
                    return _typeService.GetClassificationType("nasm.default");
                case NASMTokenTypes.Comment:
                    return _typeService.GetClassificationType("nasm.comment");
                case NASMTokenTypes.Number:
                    return _typeService.GetClassificationType("nasm.number");
                case NASMTokenTypes.Character:
                    return _typeService.GetClassificationType("nasm.character");
                case NASMTokenTypes.String:
                    return _typeService.GetClassificationType("nasm.string");
                case NASMTokenTypes.Operator:
                    return _typeService.GetClassificationType("nasm.operator");
                case NASMTokenTypes.CPUInstruction:
                    return _typeService.GetClassificationType("nasm.cpu-instruction");
                case NASMTokenTypes.MathInstruction:
                    return _typeService.GetClassificationType("nasm.math-instruction");
                case NASMTokenTypes.Register:
                    return _typeService.GetClassificationType("nasm.register");
                case NASMTokenTypes.Directive:
                    return _typeService.GetClassificationType("nasm.directive");
                case NASMTokenTypes.DirectiveOperand:
                    return _typeService.GetClassificationType("nasm.directive-operand");
                case NASMTokenTypes.ExtInstruction:
                    return _typeService.GetClassificationType("nasm.ext-instruction");
                case NASMTokenTypes.Label:
                    return _typeService.GetClassificationType("nasm.label");
                default:
                    return _typeService.GetClassificationType("nasm.default");
            }
        }

        /// <summary>
        /// Search the given span for any instances of classified tags
        /// </summary>
        public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var tagSpan in _aggregator.GetTags(spans))
            {
                var tagSpans = tagSpan.Span.GetSpans(spans[0].Snapshot);
                yield return new TagSpan<ClassificationTag>(tagSpans[0], new ClassificationTag(GetClassificationType(tagSpan.Tag.type)));
            }
        }
    }
}
