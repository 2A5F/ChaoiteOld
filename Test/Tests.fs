module Tests

open System
open Xunit

open MeowType.Chaoite
open MeowType.Chaoite.Standardization

[<Fact>]
let Base () =
    let code = "int a = 1"
    let parser = Parser(code)
    let tree = parser.getTree ()
    let root = standardized_ast tree
    for i in root.child do 
        let v = i
        Console.WriteLine v
    ()
