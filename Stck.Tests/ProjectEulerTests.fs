module ProjectEulerTests

open Xunit
open FsUnit.Xunit
open TestUtil
open Stck

[<Fact>]
let ``STCK should solve project euler problem No 1`` () =
    let program =
        "```If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9.
            The sum of these multiples is 23.
            
            Find the sum of all the multiples of 3 or 5 below 1000.```
            
        [dup 0 =] next-is-zero #
        
        [dup 3 % 0 =] multiple-of-3 #
        
        [dup 5 % 0 =] multiple-of-5 #

        [
            multiple-of-3
                [dup]
                [multiple-of-5
                    [dup]
                    [] ?] ?
        ] keep-number-if-multiple-of-3-or-5 #

        [
            next-is-zero not
                [keep-number-if-multiple-of-3-or-5 1 - generate-numbers]
                [.] ?
        ] generate-numbers #

        [
            swap next-is-zero not
                [swap + sum-numbers]
                [.] ?
        ] sum-numbers #

        0 9 generate-numbers sum-numbers x swap [f] swap app"
    
    let _, actualStack = (eval program stdlibContext)

    actualStack |> strs |> should equal "x f f f f f f f f f f f f f f f f f f f f f f f" // 23


[<Fact>]
let ``STCK should solve project euler problem No 2`` () =
    let program =
        "```Project Euler problem two
            Considering the terms in the Fibonacci sequence
            whose values do not exceed 10,
            find the sum of the even-valued terms.
            (originally it vas values not exceeding four million,
            but that takes too long to execute with naive church encoding)```

        [2dup +] next-fib #

        [dup 2 % 0 =] is-even #

        [dup 0 =] next-is-zero #

        [
            next-fib dup 10 >=
                [fib-under-10]
                [.] ?
        ] fib-under-10 #

        [
            swap is-even [+] [.] ?
            swap next-is-zero [.] [swap sum-if-even] ?
        ] sum-if-even #

        x [f] 0 1 2 fib-under-10 sum-if-even app"
    
    let _, actualStack = (eval program stdlibContext)

    actualStack |> strs |> should equal "x f f f f f f f f f f" // 10
