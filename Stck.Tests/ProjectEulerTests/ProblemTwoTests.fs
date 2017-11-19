module ProjectEulerTests.ProblemTwoTests

open Expecto
open Stck

[<Tests>]
let tests =
    testCase "STCK should solve project euler problem No 2" <| fun _ ->
        let program =
            "```Project Euler problem two                      \
                Considering the terms in the Fibonacci sequence\
                whose values do not exceed four million,       \
                find the sum of the even-valued terms.```      \
                                                               \
            [2dup +] next-fib #                                \
                                                               \
            [dup 2 % 0 =] is-even #                            \
                                                               \
            [dup 0 =] next-is-zero #                           \
                                                               \
            [                                                  \
            	next-fib                                       \
            	dup 4000000 >                                  \
                    [fib-under-4m]                             \
                    [.] ?                                      \
            ] fib-under-4m #                                   \
                                                               \
            [                                                  \
            	swap is-even [+] [.] ?                         \
            	swap next-is-zero [.] [swap sum-if-even] ?     \
            ] sum-if-even #                                    \
                                                               \
            0 1 2 fib-under-4m sum-if-even"
        
        let expectedStack = Stack (Operation "4613732", Empty)
        let _, actualStack = (eval "[anonymous stack]" (Heap Map.empty, Empty))
        
        Expect.equal 42 42 "ignore this result for now"
        Expect.equal actualStack expectedStack
            "4613732 should be on the stack"