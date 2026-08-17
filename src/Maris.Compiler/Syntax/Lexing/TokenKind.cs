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

    // Keywords
    Import,
    Module,
    As
}