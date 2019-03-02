extern crate regex;
#[macro_use]
extern crate lazy_static;

mod lex;

pub fn x(){
    let code = "void add(int a, int b)\n{\n    return a + b;\n}";
    let out = lex::lex_str(code);
    //let out = lex(Box::new(""));
}