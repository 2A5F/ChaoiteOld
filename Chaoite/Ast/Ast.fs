namespace MeowType.Chaoite.Ast

open Antlr4.Runtime
open Antlr4.Runtime.Tree

type RawAst = IParseTree

type Ast = interface abstract member raw: RawAst end
type AstDefine = interface inherit Ast end
type AstExpr = interface inherit Ast end
type AstType = interface inherit AstExpr end
type AstLiteral = interface inherit AstExpr end
type AstUnit = interface inherit Ast end

type NumType = Num | UInt | Int | Float
type VarTag = New | Stackalloc | Auto
type TypeSuffix = Const | Nullable | Pointer
type NumSuffix = SuffixHas | SuffixSome of int | SuffixNone

type Root =             {raw: RawAst; child: Ast seq}                                                       interface Ast           with member this.raw = this.raw end
and Id =                {raw: RawAst; name: string}                                                         interface Ast           with member this.raw = this.raw end
and RawVarDefine =      {raw: RawAst; Type: AstType option; name: Id; 
                            value: AstExpr option; tag: VarTag; Then: RawVarDefine option}                  interface AstDefine     with member this.raw = this.raw end
and VarDefine =         {raw: RawAst; Type: AstType; name: Id; value: AstExpr option; tag: VarTag}          interface AstDefine     with member this.raw = this.raw end
and NumLiteral =        {raw: RawAst; Type: NumType; value: string; len: NumSuffix}                         interface AstLiteral    with member this.raw = this.raw end
and BoolLiteral =       {raw: RawAst; value: bool}                                                          interface AstLiteral    with member this.raw = this.raw end
and CType =             {raw: RawAst; name: string; suffix: TypeSuffix option}                              interface AstType       with member this.raw = this.raw end
and CTypeVar =          {raw: RawAst; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeAny =          {raw: RawAst; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeVoid =         {raw: RawAst; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeNull =         {raw: RawAst; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeBool =         {raw: RawAst; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeNum =          {raw: RawAst; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeChar =         {raw: RawAst; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeString =       {raw: RawAst; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeInt =          {raw: RawAst; suffix: TypeSuffix option; len: int option}                           interface AstType       with member this.raw = this.raw end
and CTypeUInt =         {raw: RawAst; suffix: TypeSuffix option; len: int option}                           interface AstType       with member this.raw = this.raw end
and CTypeFloat =        {raw: RawAst; suffix: TypeSuffix option; len: int option}                           interface AstType       with member this.raw = this.raw end








