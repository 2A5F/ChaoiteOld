namespace MeowType.Chaoite.Ast

open Antlr4.Runtime
open Antlr4.Runtime.Tree

type Ast = interface abstract member raw: IParseTree end
type AstDefine = interface inherit Ast end
type AstExpr = interface inherit Ast end
type AstType = interface inherit AstExpr end
type AstLiteral = interface inherit AstExpr end
type AstUnit = interface inherit Ast end
type AstNumUnit = interface inherit AstUnit end

type NumType = Num | Int | Float
type VarTag = New | Stackalloc | Auto
type TypeSuffix = Const | Nullable | Pointer

type Root =             {raw: IParseTree; child: Ast seq}                                                       interface Ast           with member this.raw = this.raw end
and Id =                {raw: IParseTree; name: string}                                                         interface Ast           with member this.raw = this.raw end
and VarDefine =         {raw: IParseTree; Type: AstType; name: Id; value: AstExpr option; tag: VarTag}          interface AstDefine     with member this.raw = this.raw end
and NumLiteral =        {raw: IParseTree; Type: NumType; value: string; unit: AstNumUnit}                       interface AstLiteral    with member this.raw = this.raw end
and BoolLiteral =       {raw: IParseTree; value: bool}                                                          interface AstLiteral    with member this.raw = this.raw end
and CType =             {raw: IParseTree; name: string; suffix: TypeSuffix option}                              interface AstType       with member this.raw = this.raw end
and CTypeVar =          {raw: IParseTree; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeAny =          {raw: IParseTree; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeVoid =         {raw: IParseTree; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeNull =         {raw: IParseTree; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeBool =         {raw: IParseTree; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeNum =          {raw: IParseTree; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeChar =         {raw: IParseTree; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeString =       {raw: IParseTree; suffix: TypeSuffix option}                                            interface AstType       with member this.raw = this.raw end
and CTypeInt =          {raw: IParseTree; suffix: TypeSuffix option; len: int option}                           interface AstType       with member this.raw = this.raw end
and CTypeUInt =         {raw: IParseTree; suffix: TypeSuffix option; len: int option}                           interface AstType       with member this.raw = this.raw end
and CTypeFloat =        {raw: IParseTree; suffix: TypeSuffix option; len: int option}                           interface AstType       with member this.raw = this.raw end








