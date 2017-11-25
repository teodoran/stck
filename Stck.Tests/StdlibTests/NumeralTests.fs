module StdlibTests.NumeralTests

open Expecto
open Stck

let emptyContext = (Heap Map.empty, Empty)
let stdlibContext = eval (StdlibLoader.load stdlib) emptyContext

[<Tests>]
let tests =
    testList "numeral tests" [
        testCase "0 should apply a function zero times" <| fun _ ->
            let expectedStack = Stack (Operation "x", Empty)
            let _, actualStack = (eval "x [f] 0 app" stdlibContext)
            
            Expect.equal actualStack expectedStack "x [f] 0 app should return x"

        testCase "1 should apply a function one time" <| fun _ ->
            let expectedStack = Stack (Operation "f", Stack (Operation "x", Empty))
            let _, actualStack = (eval "x [f] 1 app" stdlibContext)
            
            Expect.equal actualStack expectedStack "[f] 1 app should return x f"

        testCase "2 should apply a function twice" <| fun _ ->
            let expectedStack = Stack (Operation "f", Stack (Operation "f", Stack (Operation "x", Empty)))
            let _, actualStack = (eval "x [f] 2 app" stdlibContext)
            
            Expect.equal actualStack expectedStack "[f] 2 app should return x f f"

        testCase "inc should increment a numeral" <| fun _ ->
            let expectedStack = Stack (Operation "f", Stack (Operation "x", Empty))
            let _, actualStack = (eval "x [f] 0 inc app" stdlibContext)
            
            Expect.equal actualStack expectedStack "x [f] 0 inc app should return x f"

        testCase "inc should increment a numeral twice" <| fun _ ->
            let expectedStack = Stack (Operation "f", Stack (Operation "f", Stack (Operation "x", Empty)))
            let _, actualStack = (eval "x [f] 0 inc inc app" stdlibContext)
            
            Expect.equal actualStack expectedStack "x [f] 0 inc inc app should return x f f"

        testCase "2 multiplied with 3 should be 6" <| fun _ ->
            let expectedStack = Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "x", Empty)))))))
            let _, actualStack = (eval "x [f] 2 3 * app" stdlibContext)
            
            Expect.equal actualStack expectedStack "x [f] 2 3 * app should return x f f f f f f"

        testCase "0 multiplied with 3 should be 0" <| fun _ ->
            let expectedStack = Stack (Operation "x", Empty)
            let _, actualStack = (eval "x [f] 0 3 * app" stdlibContext)
            
            Expect.equal actualStack expectedStack "x [f] 0 3 * app should return x"

        testCase "2 added to 3 should be 5" <| fun _ ->
            let expectedStack = Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "x", Empty))))))
            let _, actualStack = (eval "x [f] 2 3 + app" stdlibContext)
            
            Expect.equal actualStack expectedStack "x [f] 2 3 + app should return x f f f f f"

        testCase "0 added to 3 should be 3" <| fun _ ->
            let expectedStack = Stack (Operation "f", Stack (Operation "f", Stack (Operation "f", Stack (Operation "x", Empty))))
            let _, actualStack = (eval "x [f] 0 3 + app" stdlibContext)
            
            Expect.equal actualStack expectedStack "x [f] 0 3 + app should return x f f f"
    ]