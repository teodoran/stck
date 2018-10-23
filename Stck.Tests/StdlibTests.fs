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
[<InlineData("true not", "false")>]
[<InlineData("false not", "true")>]
// equivalence (<->)
[<InlineData("true true <->", "true")>]
[<InlineData("false false <->", "true")>]
[<InlineData("true false <->", "false")>]
[<InlineData("false true <->", "false")>]
// conditionals (?)
[<InlineData("true [foo] [bar] ?", "foo")>]
[<InlineData("false [foo] [bar] ?", "bar")>]
[<InlineData("false not [foo] [bar] ?", "foo")>]
// and
[<InlineData("true true and", "true")>]
[<InlineData("false false and", "false")>]
[<InlineData("false true and", "false")>]
[<InlineData("true false and", "false")>]
// or
[<InlineData("true true or", "true")>]
[<InlineData("false true or", "true")>]
[<InlineData("true false or", "true")>]
[<InlineData("false false or", "false")>]
// xor
[<InlineData("true true xor", "false")>]
[<InlineData("false true xor", "true")>]
[<InlineData("true false xor", "true")>]
[<InlineData("false false xor", "false")>]
// numerals
// zero (0)
[<InlineData("x [f] 0 app", "x")>]
// successor function (succ)
[<InlineData("0 succ", "1")>]
[<InlineData("0 succ succ", "2")>]
[<InlineData("3 succ", "4")>]
// numerals
[<InlineData("x [f] 1 app", "x f")>]
[<InlineData("x [f] 2 app", "x f f")>]
// multiplication (*)
[<InlineData("2 3 *", "6")>]
[<InlineData("1 2 *", "2")>]
[<InlineData("0 3 *", "0")>]
[<InlineData("3 0 *", "0")>]
// addition (+)
[<InlineData("1 1 +", "2")>]
[<InlineData("2 3 +", "5")>]
[<InlineData("1 2 +", "3")>]
[<InlineData("0 3 +", "3")>]
[<InlineData("3 0 +", "3")>]
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
[<InlineData("3 pred", "2")>]
[<InlineData("3 2 * pred", "5")>]
[<InlineData("2 2 * pred 3 +", "6")>]
// is-zero
[<InlineData("0 is-zero", "true")>]
[<InlineData("1 is-zero", "false")>]
[<InlineData("3 is-zero", "false")>]
// substraction (-)
[<InlineData("3 2 -", "1")>]
[<InlineData("2 0 -", "2")>]
[<InlineData("1 3 -", "0")>]
// less or equal (<=)
[<InlineData("2 3 <=", "false")>]
[<InlineData("3 0 <=", "true")>]
[<InlineData("1 1 <=", "true")>]
// greater or equal (>=)
[<InlineData("0 2 >=", "true")>]
[<InlineData("3 2 >=", "false")>]
[<InlineData("1 1 >=", "true")>]
// equal (=)
[<InlineData("2 2 =", "true")>]
[<InlineData("3 2 =", "false")>]
// remainder/modulo (%)
[<InlineData("2 2 %", "0")>]
[<InlineData("6 2 %", "0")>]
[<InlineData("5 3 %", "2")>]
[<InlineData("3 5 %", "3")>]
let withStdlib(``the expression`` : string) (``should evaluate to`` : string) =
    (eval ``the expression`` stdlibContext)
    |> stringify
    |> should equal ``should evaluate to``
