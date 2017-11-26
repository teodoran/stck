module StdlibTests

open Xunit
open FsUnit.Xunit
open TestUtil
open Stck

[<Theory>]
// booleans and boolean operations
// false and true
[<InlineData("foo bar true app", "foo")>]
[<InlineData("foo bar false app", "bar")>]
// not
[<InlineData("foo bar true not app", "bar")>]
[<InlineData("foo bar false not app", "foo")>]
// equivalence (<=>)
[<InlineData("foo bar true true <=> app", "foo")>]
[<InlineData("foo bar false false <=> app", "foo")>]
[<InlineData("true [foo] [bar] ?", "foo")>]
[<InlineData("false [foo] [bar] ?", "bar")>]
[<InlineData("false not [foo] [bar] ?", "foo")>]
// and
[<InlineData("true true and [foo] [bar] ?", "foo")>]
[<InlineData("foo bar true true and app", "foo")>]
[<InlineData("false true and [foo] [bar] ?", "bar")>]
[<InlineData("foo bar false true and app", "bar")>]
[<InlineData("true false and [foo] [bar] ?", "bar")>]
[<InlineData("foo bar true false and app", "bar")>]
[<InlineData("false false and [foo] [bar] ?", "bar")>]
[<InlineData("foo bar false false and app", "bar")>]
// or
[<InlineData("true true or [foo] [bar] ?", "foo")>]
[<InlineData("foo bar true true or app", "foo")>]
[<InlineData("false true or [foo] [bar] ?", "foo")>]
[<InlineData("foo bar false true or app", "foo")>]
[<InlineData("true false or [foo] [bar] ?", "foo")>]
[<InlineData("foo bar true false or app", "foo")>]
[<InlineData("false false or [foo] [bar] ?", "bar")>]
[<InlineData("foo bar false false or app", "bar")>]

// numerals
// zero (0)
[<InlineData("x [f] 0 app", "x")>]
// successor function (succ)
[<InlineData("x [f] 0 succ app", "x f")>]
[<InlineData("x [f] 0 succ succ app", "x f f")>]
[<InlineData("x [f] 3 succ app", "x f f f f")>]
// numerals
[<InlineData("x [f] 1 app", "x f")>]
[<InlineData("x [f] 2 app", "x f f")>]
// multiplication (*)
[<InlineData("x [f] 2 3 * app", "x f f f f f f")>]
[<InlineData("x [f] 1 2 * app", "x f f")>]
[<InlineData("x [f] 0 3 * app", "x")>]
[<InlineData("x [f] 3 0 * app", "x")>]
// addition (+)
[<InlineData("x [f] 2 3 + app", "x f f f f f")>]
[<InlineData("x [f] 1 2 + app", "x f f f")>]
[<InlineData("x [f] 0 3 + app", "x f f f")>]
[<InlineData("x [f] 3 0 + app", "x f f f")>]
let withStdlib(``the expression`` : string) (``should evaluate to`` : string) =
    let _, actualStack = (eval ``the expression`` stdlibContext)
    
    actualStack |> strs |> should equal ``should evaluate to``
