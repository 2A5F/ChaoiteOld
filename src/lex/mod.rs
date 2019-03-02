use std::rc::Rc;
use std::ptr;
use regex::Regex;

mod token;

lazy_static! {
    static ref word: Regex = Regex::new(r"\w").unwrap();
}


pub type TokenIter<'a> = Iterator<Item = token::Tokens> + 'a;
pub type RcTokenIter<'a> = Rc<TokenIter<'a>>;
pub type CharIter<'a> = Iterator<Item = char> + 'a;
pub type RcCharIter<'a> = Rc<CharIter<'a>>;

struct Lex<'a>(RcCharIter<'a>);

pub fn lex_str<'a>(code: &'a str) -> RcTokenIter<'a>
{
    lex(Rc::new(code.chars()))
}

pub fn lex_string<'a>(code: &'a mut String) -> RcTokenIter<'a>
{
    lex(Rc::new(code.drain(..)))
}

pub fn lex<'a>(code: RcCharIter<'a>) -> RcTokenIter<'a> {
    Rc::new(Lex(code))
}

impl<'a> Iterator for Lex<'a> {
    type Item = token::Tokens;

    fn next(&mut self) -> Option<Self::Item> {
        None
    }
}