module MeowType.Chaoite.Chaoite

open MeowType.Chaoite.Ast
open MeowType.Chaoite.Parser

open Antlr4.Runtime
open Antlr4.Runtime.Tree

exception UnknownTypeException of obj

let check_id (id: C3Parser.IdContext) : Id = 
    let text = id.Id().GetText()
    {raw = id; name = text}

let check_type (typ: C3Parser.TypeContext) = 
    ()

let check_var_define (var: C3Parser.VarDefineContext) = 
    let tag = 
        match var.varTag() with
        | null -> VarTag.Auto
        | t -> 
            if t.New() <> null then VarTag.New
            elif t.Stackalloc() <> null then VarTag.Stackalloc
            elif t.Auto() <> null then VarTag.Auto
            else raise <| UnknownTypeException t
    let ``type`` = check_type <| var.``type``()
    let name = check_id <| var.id()
    
    ()

let check_define (d: C3Parser.DefinesContext) =
    if d.ChildCount <> 1 then raise <| UnknownTypeException d
    match d.children.[0] with
    | :? C3Parser.VarDefineContext as var -> check_var_define var
    | _ -> raise <| UnknownTypeException d

let check_code (c: IParseTree) = 
    match c with
    | :? C3Parser.CodeContext as code -> 
        if code.ChildCount <> 1 then raise <| UnknownTypeException code
        match code.children.[0] with
        | :? C3Parser.DefinesContext as define -> check_define define
        | _ -> raise <| UnknownTypeException code
    | _ -> raise <| UnknownTypeException c

let standardized_ast (root: C3Parser.RootContext) = 
    root.children |> Seq.map check_code