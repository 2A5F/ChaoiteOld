module Tests

open System
open Xunit

open MeowType.Chaoite
open MeowType.Chaoite.Chaoite

[<Fact>]
let Base () =
    let code = "int a = 1"
    let parser = Parser(code)
    let tree = parser.getTree ()
    let ast = standardized_ast tree
    for i in ast do 
        let v = i
        Console.WriteLine v
    ()
