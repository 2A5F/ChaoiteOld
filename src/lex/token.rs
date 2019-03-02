#[derive(Debug)]
pub enum Tokens {
    #[allow(dead_code)]
    #[allow(non_camel_case_types)]
    word(String),
    #[allow(dead_code)]
    #[allow(non_camel_case_types)]
    biType(BuiltinType),
    #[allow(non_camel_case_types)]
    #[allow(dead_code)]
    keyWord(KeyWord),
    #[allow(non_camel_case_types)]
    #[allow(dead_code)]
    eof,
}

#[derive(Debug)]
pub enum BuiltinType {
    #[allow(dead_code)]
    #[allow(non_camel_case_types)]
    void
}

#[derive(Debug)]
pub enum KeyWord {
    #[allow(dead_code)]
    #[allow(non_camel_case_types)]
    kw_return,
}