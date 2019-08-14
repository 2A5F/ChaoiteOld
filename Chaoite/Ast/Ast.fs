namespace MeowType.Chaoite.Ast

open Antlr4.Runtime

type TokenRange = {start: IToken; stop: IToken}

type Ast = interface abstract member range: TokenRange end
type AstDefine = interface inherit Ast end
type AstExpr = interface inherit Ast end
type AstType = interface inherit AstExpr end
type AstLiteral = interface inherit AstExpr end
type AstUnit = interface inherit Ast end
type AstNumUnit = interface inherit AstUnit end

type NumType = Num | Int | Float

type Root =             {range: TokenRange; child: Ast seq}                                             interface Ast           with member this.range = this.range end
and Id =                {range: TokenRange; name: string}                                               interface Ast           with member this.range = this.range end
and VarDefine =         {range: TokenRange; Type: AstType; name: Id; value: AstExpr option}             interface AstDefine     with member this.range = this.range end
and NumLiteral =        {range: TokenRange; Type: NumType; value: string; unit: AstNumUnit}             interface AstLiteral    with member this.range = this.range end
and BoolLiteral =       {range: TokenRange; value: bool}                                                interface AstLiteral    with member this.range = this.range end