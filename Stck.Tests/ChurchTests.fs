module ChurchTests

open Xunit
open FsUnit.Xunit
open TestUtil
open Stck

[<Theory>]
// false and true
[<InlineData("true", "[.]")>]
[<InlineData("false", "[swap .]")>]
[<InlineData("[.]", "[.]")>]
[<InlineData("[swap .]", "[swap .]")>]

// empty program
[<InlineData("", "")>]

// exceptions
[<InlineData("fail throw", "Exception: fail")>]
[<InlineData(".", "Exception: StackUnderflow")>]
[<InlineData("a-word app", "a-word Exception: MissingQuotation")>]
let withStdlib(``the expression`` : string) (``should evaluate to`` : string) =
    (eval ``the expression`` stdlibContext)
    |> stringifyc
    |> should equal ``should evaluate to``
