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
// equivalence (<->)
[<InlineData("foo bar true true <-> app", "foo")>]
[<InlineData("foo bar false false <-> app", "foo")>]
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
// pred-first
[<InlineData("x [f] pred-first . app", "x")>]
[<InlineData("x [f] pred-first app app", "")>]
// pred-next
[<InlineData("x [f] pred-first pred-next app", "x [f]")>]
[<InlineData("x [f] pred-first pred-next . app", "x")>]
[<InlineData("x [f] pred-first pred-next pred-next . app", "x f")>]
[<InlineData("x [f] pred-first pred-next pred-next pred-next . app", "x f f")>]
[<InlineData("x [f] pred-first [pred-next] 3 app . app", "x f f")>]
[<InlineData("x [f] pred-first [pred-next] 3 2 * app . app", "x f f f f f")>]
// predecessor
[<InlineData("x [f] 3 pred app", "x f f")>]
[<InlineData("x [f] 3 2 * pred app", "x f f f f f")>]
[<InlineData("x [f] 2 2 * pred 3 + app", "x f f f f f f")>]
// is-zero
[<InlineData("foo bar 0 is-zero app", "foo")>]
[<InlineData("foo bar 1 is-zero app", "bar")>]
[<InlineData("foo bar 3 is-zero app", "bar")>]
// substraction (-)
[<InlineData("x [f] 3 2 - app", "x f")>]
[<InlineData("x [f] 2 0 - app", "x f f")>]
[<InlineData("x [f] 1 3 - app", "x")>]
// less or equal (<=)
[<InlineData("foo bar 2 3 <= app", "bar")>]
[<InlineData("foo bar 3 0 <= app", "foo")>]
// greater or equal (>=)
[<InlineData("foo bar 0 2 >= app", "foo")>]
[<InlineData("foo bar 3 2 >= app", "bar")>]
// equal (=)
[<InlineData("foo bar 2 2 = app", "foo")>]
[<InlineData("foo bar 3 2 = app", "bar")>]
// remainder/modulo (%)
[<InlineData("x [f] 2 2 % app", "x")>]
[<InlineData("x [f] 6 2 % app", "x")>]
[<InlineData("x [f] 5 3 % app", "x f f")>]
[<InlineData("x [f] 3 5 % app", "x f f f")>]
let withStdlib(``the expression`` : string) (``should evaluate to`` : string) =
    (eval ``the expression`` stdlibContext)
    |> stringify
    |> should equal ``should evaluate to``
