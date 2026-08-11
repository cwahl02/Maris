namespace Maris.Compiler.Lexer;

public enum TokenType
{
    Invalid,
    EOF,

    Identifier,
    IntegerLiteral,
    FloatLiteral,
    CharacterLiteral,
    StringLiteral,

    Plus,
    PlusPlus,
    PlusEqual,

    Minus,
    MinusMinus,
    MinusEqual,

    Star,
    StarEqual,

    Slash,
    SlashEqual,

    Percent,
    PercentEqual,

    Equal,
    EqualEqual,

    Bang,
    BangEqual,

    Less,
    LessEqual,
    LessLess,
    LeftShift,
    LeftShiftEqual,

    Greater,
    GreaterEqual,
    RightShift,
    RightShiftEqual,

    Dot,
    DotDot,

    Comma,
    Semicolon,

    Colon,
    ColonColon,
    ColonEqual,
    ColonColonEqual,

    LeftParen,
    RightParen,
    LeftBracket,
    RightBracket,
    LeftBrace,
    RightBrace,
    Underscore,
    Caret,
    CaretEqual,

    Ampersand,
    AmpersandAmpersand,
    AmpersandEqual,

    Pipe,
    PipePipe,
    PipeEqual,

    Question,

    Arrow,             // ->

    // Keywords

    // Control flow
    If,
    Else,
    Continue,
    Break,
    Return,
    Switch,
    Case,
    Default,
    Defer,
    Match,

    // Loops
    While,
    For,
    Foreach,

    // Types
    U8, U16, U32, U64,
    I8, I16, I32, I64,
    F32, F64,
    Void,
    Bool,
    String,

    // User-defined types
    Alias,
    Distinct,
    Enum,
    Struct,
    Union,

    // Collection types
    Array,
    Slice,

    Import,
    Module,
    As
}