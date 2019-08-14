namespace MeowType.Chaoite

open Antlr4.Runtime
open Antlr4.Runtime.Tree
open MeowType.Chaoite.Parser

type Parser(stream: ITokenStream) = 
    let parser = C3Parser(stream)
    new(code: string) = Parser(AntlrInputStream(code))
    new(stream: ICharStream) = 
        let lexer = C3Lexer(stream)
        Parser(CommonTokenStream(lexer))
    member __.getTree() = 
        parser.root()
