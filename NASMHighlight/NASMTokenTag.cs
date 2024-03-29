﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASMClassifier
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Text.RegularExpressions;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;

    [Export(typeof(ITaggerProvider))]
    [ContentType("nasm")]
    [TagType(typeof(NASMTokenTag))]
    internal sealed class NASMTokenTagProvider : ITaggerProvider
    {

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            return new NASMTokenTagger(buffer) as ITagger<T>;
        }
    }

    public class NASMTokenTag : ITag
    {
        public NASMTokenTypes type { get; private set; }

        public NASMTokenTag(NASMTokenTypes type)
        {
            this.type = type;
        }
    }

    internal sealed class NASMTokenTagger : ITagger<NASMTokenTag>
    {
        private ITextBuffer _buffer;
        private IDictionary<NASMTokenTypes, string> _nasmTypes;
        private string _delimiters;

        internal NASMTokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
            InitTypes();
            _delimiters = "\t;,[]()\"' ";
        }

        private void InitTypes()
        {
            _nasmTypes = new Dictionary<NASMTokenTypes, string>()
            {
                {
                    NASMTokenTypes.String,
                    "\""
                },
                {
                    NASMTokenTypes.Character,
                    "'"
                },
                {
                    NASMTokenTypes.Comment, 
                    ";"
                },
                {
                    NASMTokenTypes.Operator,
                    "( ) [ ]"
                },
                {
                    NASMTokenTypes.CPUInstruction,
                    "aaa aad aam aas adc add and call cbw cdqe clc cld cli cmc cmp cmps cmpsb cmpsw cwd daa das dec div esc hlt idiv imul in inc int into iret ja jae jb jbe jc jcxz je jg jge jl jle jmp jna jnae jnb jnbe jnc jne jng jnge jnl jnle jno jnp jns jnz jo jp jpe jpo js jz lahf lds lea les lods lodsb lodsw loop loope loopew loopne loopnew loopnz loopnzw loopw loopz loopzw mov movabs movs movsb movsw mul neg nop not or out pop popf push pushf rcl rcr ret retf retn rol ror sahf sal sar sbb scas scasb scasw shl shr stc std sti stos stosb stosw sub test wait xchg xlat xlatb xor bound enter ins insb insw leave outs outsb outsw popa pusha pushw arpl lar lsl sgdt sidt sldt smsw str verr verw clts lgdt lidt lldt lmsw ltr bsf bsr bt btc btr bts cdq cmpsd cwde insd iretd iretdf iretf jecxz lfs lgs lodsd loopd looped loopned loopnzd loopzd lss movsd movsx movsxd movzx outsd popad popfd pushad pushd pushfd scasd seta setae setb setbe setc sete setg setge setl setle setna setnae setnb setnbe setnc setne setng setnge setnl setnle setno setnp setns setnz seto setp setpe setpo sets setz shld shrd stosd bswap cmpxchg invd invlpg wbinvd xadd lock rep repe repne repnz repz cflush cpuid emms femms cmovo cmovno cmovb cmovc cmovnae cmovae cmovnb cmovnc cmove cmovz cmovne cmovnz cmovbe cmovna cmova cmovnbe cmovs cmovns cmovp cmovpe cmovnp cmovpo cmovl cmovnge cmovge cmovnl cmovle cmovng cmovg cmovnle cmpxchg486 cmpxchg8b loadall loadall286 ibts icebp int1 int3 int01 int03 iretw popaw popfw pushaw pushfw rdmsr rdpmc rdshr rdtsc rsdc rsldt rsm rsts salc smi smint smintold svdc svldt svts syscall sysenter sysexit sysret ud0 ud1 ud2 umov xbts wrmsr wrshr"
                },
                {
                    NASMTokenTypes.MathInstruction,
                    "f2xm1 fabs fadd faddp fbld fbstp fchs fclex fcom fcomp fcompp fdecstp fdisi fdiv fdivp fdivr fdivrp feni ffree fiadd ficom ficomp fidiv fidivr fild fimul fincstp finit fist fistp fisub fisubr fld fld1 fldcw fldenv fldenvw fldl2e fldl2t fldlg2 fldln2 fldpi fldz fmul fmulp fnclex fndisi fneni fninit fnop fnsave fnsavew fnstcw fnstenv fnstenvw fnstsw fpatan fprem fptan frndint frstor frstorw fsave fsavew fscale fsqrt fst fstcw fstenv fstenvw fstp fstsw fsub fsubp fsubr fsubrp ftst fwait fxam fxch fxtract fyl2x fyl2xp1 fsetpm fcos fldenvd fnsaved fnstenvd fprem1 frstord fsaved fsin fsincos fstenvd fucom fucomp fucompp fcomi fcomip ffreep fcmovb fcmove fcmovbe fcmovu fcmovnb fcmovne fcmovnbe fcmovnu"
                },
                {
                    NASMTokenTypes.Register,
                    "ah al ax bh bl bp bx ch cl cr0 cr2 cr3 cr4 cs cx dh di dl dr0 dr1 dr2 dr3 dr6 dr7 ds dx eax ebp ebx ecx edi edx es esi esp fs gs rax rbx rcx rdx rdi rsi rbp rsp r8 r9 r10 r11 r12 r13 r14 r15 r8d r9d r10d r11d r12d r13d r14d r15d r8w r9w r10w r11w r12w r13w r14w r15w r8b r9b r10b r11b r12b r13b r14b r15b si sp ss st tr3 tr4 tr5 tr6 tr7 st0 st1 st2 st3 st4 st5 st6 st7 mm0 mm1 mm2 mm3 mm4 mm5 mm6 mm7 xmm0 xmm1 xmm2 xmm3 xmm4 xmm5 xmm6 xmm7 xmm8 xmm9 xmm10 xmm11 xmm12 xmm13 xmm14 xmm15"
                },
                {
                    NASMTokenTypes.Directive,
                    ".186 .286 .286c .286p .287 .386 .386c .386p .387 .486 .486p .8086 .8087 .alpha .break .code .const .continue .cref .data .data? .dosseg .else .elseif .endif .endw .err .err1 .err2 .errb .errdef .errdif .errdifi .erre .erridn .erridni .errnb .errndef .errnz .exit .fardata .fardata? .if .lall .lfcond .list .listall .listif .listmacro .listmacroall .model .no87 .nocref .nolist .nolistif .nolistmacro .radix .repeat .sall .seq .sfcond .stack .startup .tfcond .type .until .untilcxz .while .xall .xcref .xlist alias align assume catstr comm comment db dd df dosseg dq dt dup dw echo else elseif elseif1 elseif2 elseifb elseifdef elseifdif elseifdifi elseife elseifidn elseifidni elseifnb elseifndef end endif endm endp ends eq equ even exitm extern externdef extrn for forc ge goto group gt high highword if if1 if2 ifb ifdef ifdif ifdifi ife ifidn ifidni ifnb ifndef include includelib instr invoke irp irpc label le length lengthof local low lowword lroffset lt macro mask mod .msfloat name ne offset opattr option org %out page popcontext proc proto ptr public purge pushcontext record repeat rept seg segment short size sizeof sizestr struc struct substr subtitle subttl textequ this title type typedef union while width resb resw resd resq rest incbin times %define %idefine %xdefine %xidefine %undef %assign %iassign %strlen %substr %macro %imacro %endmacro %rotate %if %elif %else %endif %ifdef %ifndef %elifdef %elifndef %ifmacro %ifnmacro %elifmacro %elifnmacro %ifctk %ifnctk %elifctk %elifnctk %ifidn %ifnidn %elifidn %elifnidn %ifidni %ifnidni %elifidni %elifnidni %ifid %ifnid %elifid %elifnid %ifstr %ifnstr %elifstr %elifnstr %ifnum %ifnnum %elifnum %elifnnum %error %rep %endrep %exitrep %include %push %pop %repl endstruc istruc at iend alignb %arg %stacksize %local %line bits use16 use32 section absolute global common cpu import export"
                },
                {
                    NASMTokenTypes.DirectiveOperand,
                    "$ ? @b @f addr basic byte carry? dword far far16 fortran fword near near16 overflow? parity? pascal qword real4 real8 real10 sbyte sdword sign? stdcall sword syscall tbyte vararg word zero? flat near32 far32 abs all assumes at casemap common compact cpu dotname emulator epilogue error export expr16 expr32 farstack forceframe huge language large listing ljmp loadds m510 medium memory nearstack nodotname noemulator nokeyword noljmp nom510 none nonunique nooldmacros nooldstructs noreadonly noscoped nosignextend nothing notpublic oldmacros oldstructs os_dos para private prologue radix readonly req scoped setif2 smallstack tiny use16 use32 uses a16 a32 o16 o32 nosplit $$ seq wrt small .text .data .bss %0 %1 %2 %3 %4 %5 %6 %7 %8 %9"
                }
            };

        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        public NASMTokenTypes GetTokenType(string token)
        {
            if (String.IsNullOrEmpty(token))
            {
                return NASMTokenTypes.Default;
            }

            foreach (KeyValuePair<NASMTokenTypes, string> type in _nasmTypes)
            {
                
                if (type.Value.ToLower().Split(' ').Contains(token.ToLower()))
                {
                    return type.Key;
                }
            }

            if (Regex.IsMatch(token, @"\w+:$", RegexOptions.IgnoreCase))
            {
                return NASMTokenTypes.Label;
            }

            if (Regex.IsMatch(token, @"^\d[0-9a-f]*", RegexOptions.IgnoreCase))
            {
                return NASMTokenTypes.Number;
            }

            return NASMTokenTypes.Default;
        }

        private string[] Split(string target, char[] separators)
        {
            List<string> splitList = new List<string>();

            string word = "";

            for (int i = 0; i < target.Length; i++)
            {
                char c = target[i];

                if (separators.Contains(c))
                {
                    if (word != "")
                    {
                        splitList.Add(word);
                        word = "";
                    }

                    splitList.Add(c.ToString());
                }
                else
                {
                    word = word + c.ToString();
                }
            }

            if (word != "")
            {
                splitList.Add(word);
                word = "";
            }

            return splitList.ToArray();
        }

        public IEnumerable<ITagSpan<NASMTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (SnapshotSpan curSpan in spans)
            {
                ITextSnapshotLine containingLine = curSpan.Start.GetContainingLine();
                int curLoc = containingLine.Start.Position;

                string[] tokens = Split(containingLine.GetText().ToLower(), _delimiters.ToCharArray());

                NASMTokenTypes overrideType = NASMTokenTypes.Default;

                foreach (string token in tokens)
                {
                    NASMTokenTypes type = GetTokenType(token);

                    SnapshotSpan tokenSpan;

                    if (type == NASMTokenTypes.Comment)
                    {
                        tokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(curLoc, containingLine.End.Position - curLoc));

                        if (tokenSpan.IntersectsWith(curSpan))
                        {
                            yield return new TagSpan<NASMTokenTag>(tokenSpan, new NASMTokenTag(type));
                            break;
                        }

                    }


                    if (overrideType == NASMTokenTypes.String)
                    {
                        if (type == NASMTokenTypes.String)
                        {
                            overrideType = NASMTokenTypes.Default;
                        }
                        else
                        {
                            type = overrideType;
                        }
                    }
                    else
                    {
                        if (type == NASMTokenTypes.String)
                        {
                            overrideType = NASMTokenTypes.String;
                        }
                    }

                    if (overrideType == NASMTokenTypes.Character)
                    {
                        if (type == NASMTokenTypes.Character)
                        {
                            overrideType = NASMTokenTypes.Default;
                        }
                        else
                        {
                            type = NASMTokenTypes.Character;
                        }
                    }
                    else
                    {
                        if (type == NASMTokenTypes.Character)
                        {
                            overrideType = NASMTokenTypes.Character;
                        }
                    }

                    tokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(curLoc, token.Length));

                    if (tokenSpan.IntersectsWith(curSpan))
                    {
                        yield return new TagSpan<NASMTokenTag>(tokenSpan, new NASMTokenTag(type));
                    }

                    //add an extra char location because of the space
                    curLoc += token.Length;

                }
            }
        }
    }
}
