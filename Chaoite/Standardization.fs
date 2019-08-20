module MeowType.Chaoite.Standardization

open MeowType.Chaoite.Ast
open MeowType.Chaoite.Parser
open MeowType.Chaoite.Utils

open Antlr4.Runtime
open Antlr4.Runtime.Tree

exception UnknownTypeException of obj

let check_id (id: C3Parser.IdContext) : Id = 
    let text = id.Id().GetText()
    {raw = id; name = text}

let check_basetype typ (bt: C3Parser.BaseTypesContext) check_suffix : AstType =
    if bt.Var() <> null then upcast ({raw = typ; suffix = check_suffix ()} : CTypeVar)
    elif bt.Any() <> null then upcast ({raw = typ; suffix = check_suffix ()} : CTypeAny)
    elif bt.Void() <> null then upcast ({raw = typ; suffix = check_suffix ()} : CTypeVoid)
    elif bt.Null() <> null then upcast ({raw = typ; suffix = check_suffix ()} : CTypeNull)
    elif bt.Bool() <> null then upcast ({raw = typ; suffix = check_suffix ()} : CTypeBool)
    elif bt.Num() <> null then upcast ({raw = typ; suffix = check_suffix ()} : CTypeNum)
    elif bt.Char() <> null then upcast ({raw = typ; suffix = check_suffix ()} : CTypeChar)
    elif bt.String() <> null then upcast ({raw = typ; suffix = check_suffix ()} : CTypeString)
    elif bt.Int() <> null then upcast ({raw = typ; suffix = check_suffix (); len = None} : CTypeInt)
    elif bt.I8() <> null then upcast ({raw = typ; suffix = check_suffix (); len = Some 8} : CTypeInt)
    elif bt.I16() <> null then upcast ({raw = typ; suffix = check_suffix (); len = Some 16} : CTypeInt)
    elif bt.I32() <> null then upcast ({raw = typ; suffix = check_suffix (); len = Some 32} : CTypeInt)
    elif bt.I64() <> null then upcast ({raw = typ; suffix = check_suffix (); len = Some 64} : CTypeInt)
    elif bt.I128() <> null then upcast ({raw = typ; suffix = check_suffix (); len = Some 128} : CTypeInt)
    elif bt.UInt() <> null then upcast ({raw = typ; suffix = check_suffix (); len = None} : CTypeUInt)
    elif bt.U8() <> null then upcast ({raw = typ; suffix = check_suffix (); len = Some 8} : CTypeUInt)
    elif bt.U16() <> null then upcast ({raw = typ; suffix = check_suffix (); len = Some 16} : CTypeUInt)
    elif bt.U32() <> null then upcast ({raw = typ; suffix = check_suffix (); len = Some 32} : CTypeUInt)
    elif bt.U64() <> null then upcast ({raw = typ; suffix = check_suffix (); len = Some 64} : CTypeUInt)
    elif bt.U128() <> null then upcast ({raw = typ; suffix = check_suffix (); len = Some 128} : CTypeUInt)
    elif bt.Float() <> null then upcast ({raw = typ; suffix = check_suffix (); len = None} : CTypeFloat)
    elif bt.F32() <> null then upcast ({raw = typ; suffix = check_suffix (); len = Some 32} : CTypeFloat)
    elif bt.F64() <> null then upcast ({raw = typ; suffix = check_suffix (); len = Some 64} : CTypeFloat)
    elif bt.F128() <> null then upcast ({raw = typ; suffix = check_suffix (); len = Some 128} : CTypeFloat)
    else raise <| UnknownTypeException bt

let check_type (typ: C3Parser.TypeContext) : AstType = 
    if typ.ArrowR2L() <> null then failwith "todo"
    else 
        let id = typ.id()
        let baseType = typ.baseTypes()
        let check_suffix () = 
            let suffix = typ.typeSuffix()
            if suffix = null then None
            elif suffix.Exclamation() <> null then Some <| TypeSuffix.Const
            elif suffix.Question() <> null then Some <| TypeSuffix.Nullable
            elif suffix.Star() <> null then Some <| TypeSuffix.Pointer
            else raise <| UnknownTypeException suffix
        if id <> null then upcast ({raw = typ; name = id.GetText(); suffix = check_suffix ()} : CType)
        elif baseType <> null then check_basetype typ baseType check_suffix 
        else raise <| UnknownTypeException typ

let check_literals (liter: C3Parser.LiteralsContext) : AstLiteral = 
    if liter.ChildCount <> 1 then raise <| UnknownTypeException liter
    if liter.This() <> null then upcast ({raw = liter} : ThisLiteral)
    elif liter.It() <> null then upcast ({raw = liter} : ItLiteral)
    elif liter.NaN() <> null then upcast ({raw = liter} : NaNLiteral)
    else 
        match liter.children.[0] with
        | :? C3Parser.LiterIntContext as liter_int -> 
            let value = liter_int.LiterInt().GetText()
            let len = 
                match liter_int.literIntSuffix() with 
                | null -> NumSuffix.SuffixNone 
                | suffix -> 
                    if suffix.ChildCount <> 1 then raise <| UnknownTypeException suffix
                    if suffix.IntSuffix() <> null then NumSuffix.SuffixHas
                    elif suffix.I8() <> null then NumSuffix.SuffixSome 8
                    elif suffix.I16() <> null then NumSuffix.SuffixSome 16
                    elif suffix.I32() <> null then NumSuffix.SuffixSome 32
                    elif suffix.I64() <> null then NumSuffix.SuffixSome 64
                    elif suffix.I128() <> null then NumSuffix.SuffixSome 128
                    else raise <| UnknownTypeException suffix
            upcast ({raw = liter_int; Type = NumType.Int; value = value; len = len}: NumLiteral)
        | :? C3Parser.LiterUIntContext as liter_uint -> 
            let value = liter_uint.LiterUInt().GetText()
            let len = 
                match liter_uint.literUIntSuffix() with 
                | null -> NumSuffix.SuffixNone 
                | suffix -> 
                    if suffix.ChildCount <> 1 then raise <| UnknownTypeException suffix
                    if suffix.UIntSuffix() <> null then NumSuffix.SuffixHas
                    elif suffix.U8() <> null then NumSuffix.SuffixSome 8
                    elif suffix.U16() <> null then NumSuffix.SuffixSome 16
                    elif suffix.U32() <> null then NumSuffix.SuffixSome 32
                    elif suffix.U64() <> null then NumSuffix.SuffixSome 64
                    elif suffix.U128() <> null then NumSuffix.SuffixSome 128
                    else raise <| UnknownTypeException suffix
            upcast ({raw = liter_uint; Type = NumType.UInt; value = value; len = len}: NumLiteral)
        | :? C3Parser.LiterFloatContext as liter_float -> 
            let value = liter_float.LiterFloat().GetText()
            let len = 
                match liter_float.literFloatSuffix() with 
                | null -> NumSuffix.SuffixNone 
                | suffix -> 
                    if suffix.ChildCount <> 1 then raise <| UnknownTypeException suffix
                    if suffix.FloatSuffix() <> null then NumSuffix.SuffixHas
                    elif suffix.F32() <> null then NumSuffix.SuffixSome 32
                    elif suffix.F64() <> null then NumSuffix.SuffixSome 64
                    elif suffix.F128() <> null then NumSuffix.SuffixSome 128
                    else raise <| UnknownTypeException suffix
            upcast ({raw = liter_float; Type = NumType.Float; value = value; len = len}: NumLiteral)
        | :? C3Parser.LiterBoolContext as liter_bool ->
            if liter_bool.True() <> null then upcast ({raw = liter_bool; value = true}: BoolLiteral)
            elif liter_bool.False() <> null then upcast ({raw = liter_bool; value = false}: BoolLiteral)
            else raise <| UnknownTypeException liter_bool
        | unknow -> raise <| UnknownTypeException unknow // todo other

let check_primary_expr (expr: C3Parser.Primary_exprContext) = 
    if expr.ChildCount <> 1 then raise <| UnknownTypeException expr
    match expr.children.[0] with 
    | :? C3Parser.LiteralsContext as liter -> check_literals liter
    // todo other
    | unknow -> raise <| UnknownTypeException unknow // todo other

let check_expr (expr: IParseTree) : AstExpr = 
    match expr with
    | :? C3Parser.Primary_exprContext as primary_expr -> upcast check_primary_expr primary_expr
    // todo other
    | _ -> raise <| UnknownTypeException expr // todo other

let rec find_expr (expr: IParseTree) : IParseTree = 
    match expr.ChildCount with
    | 0 -> raise <| UnknownTypeException expr
    | 1 -> 
        match expr.GetChild 0 with
        | :? C3Parser.Primary_exprContext as primary_expr -> upcast primary_expr
        | :? ParserRuleContext as rule -> find_expr rule
        | _ -> expr
    | _ -> expr

let check_var_tag (tag: C3Parser.VarTagContext) : VarTag =
    match tag with
    | null -> VarTag.Auto
    | t -> 
        if t.New() <> null then VarTag.New
        elif t.Stackalloc() <> null then VarTag.Stackalloc
        elif t.Auto() <> null then VarTag.Auto
        else raise <| UnknownTypeException t

let rec check_var_defines (var: C3Parser.VarDefinesContext) (list: (RawVarDefine option -> RawVarDefine option) System.Collections.Generic.Stack) = 
    match var with
    | null -> ()
    | _ -> 
        let tag = check_var_tag <| var.varTag()
        let ``type`` = match var.``type``() with null -> None | t -> check_type t |> Some
        let name = check_id <| var.id()
        let expr = match var.expr() with null -> None | expr -> find_expr expr |> check_expr |> Some
        let fn more = 
            ({raw = var; Type = ``type``; name = name; value = expr; tag = tag; Then = more} : RawVarDefine)
            |> Some
        list.Push fn
        check_var_defines (var.varDefines()) list

let check_var_define (var: C3Parser.VarDefineContext) : RawVarDefine = 
    let tag = check_var_tag <| var.varTag()
    let ``type`` = check_type <| var.``type``()
    let name = check_id <| var.id()
    let expr = match var.expr() with null -> None | expr -> find_expr expr |> check_expr |> Some
    let list = System.Collections.Generic.Stack()
    check_var_defines (var.varDefines()) list
    let fold last now = now last
    let more = list |> Utils.PopFor |> Seq.fold fold None
    {raw = var; Type = Some ``type``; name = name; value = expr; tag = tag; Then = more} : RawVarDefine

let check_define (d: C3Parser.DefinesContext) : AstDefine =
    if d.ChildCount <> 1 then raise <| UnknownTypeException d
    match d.children.[0] with
    | :? C3Parser.VarDefineContext as var -> upcast check_var_define var
    // todo
    | unknow -> raise <| UnknownTypeException unknow
    
let check_code (c: IParseTree) : Ast = 
    match c with
    | :? C3Parser.CodeContext as code -> 
        if code.ChildCount <> 1 then raise <| UnknownTypeException code
        match code.children.[0] with
        | :? C3Parser.DefinesContext as define -> upcast check_define define
        // todo
        | unknow -> raise <| UnknownTypeException unknow
    | _ -> raise <| UnknownTypeException c

let standardized_ast (root: C3Parser.RootContext) : Root = 
    {raw = root; child = (root.children |> Seq.map check_code)}  