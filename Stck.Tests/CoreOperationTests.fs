module CoreOperationTests

open Xunit
open FsUnit.Xunit
open Stck

[<Theory>]
// empty program
[<InlineData("", "")>]

// stack operations
// push symbols
[<InlineData("my-symbol", "my-symbol")>]
[<InlineData("first next last", "first next last")>]
// drop (.)
[<InlineData("not-empty .", "")>]
[<InlineData(".", "Exception: StackUnderflow")>]
[<InlineData("keep-this drop-this and-drop-this . . drop-this .", "keep-this")>]
[<InlineData("..", "")>]
// dup
[<InlineData("duplicated dup", "duplicated duplicated")>]
[<InlineData("dup", "Exception: StackUnderflow")>]
// swap
[<InlineData("last first swap", "first last")>]
[<InlineData("one swap", "one Exception: StackUnderflow")>]
// emp
[<InlineData("emp", "[true]")>]
[<InlineData("something emp", "something [false]")>]

// anonymous stacks and their operations
// anonymous stacks / quotations ([])
[<InlineData("[anonymous stack]", "[stack anonymous]")>]
[<InlineData("[anonymous [nested] stack]", "[stack [nested] anonymous]")>]
[<InlineData("first [next last] app", "first next last")>]
[<InlineData("a-word app", "a-word Exception: MissingQuotation")>]
// concat (||)
[<InlineData("[a b] [c d] ||", "[d c b a]")>]
[<InlineData("[one] ||", "[one] Exception: StackUnderflow")>]
// chop (|)
[<InlineData("[a b c] |", "[c b] [a]")>]
[<InlineData("[] |", "[] []")>]
[<InlineData("one |", "one Exception: StackUnderflow")>]
// ontail (>>)
[<InlineData("[last] first >>", "[first last]")>]
[<InlineData("one >>", "one Exception: StackUnderflow")>]
// ontop (<<)
[<InlineData("[last] first <<", "[last first]")>]
[<InlineData("one <<", "one Exception: StackUnderflow")>]

// definitions (#)
[<InlineData("[word-content] my-word #", "")>]
[<InlineData("[word-content] my-word # my-word", "word-content")>]
[<InlineData("not-quotation my-word #", "not-quotation my-word Exception: StackUnderflow")>]

// exceptions
[<InlineData("fail throw", "Exception: fail")>]
[<InlineData("throw", "Exception: StackUnderflow")>]
[<InlineData("fail throw err", "[true]")>]
[<InlineData("not-exception: err", "[false]")>]
[<InlineData("err", "Exception: StackUnderflow")>]

// comments (```comment```)
[<InlineData("```This is a comment```", "")>]
[<InlineData("first ```should not be affected``` last", "first last")>]
let withoutDependencies(``the expression`` : string) (``should evaluate to`` : string) =
    (eval ``the expression`` emptyContext)
    |> stringify
    |> should equal ``should evaluate to``
