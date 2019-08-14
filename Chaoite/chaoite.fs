module MeowType.Chaoite.Chaoite

open MeowType.Chaoite.Ast
open MeowType.Chaoite.Parser

open Antlr4.Runtime.Tree

exception UnknownTypeException of obj

let check_var_define (var: C3Parser.VarDefineContext) = 
    let x = var.varTag()
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