#[derive(Debug)]
pub enum Tokens {
    word(String),
    biType(BuiltinType),
    keyWord(KeyWord),
    eof,
}

#[derive(Debug)]
pub enum BuiltinType {
    void
}

#[derive(Debug)]
pub enum KeyWord {
    kw_return,
}