namespace Maris.Compiler.Syntax.Lexing;

public enum TokenKind
{
    Eof,
    Invalid,

    Identifier,
    CharacterLiteral,
    StringLiteral,
    IntegerLiteral,
    FloatLiteral,


    // Plus
    Plus,
    PlusPlus,
    PlusEqual,

    // Minus
    Minus,
    MinusMinus,
    MinusEqual,
    Arrow,

    // Star
    Star,
    StarEqual,

    // Slash
    Slash,
    SlashEqual,

    // Percent
    Percent,
    PercentEqual,

    // Ampersand
    Ampersand,
    AmpersandAmpersand,
    AmpersandEqual,

    // Pipe
    Pipe,
    PipePipe,
    PipeEqual,

    // Caret
    Caret,
    CaretEqual,

    // Colon
    Colon,
    ColonColon,
    ColonEqual,
    ColonColonEqual,

    // Equal
    Equal,
    EqualEqual,

    // Dot
    Dot,
    DotDot,

    // Greater
    Greater,
    GreaterEqual,
    RightShift,
    RightShiftEqual,

    // Less
    Less,
    LessEqual,
    LeftShift,
    LeftShiftEqual,

    // Bang
    Bang,
    BangEqual,

    // Tilde
    Tilde,
    LeftParen,
    RightParen,
    LeftBrace,
    RightBrace,
    LeftBracket,
    RightBracket,
    Comma,
    Semicolon,
    Underscore,

    // Keywords

    // Control Flow
    If,
    Else,
    While,
    For,
    Return,
    Defer,
    Continue,
    Break,
    Switch,
    Case,
    Default,
    Match,

    // Primitive Types
    U8,
    U16,
    U32,
    U64,
    I8,
    I16,
    I32,
    I64,
    F32,
    F64,
    Void,
    Bool,
    String,

    // User Defined Types
    Alias,
    Distinct,
    Enum,
    Struct,
    Union,

    // Boolean Literals
    True,
    False,

    // Boolean Operators
    And,
    Or,

    Import,
    Module,
    As
}