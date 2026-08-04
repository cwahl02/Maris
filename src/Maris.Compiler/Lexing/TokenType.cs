namespace Maris.Compiler.Lexing;

public enum TokenType
{
    // Special
    Invalid,
    EndOfFile,

    // Identifiers & literals
    Identifier,
    IntegerLiteral,
    FloatLiteral,
    CharacterLiteral,
    StringLiteral,

    // Punctuation
    Dot,
    Comma,
    Colon,
    Semicolon,

    LeftParen,
    RightParen,
    LeftBracket,
    RightBracket,
    LeftBrace,
    RightBrace,

    // Single-character operators
    Plus,
    Minus,
    Star,
    Slash,
    Percent,

    Caret,
    Ampersand,
    Pipe,

    Bang,
    Question,

    Equal,
    
    Less,
    Greater,

    Tilde,

    At,
    Hash,

    // Compound operators
    ColonColon,         // ::

    EqualEqual,         // ==
    BangEqual,          // !=

    LessEqual,          // <=
    GreaterEqual,       // >=

    LeftShift,          // <<
    RightShift,         // >>

    PlusEqual,          // +=
    MinusEqual,         // -=
    StarEqual,          // *=
    SlashEqual,         // /=
    PercentEqual,       // %=

    CaretEqual,         // ^=
    AmpersandEqual,     // &=
    PipeEqual,          // |=

    ColonEqual,         // :=
    ColonColonEqual,    // ::=

    LeftShiftEqual,     // <<=
    RightShiftEqual,    // >>=

    PlusPlus,           // ++
    MinusMinus,         // --

    AmpersandAmpersand, // &&
    PipePipe,           // ||

    Arrow,              // ->
    FatArrow,           // =>

    Range,             // ..


    // Keywords
    // Control flow
    If,
    Else,
    Switch,
    Case,
    Default,
    Match,

    // Loops
    For,
    Foreach,
    While,

    // Control flow
    Break,
    Continue,
    Return,
    Defer,

    // User-defined types
    Alias,
    Distinct,
    Enum,
    Struct,
    Union,

    // Collection types
    Array,
    Slice,

    // Types
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

    Bool,
    String,
    Void,

    // Modules
    Import,
    Module,
    As,
}