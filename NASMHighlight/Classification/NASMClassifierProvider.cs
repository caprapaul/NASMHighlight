using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

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

    [Export(typeof(ITaggerProvider))]
    [ContentType("nasm")]
    [TagType(typeof(ClassificationTag))]
    internal sealed class NasmClassifierProvider : ITaggerProvider
    {

        [Export]
        [Name("nasm")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition OokContentType = null;

        [Export]
        [FileExtension(".asm")]
        [ContentType("nasm")]
        internal static FileExtensionToContentTypeDefinition NasmFileType = null;

        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry = null;

        [Import]
        internal IBufferTagAggregatorFactoryService AggregatorFactory = null;

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {

            ITagAggregator<NASMTokenTag> nasmTagAggregator = AggregatorFactory.CreateTagAggregator<NASMTokenTag>(buffer);

            return new NasmClassifier(buffer, nasmTagAggregator, ClassificationTypeRegistry) as ITagger<T>;
        }
    }
}
